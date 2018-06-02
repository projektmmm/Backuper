using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daemon
{
    public static class DaemonSettings
    {
        public static int Id = 31;
        //public static int UserId = 1;
        public static string Name = "PC01";
        public static string ApiAdress = "http://localhost:63324/";

        public static int AskInterval = 120000;

        public static List<PlannedBackups> plannedBackups;
        public static List<FtpSettings> ftpSettings;
        public static List<SshSettings> sshSettings;
    }
}
