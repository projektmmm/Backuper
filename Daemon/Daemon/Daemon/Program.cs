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

            DaemonSettings.plannedBackups = new List<PlannedBackups>()
            {
                new PlannedBackups()
                {
                    BackupType = "FULL",
                    SourcePath = "[\"C:\\\\BACKUP\\\\BACKUPI\",\"C:\\\\BACKUP\\\\BACKUPII\",\"C:\\\\BACKUP\\\\BACKUPIII\"]",
                    DestinationPath = "[\"C:\\\\BACKUP\\\\DESTINATIONI\",\"C:\\\\BACKUP\\\\DESTINATIONII\"]",
                    NextRun = DateTime.Now,               
                    Rar = false
                }
            };

            app.AnalyzeBackups();


            Console.ReadLine();
        }
    }
}
