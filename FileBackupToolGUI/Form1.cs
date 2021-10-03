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
    }
}
