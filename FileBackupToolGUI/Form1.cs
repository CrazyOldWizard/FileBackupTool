using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileBackupTool;
using Microsoft.Win32.TaskScheduler;
using Newtonsoft;

namespace FileBackupToolGUI
{
    public partial class Form1 : Form
    {
        private string settingsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SettingsFile.json");

        private Settings SettingsFile = new Settings();

        private long currentBackupSizeBytes = 0;
        public Form1()
        {
            if(File.Exists(settingsFilePath) == false)
            {
                var sFile = new Settings();
                sFile.InstallFolder = settingsFilePath;
                var serialize = Newtonsoft.Json.JsonConvert.SerializeObject(sFile, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(settingsFilePath, serialize);
            }
            else
            {
                var rawSettings = File.ReadAllText(settingsFilePath);
                var converted = Newtonsoft.Json.JsonConvert.DeserializeObject<Settings>(rawSettings);
                if(converted != null)
                {
                    SettingsFile = converted;
                }
            }
            InitializeComponent();
        }

        private void buttonSource_Click(object sender, EventArgs e)
        {
            var fd = new FolderBrowserDialog();
            fd.RootFolder = Environment.SpecialFolder.Desktop;
            fd.ShowNewFolderButton = true;
            var result = fd.ShowDialog();
            if (result == DialogResult.OK)
            {
                listBox_BackupDirs.Items.Add(fd.SelectedPath);
            }
        }

        private void buttonDestination_Click(object sender, EventArgs e)
        {
            var fd = new FolderBrowserDialog();
            fd.RootFolder = Environment.SpecialFolder.Desktop;
            fd.ShowNewFolderButton = true;
            var result = fd.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBoxDestinationPath.Text = fd.SelectedPath;
            }
            UpdateTotalBackupSizeLabel();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (SettingsFile.BackupSaveDirectory != string.Empty)
            {
                textBoxDestinationPath.Text = SettingsFile.BackupSaveDirectory;
            }
            foreach(var dir in SettingsFile.BackupDirectories)
            {
                listBox_BackupDirs.Items.Add(dir);
            }
            numericInterval.Value = SettingsFile.BackupInterval;
            numericKeepBackups.Value = SettingsFile.KeepBackupsFor;
            numericMaxSizeGB.Value = SettingsFile.BackupSizeGB;
            textBoxBackupName.Text = SettingsFile.BackupName;
            UpdateTotalBackupSizeLabel();
        }

        public void UpdateTotalBackupSizeLabel()
        {
            if (Directory.Exists(textBoxDestinationPath.Text)) // make this work faster over a network connection by making it it's own thread?
            {
                var dInfo = new DirectoryInfo(textBoxDestinationPath.Text);
                currentBackupSizeBytes = DirSize(dInfo);
                var gb = currentBackupSizeBytes / (long)1073741824;
                CurrentBackupSizeLabel.Text = "Total Backup Size: " + gb.ToString() + " GB";
            }
        }


        public long DirSize(DirectoryInfo d)
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

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SettingsFile.BackupSaveDirectory = textBoxDestinationPath.Text;
            var backupDirs = new List<string>();
            foreach (var item in listBox_BackupDirs.Items)
            {
                if(item.ToString() != string.Empty)
                    backupDirs.Add(item.ToString());
            }
            SettingsFile.BackupDirectories = backupDirs.ToArray();
            SettingsFile.KeepBackupsFor = decimal.ToInt32(numericKeepBackups.Value);
            SettingsFile.BackupSaveDirectory = textBoxDestinationPath.Text;
            SettingsFile.BackupInterval = decimal.ToInt32(numericInterval.Value);
            SettingsFile.BackupSizeGB = decimal.ToInt32(numericMaxSizeGB.Value);
            SettingsFile.BackupName = textBoxBackupName.Text;
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(SettingsFile, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(settingsFilePath, jsonString);
            CreateStartupTask();
            MessageBox.Show("Saved settings file.");
        }

        private Microsoft.Win32.TaskScheduler.Task CreateStartupTask()
        {
            string taskName = "FileBackupTool";
            var x = TaskService.Instance.FindTask(taskName, true);
            if (x != null)
            {
                return x;
            }
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FileBackupTool.exe");
            var t = TaskService.Instance.AddTask(taskName, QuickTriggerType.Logon, path);
            t.Definition.Principal.RunLevel = TaskRunLevel.Highest;
            t.RegisterChanges();
            return t;
        }

        private void buttonRemoveBackupItem_Click(object sender, EventArgs e)
        {
            var currentItem = listBox_BackupDirs.SelectedIndex;
            listBox_BackupDirs.Items.RemoveAt(currentItem);
            MessageBox.Show("Deleted Item");
        }

        private void openCurrentDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", AppDomain.CurrentDomain.BaseDirectory);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void backupFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(SettingsFile.BackupSaveDirectory))
            {
                Process.Start("explorer.exe", SettingsFile.BackupSaveDirectory);
            }
            else
                MessageBox.Show("Couldn't find backup folder");
        }

        private void startBackupToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stopBackupToolToolStripMenuItem_Click(null, null);
            var pinfo = new ProcessStartInfo();
            pinfo.Arguments = @"/C " + $"\"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FileBackupTool.exe")}\"";
            pinfo.FileName = "CMD.EXE";
            pinfo.UseShellExecute = false;
            pinfo.CreateNoWindow = true;
            Process.Start(pinfo);

        }

        private void stopBackupToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var processess = Process.GetProcessesByName("FileBackupTool");
            if (processess != null)
            {
                foreach (var p in processess)
                {
                    p.Kill();
                }
            }
            processess = Process.GetProcessesByName("Robocopy");
            if (processess != null)
            {
                foreach (var p in processess)
                {
                    p.Kill();
                }
            }
        }
    }
}
