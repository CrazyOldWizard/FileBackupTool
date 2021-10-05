
namespace FileBackupToolGUI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBox_BackupDirs = new System.Windows.Forms.ListBox();
            this.numericMaxSizeGB = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxBackupName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numericInterval = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numericKeepBackups = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxSourcePath = new System.Windows.Forms.TextBox();
            this.textBoxDestinationPath = new System.Windows.Forms.TextBox();
            this.buttonSource = new System.Windows.Forms.Button();
            this.buttonDestination = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.CurrentBackupSizeLabel = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonRemoveBackupItem = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openCurrentDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backupFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startBackupToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopBackupToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.numericMaxSizeGB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericKeepBackups)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox_BackupDirs
            // 
            this.listBox_BackupDirs.FormattingEnabled = true;
            this.listBox_BackupDirs.Location = new System.Drawing.Point(12, 234);
            this.listBox_BackupDirs.Name = "listBox_BackupDirs";
            this.listBox_BackupDirs.Size = new System.Drawing.Size(793, 186);
            this.listBox_BackupDirs.TabIndex = 0;
            this.listBox_BackupDirs.TabStop = false;
            this.listBox_BackupDirs.UseTabStops = false;
            // 
            // numericMaxSizeGB
            // 
            this.numericMaxSizeGB.Location = new System.Drawing.Point(131, 52);
            this.numericMaxSizeGB.Name = "numericMaxSizeGB";
            this.numericMaxSizeGB.Size = new System.Drawing.Size(62, 20);
            this.numericMaxSizeGB.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(128, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Max Backup Size GB";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Backup Name";
            // 
            // textBoxBackupName
            // 
            this.textBoxBackupName.Location = new System.Drawing.Point(12, 52);
            this.textBoxBackupName.Name = "textBoxBackupName";
            this.textBoxBackupName.Size = new System.Drawing.Size(100, 20);
            this.textBoxBackupName.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(242, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Interval (Minutes)";
            // 
            // numericInterval
            // 
            this.numericInterval.Location = new System.Drawing.Point(245, 53);
            this.numericInterval.Name = "numericInterval";
            this.numericInterval.Size = new System.Drawing.Size(62, 20);
            this.numericInterval.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(336, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Keep Backups For (Days)";
            // 
            // numericKeepBackups
            // 
            this.numericKeepBackups.Location = new System.Drawing.Point(339, 53);
            this.numericKeepBackups.Name = "numericKeepBackups";
            this.numericKeepBackups.Size = new System.Drawing.Size(62, 20);
            this.numericKeepBackups.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 218);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Backup Folders";
            // 
            // textBoxSourcePath
            // 
            this.textBoxSourcePath.AllowDrop = true;
            this.textBoxSourcePath.Location = new System.Drawing.Point(12, 101);
            this.textBoxSourcePath.Name = "textBoxSourcePath";
            this.textBoxSourcePath.ReadOnly = true;
            this.textBoxSourcePath.Size = new System.Drawing.Size(705, 20);
            this.textBoxSourcePath.TabIndex = 11;
            // 
            // textBoxDestinationPath
            // 
            this.textBoxDestinationPath.Location = new System.Drawing.Point(12, 149);
            this.textBoxDestinationPath.Name = "textBoxDestinationPath";
            this.textBoxDestinationPath.ReadOnly = true;
            this.textBoxDestinationPath.Size = new System.Drawing.Size(705, 20);
            this.textBoxDestinationPath.TabIndex = 12;
            // 
            // buttonSource
            // 
            this.buttonSource.Location = new System.Drawing.Point(730, 99);
            this.buttonSource.Name = "buttonSource";
            this.buttonSource.Size = new System.Drawing.Size(75, 23);
            this.buttonSource.TabIndex = 13;
            this.buttonSource.Text = "Browse";
            this.buttonSource.UseVisualStyleBackColor = true;
            this.buttonSource.Click += new System.EventHandler(this.buttonSource_Click);
            // 
            // buttonDestination
            // 
            this.buttonDestination.Location = new System.Drawing.Point(730, 147);
            this.buttonDestination.Name = "buttonDestination";
            this.buttonDestination.Size = new System.Drawing.Size(75, 23);
            this.buttonDestination.TabIndex = 14;
            this.buttonDestination.Text = "Browse";
            this.buttonDestination.UseVisualStyleBackColor = true;
            this.buttonDestination.Click += new System.EventHandler(this.buttonDestination_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Folder to Backup";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 133);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(102, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Backup Root Folder";
            // 
            // CurrentBackupSizeLabel
            // 
            this.CurrentBackupSizeLabel.AutoSize = true;
            this.CurrentBackupSizeLabel.Location = new System.Drawing.Point(13, 427);
            this.CurrentBackupSizeLabel.Name = "CurrentBackupSizeLabel";
            this.CurrentBackupSizeLabel.Size = new System.Drawing.Size(97, 13);
            this.CurrentBackupSizeLabel.TabIndex = 17;
            this.CurrentBackupSizeLabel.Text = "Total Backup Size:";
            // 
            // buttonSave
            // 
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSave.Location = new System.Drawing.Point(682, 195);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(123, 33);
            this.buttonSave.TabIndex = 18;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonRemoveBackupItem
            // 
            this.buttonRemoveBackupItem.Location = new System.Drawing.Point(682, 422);
            this.buttonRemoveBackupItem.Name = "buttonRemoveBackupItem";
            this.buttonRemoveBackupItem.Size = new System.Drawing.Size(123, 23);
            this.buttonRemoveBackupItem.TabIndex = 19;
            this.buttonRemoveBackupItem.Text = "Remove Backup Item";
            this.buttonRemoveBackupItem.UseVisualStyleBackColor = true;
            this.buttonRemoveBackupItem.Click += new System.EventHandler(this.buttonRemoveBackupItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.backupToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(817, 24);
            this.menuStrip1.TabIndex = 20;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openCurrentDirectoryToolStripMenuItem,
            this.backupFolderToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openCurrentDirectoryToolStripMenuItem
            // 
            this.openCurrentDirectoryToolStripMenuItem.Name = "openCurrentDirectoryToolStripMenuItem";
            this.openCurrentDirectoryToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.openCurrentDirectoryToolStripMenuItem.Text = "Open Current Directory";
            this.openCurrentDirectoryToolStripMenuItem.Click += new System.EventHandler(this.openCurrentDirectoryToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // backupFolderToolStripMenuItem
            // 
            this.backupFolderToolStripMenuItem.Name = "backupFolderToolStripMenuItem";
            this.backupFolderToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.backupFolderToolStripMenuItem.Text = "Open Backup Folder";
            this.backupFolderToolStripMenuItem.Click += new System.EventHandler(this.backupFolderToolStripMenuItem_Click);
            // 
            // backupToolStripMenuItem
            // 
            this.backupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startBackupToolToolStripMenuItem,
            this.stopBackupToolToolStripMenuItem});
            this.backupToolStripMenuItem.Name = "backupToolStripMenuItem";
            this.backupToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.backupToolStripMenuItem.Text = "Backup";
            // 
            // startBackupToolToolStripMenuItem
            // 
            this.startBackupToolToolStripMenuItem.Name = "startBackupToolToolStripMenuItem";
            this.startBackupToolToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.startBackupToolToolStripMenuItem.Text = "Start Backup Tool";
            this.startBackupToolToolStripMenuItem.Click += new System.EventHandler(this.startBackupToolToolStripMenuItem_Click);
            // 
            // stopBackupToolToolStripMenuItem
            // 
            this.stopBackupToolToolStripMenuItem.Name = "stopBackupToolToolStripMenuItem";
            this.stopBackupToolToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.stopBackupToolToolStripMenuItem.Text = "Stop Backup Tool";
            this.stopBackupToolToolStripMenuItem.Click += new System.EventHandler(this.stopBackupToolToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(817, 450);
            this.Controls.Add(this.buttonRemoveBackupItem);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.CurrentBackupSizeLabel);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.buttonDestination);
            this.Controls.Add(this.buttonSource);
            this.Controls.Add(this.textBoxDestinationPath);
            this.Controls.Add(this.textBoxSourcePath);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericKeepBackups);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericInterval);
            this.Controls.Add(this.textBoxBackupName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericMaxSizeGB);
            this.Controls.Add(this.listBox_BackupDirs);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(833, 489);
            this.MinimumSize = new System.Drawing.Size(833, 489);
            this.Name = "Form1";
            this.Text = "File Backup Tool";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericMaxSizeGB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericKeepBackups)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox_BackupDirs;
        private System.Windows.Forms.NumericUpDown numericMaxSizeGB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxBackupName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericInterval;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericKeepBackups;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxSourcePath;
        private System.Windows.Forms.TextBox textBoxDestinationPath;
        private System.Windows.Forms.Button buttonSource;
        private System.Windows.Forms.Button buttonDestination;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label CurrentBackupSizeLabel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonRemoveBackupItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openCurrentDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backupFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startBackupToolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopBackupToolToolStripMenuItem;
    }
}

