using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daemon
{
    class Program
    {
        static void Main(string[] args)
        {
            Application app = new Application();
            /*
            app.SetTimer();

            app.AnalyzeBackups();
            */
            PlannedBackups backup = new PlannedBackups()
            {
                Id = 143,
                BackupType = "DIFF",
                SourcePath = "[\"C:\\\\BACKUP\\\\BACKUPI\",\"C:\\\\BACKUP\\\\BACKUPII\",\"C:\\\\BACKUP\\\\BACKUPIII\"]",
                DestinationPath = "[\"C:\\\\BACKUP\\\\DESTINATIONI\",\"C:\\\\BACKUP\\\\DESTINATIONII\"]",
                NextRun = DateTime.Now,
            };

            FullBackup df = new FullBackup(backup);
            df.Start();

            Console.ReadLine();
        }
    }
}
