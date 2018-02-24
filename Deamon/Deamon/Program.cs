using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Daemon.Backups;
using Deamon;

namespace Daemon
{
    class Program
    {
        static void Main(string[] args)
        {
            Timer timer = new Timer();
            timer.Start();
            //ApiCommunication.GetNextRunSetting("api/daemon");
            ////Backuper b = new Backuper();
            ////FullBackup fb = new FullBackup();
            //DifferentialBackup df = new DifferentialBackup();
            ////b.FullBackup();

            ////b.DifferentialBackup();

            //Console.ReadLine();
        }
    }
}
