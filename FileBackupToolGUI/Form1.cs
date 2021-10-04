using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileBackupTool;
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

        }

        private void buttonDestination_Click(object sender, EventArgs e)
        {

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
            if(Directory.Exists(textBoxDestinationPath.Text))
            {
                var dInfo = new DirectoryInfo(textBoxDestinationPath.Text);
                currentBackupSizeBytes = DirSize(dInfo);
                var gb = currentBackupSizeBytes * (long)1073741824;
                CurrentBackupSizeLabel.Text = gb.ToString() + " GB";
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
                backupDirs.Add(item.ToString()); //not sure if this works
            }
            SettingsFile.BackupDirectories = backupDirs.ToArray();
            SettingsFile.KeepBackupsFor = decimal.ToInt32(numericKeepBackups.Value);
            SettingsFile.BackupSaveDirectory = textBoxDestinationPath.Text;
            SettingsFile.BackupInterval = decimal.ToInt32(numericInterval.Value);
            SettingsFile.BackupSizeGB = decimal.ToInt32(numericMaxSizeGB.Value);
            SettingsFile.BackupName = textBoxBackupName.Text;
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(SettingsFile);
            File.WriteAllText(settingsFilePath, jsonString);
            MessageBox.Show("Saved settings file.");
        }
    }
}
