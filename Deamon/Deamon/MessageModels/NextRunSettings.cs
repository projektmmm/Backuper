using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daemon
{
    public class NextRunSettings
    {
        public DateTime RunAt { get; set; }
        public int BackupType { get; set; }
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }


        public Settings OverrideSettings()
        {
            Settings s = new Settings();
            s.DestinationPath = this.DestinationPath;
            s.SourcePath = this.SourcePath;
            s.RunAt = this.RunAt;
            s.BackupType = this.BackupType;

            return s;
        }
    }
}
