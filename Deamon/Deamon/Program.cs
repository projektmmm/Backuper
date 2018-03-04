using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Daemon.Backups;

namespace Daemon
{
    class Program
    {
        static void Main(string[] args)
        {
            //Timer timer = new Timer();
            //timer.Start();

            //FullBackup fb = new FullBackup(@"C:\ProjektMMM\From", @"C:\ProjektMMM\To");
            //fb.Backup();

          
            //DifferentialBackup df = new DifferentialBackup(@"C:\ProjektMMM\From", @"C:\ProjektMMM\To");
            //df.Backup();

            IncrementalBackup ib = new IncrementalBackup(@"C:\ProjektMMM\From", @"C:\ProjektMMM\To");
            ib.Backup();


            //ApiCommunication.GetNextRunSetting("api /daemon");
            ////Backuper b = new Backuper();
            ////FullBackup fb = new FullBackup();
            //DifferentialBackup df = new DifferentialBackup();
            ////b.FullBackup();

            ////b.DifferentialBackup();

            Console.ReadLine();
        }
    }
}
