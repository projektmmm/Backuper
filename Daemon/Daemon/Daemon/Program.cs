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
                    SourcePath = "[\"D:\\\\vojta\\\\Knihovny\\\\Hudba\\\\iTunes\",\"D:\\\\vojta\\\\Knihovny\\\\Obrázky\\\\lol\"]",
                    DestinationPath = "[\"C:\\\\BACKUP\\\\BACKUPI\",\"C:\\\\BACKUP\\\\BACKUPII\",\"C:\\\\BACKUP\\\\BACKUPIII\"]",
                    NextRun = DateTime.Now,               
                }
            };

            app.AnalyzeBackups();


            Console.ReadLine();
        }
    }
}
