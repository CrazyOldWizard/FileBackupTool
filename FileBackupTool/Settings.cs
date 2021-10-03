using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileBackupTool
{
    public class Settings
    {
        public string InstallFolder { get; set; } = Path.Combine
            (Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FileBackupTool");
        public string[] BackupDirectories { get; set; } = new string[] { "" };
        public string BackupSaveDirectory { get; set; } = "";
        public string BackupName { get; set; } = "Backup";
        public int BackupInterval { get; set; } = 60; //minutes
        public int BackupSizeGB { get; set; } = 50; //size in GB
        public int KeepBackupsFor { get; set; } = 30; //days

    }
}
