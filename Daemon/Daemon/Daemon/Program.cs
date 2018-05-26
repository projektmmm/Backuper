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
                BackupType = "INCR",
                SourcePath = "[\"C:\\\\BACKUP\\\\BACKUPI\",\"C:\\\\BACKUP\\\\BACKUPII\",\"C:\\\\BACKUP\\\\BACKUPIII\"]",
                DestinationPath = "[\"C:\\\\BACKUP\\\\DESTINATIONI\",\"C:\\\\BACKUP\\\\DESTINATIONII\"]",
                NextRun = DateTime.Now,
                Override = true
            };

            //app.SetTimer();
           // app.AnalyzeBackups();
            IncrementalBackup df = new IncrementalBackup(backup);
            

            Console.ReadLine();
        }
    }
}
