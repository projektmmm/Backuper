using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Daemon.Backups;
using Deamon;

namespace Deamon.Backups
{
    public class BackupMenu
    {
        public IBackup Back;

        public void ChangeBackupSettings(int type)
        {
            if (type ==1)
            {
                this.Back = new FullBackup();
            }
            else if(type ==2)
            {
                this.Back = new DifferentialBackup();
            }
        }
        public void StartBackup(string sourcePath, string destinationPath)
        {
            this.Back.Backup(sourcePath,destinationPath);
        }
    

    }
}
