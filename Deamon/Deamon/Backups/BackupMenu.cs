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

    
        int BackupType = 1; //1-Full,2=Diff
        public string SourcePath { get; set; } = @"C:\ProjektMMM\From\";
        public string DestinationPath { get; set; } = @"C:\ProjektMMM\To\";

        public void ChangeType(int type, string sourcePath, string destinationPath)
        {
            if (type == 1)
            {
                this.Backup = new FullBackup(sourcePath, destinationPath);
            }
            else if(type == 2)
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
