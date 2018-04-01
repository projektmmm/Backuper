using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daemon
{
    public class NextRunSettings
    {
        public int Id { get; set; }
        public DateTime RunAt { get; set; }
        public string Cron { get; set; }
        public int DaemonId { get; set; }
        public string BackupType { get; set; }
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }


        public Settings OverrideSettings()
        {
            Settings s = new Settings();
            s.DestinationPath = this.DestinationPath;
            s.SourcePath = this.SourcePath;
            s.RunAt = this.RunAt;
            s.Cron = this.Cron;
            s.BackupType = this.BackupType;

            return s;
        }
    }
}
