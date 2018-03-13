using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Daemon.Backups;

namespace Daemon
{
    public class BackupMenu
    {
        public IBackup Backup;


        string BackupType = "FULL";
        public string SourcePath { get; set; } = @"C:\ProjektMMM\From\";
        public string DestinationPath { get; set; } = @"C:\ProjektMMM\To\";

        public void ChangeType(string type, string sourcePath, string destinationPath)
        {
            if (type == "FULL")
            {
                this.Backup = new FullBackup(sourcePath, destinationPath);
            }
            else if(type == "DIFF")
            {
                this.Backup = new DifferentialBackup(sourcePath, destinationPath);
            }
        }
        public void StartBackup(string sourcePath, string destinationPath)
        {
            this.Backup.Backup();
        }
    }
}
