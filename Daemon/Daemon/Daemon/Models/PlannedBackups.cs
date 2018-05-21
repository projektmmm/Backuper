using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daemon
{
    public class PlannedBackups
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime NextRun { get; set; }
        public string Cron { get; set; }
        public int DaemonId { get; set; }
        public string BackupType { get; set; }
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
        public bool Rar { get; set; }
        public bool Override { get; set; } = false;
    }
}
