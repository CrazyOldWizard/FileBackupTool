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
                //open settings ui instead
                return;
            }

            //read and update settings from json file.
            var setSettings = ReadSettingsFile();
            if(setSettings == false)
            {
                Console.WriteLine("Backup failed - couldn't read settings file.");
                return;
            }


            DeleteOldBackups();
            BackupFiles();

        }

        private static bool ReadSettingsFile()
        {
            //settings file path
            var settingsFilePath = Path.Combine(settingsFile.InstallFolder, "SettingsFile.json");
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
                    Console.WriteLine("settings file couldn't be deserialized!");
                    return false;
                }
                catch(Exception e)
                {
                    Console.WriteLine("Couldn't read json file!");
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Failed to read settings file");
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
                Console.WriteLine($"Backing up {dir}");
                string roboCopyScirpt = GetRoboCopyScript(dir, settingsFile.BackupSaveDirectory);

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

        private static string GetRoboCopyScript(string dirToBackup, string BackupDir)
        {
            Console.WriteLine("Creating robocopy script...");
            var script = new StringBuilder();

            script.Append("/C robocopy ");
            script.Append("\"");
            script.Append(dirToBackup);
            script.Append("\" ");
            script.Append("\"");
            var savePath = Path.Combine(settingsFile.BackupSaveDirectory, settingsFile.BackupName, todaysDate);
            script.Append(savePath);
            script.Append("\" ");
            script.Append(@"/E /Z /FFT /R:3 /W:10 /MT");
            return script.ToString();
        }

        private static void DeleteOldBackups()
        {
            var dirs = Directory.GetDirectories(settingsFile.BackupSaveDirectory);

            var backupDirs = new List<DirectoryInfo>();
            
            //loop through all backups and check to see if they are old enough to be deleted
            foreach (var dir in dirs)
            {
                var dInfo = new DirectoryInfo(dir);
                if (dInfo.LastWriteTimeUtc < DateTime.UtcNow.AddDays(-Math.Abs(settingsFile.KeepBackupsFor)))
                {
                    try
                    {
                        Console.WriteLine($"Deleting {dInfo.FullName}");
                        dInfo.Delete(true);
                    }
                    catch
                    {
                        Console.WriteLine($"Failed to delete {dInfo.FullName}");
                    }
                }

                backupDirs.Add(dInfo);
            }

            //sort backup directories by date - oldest first
            backupDirs.OrderBy(d => d.LastWriteTimeUtc).ToList();
            //set max backup size in bytes
            var maxBackupSize = settingsFile.BackupSizeGB * (long)1073741824;
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
                        currentBackupSize -= currentDirSize;
                        if (currentBackupSize < maxBackupSize)
                        {
                            return; //folder is now small enough, so don't delete anything else.
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        return;
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
