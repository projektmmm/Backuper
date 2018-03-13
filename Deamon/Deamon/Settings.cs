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
        public string SourcePath { get; set; } = @"C:\ProjektMMM\From\";
        public string DestinationPath { get; set; } = @"C:\ProjektMMM\To\";

        //upgrade pro timer
        public int AskInterval { get; set; } = 1 * 60 * 1000;
        //Upgrade pro novou verzi
        public int BackupType { get; set; } = 1;
        //rozdělávač cronu
        public void UnparseCron()
        {
            string _cron = Cron;
            int index = _cron.IndexOf('/');

            if (index == 0)
            {
                AskInterval = _cron[index + 1] * 60 * 1000;
            }
            else if (index == 2)
            {
                AskInterval = _cron[index + 1] * 60 * 60 * 1000;
            }
            else if (index == 4)
            {
                AskInterval = _cron[index + 1] * 60 * 60 * 24 * 1000;
            }
            else
            {
                
            }
        }
    }
}
