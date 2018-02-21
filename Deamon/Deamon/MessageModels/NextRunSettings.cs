using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daemon
{
    public class NextRunSettings
    {

        // bude potřebovat debug z důvodů nestatický settings
        // Osobní návrh: Zrušil bych tuhle classu úplně a prostě bych do timeru dal queue<settings> který by se následně to vyvolali Dequeue()
        public DateTime RunAt { get; set; }
        public int BackupType { get; set; }
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }


        public void OverrideSettings()
        {
            //Settings.DestinationPath = this.DestinationPath;
            //Settings.SourcePath = this.SourcePath;
            //Settings.RunAt = this.RunAt;
        }
    }
}
