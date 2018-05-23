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
            //Zmena destinationPath I
            for (int i = 0; i < destinationPaths.Count; i++)
            {
                destinationPaths[i] += $"\\{sourcePath.Substring(Functionality.IdentifyCharPosition('\\', sourcePath))}";
            }

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
                try
                {
                    Directory.CreateDirectory(sourcePath += "\\incr_backup_0");
                }
                catch (Exception ex)
                {
                    this.reportMaker.AddError(new ErrorDetails()
                    {
                        Exception = ex.Message,
                        Path = sourcePath += "\\incr_backup_0",
                        Problem = "Can't create the folder for incremental backup. Backup hasn't been done."
                    });
                    return;
                }
            }
            else
            {
                try
                {
                    Directory.CreateDirectory(sourcePath += $"\\incr_backup_{sourceFolder.Length}");
                }
                catch (Exception ex)
                {
                    this.reportMaker.AddError(new ErrorDetails()
                    {
                        this.reportMaker.AddError(new ErrorDetails()
                    {
                        Exception = ex.Message,
                        Path = sourcePath += "\\incr_backup_0",
                        Problem = "Can't create the folder for incremental backup. Backup hasn't been done."
                    });
                })
                }
            }
        }
    }
}
