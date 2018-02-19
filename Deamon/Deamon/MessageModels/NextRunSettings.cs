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


        public void OverrideSettings()
        {
            Settings.DestinationPath = this.DestinationPath;
            Settings.SourcePath = this.SourcePath;
            Settings.RunAt = this.RunAt;
        }
    }
}
