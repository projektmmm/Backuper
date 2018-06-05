using DaemonBackuper.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daemon
{
    public static class DaemonSettings
    {
        public static int Id = Convert.ToInt32(string.Join(" ", File.ReadAllLines(@"C:\Settings\DaemonSettings.txt")));
        //public static int UserId = 1;
        //public static string Name = "PC01";
        public static string ApiAdress = "http://localhost:63324/";

        public static int AskInterval = 300000;

        public static List<PlannedBackups> plannedBackups;
        public static List<FtpSettings> ftpSettings;
        public static List<SshSettings> sshSettings;
        public static List<BatchesSettings> batchesSettings;
<<<<<<< HEAD
        public static List<Databases> ListDatabases;
=======
>>>>>>> 4ec0bac1a3e487221c71023e912948ae9952ed08
    }
}
