using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daemon
{
    public class Settings
    {
        public string DaemonId { get; private set; }
        public DateTime RunAt { get; set; }
        public string Cron { get; set; }
        public string BackupType { get; set; } = "FULL";
        public string SourcePath { get; set; } = @"C:\ProjektMMM\From\";
        public string DestinationPath { get; set; } = @"C:\ProjektMMM\To\";

        public int AskInterval { get; set; } = 50000;
    }
}
