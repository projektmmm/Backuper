using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Daemon.Backups;
using Deamon;

namespace Daemon
{
    public class BackupMenu
    {
        public IBackup Back;

<<<<<<< HEAD
        public void ChangeBackupSettings(int type)
=======
        //Tuhle classu využijeme jako středobod backupů. Musíme vymyslet nějáký chytrý pusob jak je přepínat
        //FullBackup fb = new FullBackup();
        //DifferentialBackup df = new DifferentialBackup();


        int BackupType = 1; //1-Full,2=Diff
        public string SourcePath { get; set; } = @"C:\ProjektMMM\From\";
        public string DestinationPath { get; set; } = @"C:\ProjektMMM\To\";

        public void ChangeType(int type,string sorcePath,string destinationPath)
>>>>>>> 75d14c7ecd5aa9a38f65215afa279373b46a1a8f
        {
            if (type ==1)
            {
<<<<<<< HEAD
                this.Back = new FullBackup();
=======
                //this.fb.Backup(this.SourcePath,this.DestinationPath);
>>>>>>> 75d14c7ecd5aa9a38f65215afa279373b46a1a8f
            }
            else if(type ==2)
            {
<<<<<<< HEAD
                this.Back = new DifferentialBackup();
=======
                //this.df.Backup(this.SourcePath, this.DestinationPath);
>>>>>>> 75d14c7ecd5aa9a38f65215afa279373b46a1a8f
            }
        }
        public void StartBackup(string sourcePath, string destinationPath)
        {
            this.Back.Backup(sourcePath,destinationPath);
        }
    

    }
}
