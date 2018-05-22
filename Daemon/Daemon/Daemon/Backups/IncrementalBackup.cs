using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Daemon
{
    public class IncrementalBackup : ABackup
    {
        public IncrementalBackup(PlannedBackups item) : base(item)
        {
            
        }

        protected override void Backup(string sourcePath, List<string> destinationPaths)
        {
            //Nacteni posledniho logu
            List<LogModel> oldLog = new List<LogModel>();

            int cnt = 0;
            string[] sourceFolder = Directory.GetDirectories(sourcePath, "*", SearchOption.TopDirectoryOnly);
            foreach (string item in sourceFolder)
            {
                if (item.Contains("incr_backup"))
                    cnt++;
            }
            
            if (cnt == 0)
            {
                
            }
        }
    }
}
