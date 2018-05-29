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
        {/*
            Application app = new Application();
            
            app.SetTimer();

            app.AnalyzeBackups();
            
            PlannedBackups backup = new PlannedBackups()
            {
                Id = 143,
                BackupType = "INCR",
                SourcePath = "[\"C:\\\\BACKUP\\\\BACKUPI\",\"C:\\\\BACKUP\\\\BACKUPII\",\"C:\\\\BACKUP\\\\BACKUPIII\"]",
                DestinationPath = "[\"C:\\\\BACKUP\\\\DESTINATIONI\",\"C:\\\\BACKUP\\\\DESTINATIONII\"]",
                NextRun = DateTime.Now,
                Override = true
            };

            TOHLE //app.SetTimer();
           // app.AnalyzeBackups();
            IncrementalBackup df = new IncrementalBackup(backup);
            */
            /*
            DaemonSettings.ftpSettings = new List<FtpSettings>()
            {
                new FtpSettings()
                {
                    Password = "deiTqR7EMZD5zy7M",
                    Username = "dlpuser@dlptest.com",
                    ServerAdress = "ftp://ftp.dlptest.com/"
                }
            };

            BackupOperations.Ftp("C:\\BACKUP\\BACKUPI");*/

            DaemonSettings.sshSettings = new List<SshSettings>()
            {
                new SshSettings()
                {
                    HostOrIp = "test.rebex.net",
                    Password = "password",
                    Username = "demo",
                    Port = 22
                }
            };
            Console.ReadLine();
        }
    }
}
