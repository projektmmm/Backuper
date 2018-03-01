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

        //Tuhle classu využijeme jako středobod backupů. Musíme vymyslet nějáký chytrý pusob jak je přepínat
        //FullBackup fb = new FullBackup();
        //DifferentialBackup df = new DifferentialBackup();


        int BackupType = 1; //1-Full,2=Diff
        public string SourcePath { get; set; } = @"C:\ProjektMMM\From\";
        public string DestinationPath { get; set; } = @"C:\ProjektMMM\To\";

        public void ChangeType(int type,string sorcePath,string destinationPath)
        {
            this.BackupType = type;
            this.SourcePath = sorcePath;
            this.DestinationPath = destinationPath;
        }
        public void StartBackup()
        {
            //Musíme vymyslet něco chytřejšího 
            if(this.BackupType == 1)
            {
                //this.fb.Backup(this.SourcePath,this.DestinationPath);
            }
            else if(this.BackupType == 2)
            {
                //this.df.Backup(this.SourcePath, this.DestinationPath);
            }
        }
    

    }
}
