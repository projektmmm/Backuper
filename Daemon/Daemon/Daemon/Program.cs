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
                    SourcePath = "[\"C:\\\\backupMMM\\\\Source\\\\DATA1\",\"C:\\\\backupMMM\\\\Source\\\\DATA2\",\"C:\\\\backupMMM\\\\Source\\\\DATA3\"]",
                    DestinationPath = "[\"C:\\\\backupMMM\\\\Destination\\\\DESTINATION1\",\"C:\\\\backupMMM\\\\Destination\\\\DESTINATION2\"]",
                    NextRun = DateTime.Now,               
                    Rar = false
                }
            };

            app.AnalyzeBackups();


            Console.ReadLine();
        }
    }
}
