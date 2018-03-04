using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin
{
    public class CommandInformation
    {
        public int DaemonId { get; set; }
        public DateTime RunAt { get; set; }
        public string BackupType { get; set; }
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
        public string Cron { get; set; }
    }
}
