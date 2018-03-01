using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daemon
{
    public  class Settings
    { 
        
        public string DaemonId { get; private set; }
        public DateTime RunAt { get; set; }
        public string SourcePath { get; set; } = @"C:\ProjektMMM\From\";
        public string DestinationPath { get; set; } = @"C:\ProjektMMM\To\";

        //upgrade pro timer
        public int AskInterval { get; set; } = 10 * 60 * 1000; //10 min
        //Upgrade pro novou verzi
        public int BackupType { get; set; } = 1;
    }
}
