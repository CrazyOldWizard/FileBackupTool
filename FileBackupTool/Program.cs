using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace FileBackupTool
{
    class Program
    {
        private static Settings settingsFile = new Settings();
        public static string todaysDate = DateTime.Today.ToString("MM-dd-yyy");
        static void Main(string[] args)
        {

            if(args.Contains("SettingsUI"))
            {
                
                return;
            }

            while (true)
            {
                //read and update settings from json file.
                var setSettings = ReadSettingsFile();
                if (setSettings == false)
                {
                    Console.WriteLine("Backup failed - couldn't read settings file.");
                    return;
                }
                if(Directory.Exists(settingsFile.BackupSaveDirectory))
                {
                    DeleteOldBackups();
                    BackupFiles();
                }
                System.Threading.Thread.Sleep(settingsFile.BackupInterval * 60000);
            }
            

        }

        private static bool ReadSettingsFile()
        {
            //settings file path
            var settingsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SettingsFile.json");
            if (File.Exists(settingsFilePath))
            {
                try
                {
                    //read settings file
                    Console.WriteLine("Reading settings file...");
                    var rawSettings = File.ReadAllText(settingsFilePath);
                    var jsonObject = Newtonsoft.Json.JsonConvert.DeserializeObject<Settings>(rawSettings);
                    if(jsonObject != null)
                    {
                        settingsFile = jsonObject;
                        Console.WriteLine("Success");
                        return true;
                    }
                    WriteToLog("Settings file couldn't be deserialized!", EventLogEntryType.Error);
                    return false;
                }
                catch(Exception e)
                {
                    Console.WriteLine("Couldn't read json file!");
                    WriteToLog(e.Message, EventLogEntryType.Error);
                    return false;
                }
            }
            else
            {
                WriteToLog("Failed to read settings file", EventLogEntryType.Error);
                return false;
            }    
        }


        private static void BackupFiles()
        {
            if (settingsFile.BackupDirectories.Length == 0)
                return;

            Directory.CreateDirectory(Path.Combine(settingsFile.BackupSaveDirectory, settingsFile.BackupName, todaysDate));
            if (Directory.Exists(Path.Combine(settingsFile.BackupSaveDirectory, settingsFile.BackupName, todaysDate)) == false)
                return;
            foreach(var dir in settingsFile.BackupDirectories)
            {
                string roboCopyScirpt = GetRoboCopyScript(dir, settingsFile.BackupSaveDirectory);
                Console.WriteLine($"Backing up {dir}");
                WriteToLog($"Backing up {dir}" + $" Script: {roboCopyScirpt}", EventLogEntryType.Information);

                Console.WriteLine("Starting robocopy...");
                Process rc = new Process();
                rc.StartInfo.Arguments = roboCopyScirpt;
                rc.StartInfo.FileName = "CMD.EXE";
                rc.StartInfo.CreateNoWindow = true;
                rc.StartInfo.UseShellExecute = false;
                rc.Start();
                rc.WaitForExit();
                Console.WriteLine("Robocopy finished...");
            }
        }


        private static void WriteToLog(string message, EventLogEntryType entryType)
        {
            using (EventLog eventLog = new EventLog("Application"))
            {
                eventLog.Source = "FileBackupTool";
                eventLog.WriteEntry(message, entryType);
            }
            Console.WriteLine(message);
        }

        private static string GetRoboCopyScript(string dirToBackup, string BackupDir)
        {
            Console.WriteLine("Creating robocopy script...");
            var dirInfo = new DirectoryInfo(dirToBackup);
            var script = new StringBuilder();

            script.Append("/C robocopy ");
            script.Append("\"");
            script.Append(dirToBackup);
            script.Append("\" ");
            script.Append("\"");
            var savePath = Path.Combine(settingsFile.BackupSaveDirectory, settingsFile.BackupName, todaysDate, dirInfo.Name);
            script.Append(savePath);
            script.Append("\" ");
            script.Append(@"/E /Z /FFT /R:3 /W:10 /IPG:10"); //ipg = deley/throttle + if using ipg you can't use /MT for multi-threading
            return script.ToString();
        }

        private static void DeleteOldBackups()
        {
            var dirs = Directory.GetDirectories(settingsFile.BackupSaveDirectory);
            var backupDirs = new List<DirectoryInfo>();

            foreach (var dir in dirs)//loop through all the days in each backup and add them to the list
            {
                var subDs = Directory.GetDirectories(dir);
                foreach(var d in subDs)
                {
                    var dInfo = new DirectoryInfo(d);
                    if (dInfo.LastWriteTimeUtc < DateTime.UtcNow.AddDays(-Math.Abs(settingsFile.KeepBackupsFor)))
                    {
                        try
                        {
                            Console.WriteLine($"Deleting {dInfo.FullName}");
                            dInfo.Delete(true);
                            WriteToLog($"Deleted {dInfo.FullName} - Reason: Older than {settingsFile.KeepBackupsFor} days.", EventLogEntryType.Information);
                        }
                        catch
                        {
                            Console.WriteLine($"Failed to delete {dInfo.FullName}");
                            WriteToLog($"Failed to delete {dInfo.FullName}", EventLogEntryType.Error);
                        }
                    }
                    else
                    {
                        backupDirs.Add(dInfo);
                    }
                    
                }
            }
            
            //sort backup directories by date - oldest first
            backupDirs.OrderBy(d => d.LastWriteTimeUtc).ToList();
            //set max backup size in bytes
            var maxBackupSize = settingsFile.BackupSizeGB * (long)1000000000;
            //get current backup size
            var currentBackupSize = DirSize(new DirectoryInfo(settingsFile.BackupSaveDirectory));
            if(currentBackupSize > maxBackupSize)
            {
                //loop through all dirs (oldest first) and delete them until the backup is below the max size
                foreach(var dir in backupDirs)
                {
                    try
                    {
                        var currentDirSize = DirSize(dir);
                        Console.WriteLine($"Deleting directory {dir.FullName} because backup is bigger than max backup size");
                        dir.Delete(true);
                        Console.WriteLine("Success");
                        WriteToLog($"Deleted directory {dir.FullName} " +
                            $"because backup is bigger than max backup size ({settingsFile.BackupSizeGB}GB)", EventLogEntryType.Information);
                        currentBackupSize -= currentDirSize;
                        if (currentBackupSize < maxBackupSize)
                        {
                            return; //folder is now small enough, so don't delete anything else.
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        WriteToLog($"Removing directory to make space failed! Reason: {e.Message}", EventLogEntryType.Error);
                        continue;
                    }
                }

            }

        }

        public static long DirSize(DirectoryInfo d)
        {
            long size = 0;
            // Add file sizes.
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
            }
            // Add subdirectory sizes.
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += DirSize(di);
            }
            return size;
        }
    }
}
