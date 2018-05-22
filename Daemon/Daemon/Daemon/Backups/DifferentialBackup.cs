using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Daemon
{
    public class DifferentialBackup : ABackup
    {
        public DifferentialBackup(PlannedBackups item) : base(item)
        {

        }

        protected override void Backup(string sourcePath, List<string> destinationPaths)
        {
            //Nacteni FULL backup logu
            List<LogModel> oldLog = new List<LogModel>();
            //Nacetni stareho logu
            try
            {
                using (StreamReader streamReader = new StreamReader(sourcePath + "\\BackupsLog.log", false))
                {
                    string logtext = streamReader.ReadToEnd();
                    oldLog = JsonConvert.DeserializeObject<List<LogModel>>(logtext);
                }
            }
            catch (Exception ex)
            {
                this.reportMaker.AddError(new ErrorDetails()
                {
                    Exception = ex.Message,
                    Problem = "The program didn't find the full backup log. DIFF backup for this source folder hadn't been done.",
                    Path = sourcePath
                });
                return;
            }

            //Prespsani destination path
            for (int i = 0; i < destinationPaths.Count; i++)
            {
                destinationPaths[i] += $"\\{sourcePath.Substring(Functionality.IdentifyCharPosition('\\', sourcePath))}";
                int count = 0;
                foreach (string item in Directory.GetDirectories(destinationPaths[i], "*", SearchOption.TopDirectoryOnly).Where(s => s.Contains("diff_backup")))
                {
                    if (item.Contains("diff_backup_"))
                        count++;
                }
                destinationPaths[i] += $"\\diff_backup_{count}\\{sourcePath.Substring(Functionality.IdentifyCharPosition('\\', sourcePath))}";
                Directory.CreateDirectory(destinationPaths[i]);
            }

            int index;
            //Porovnavani logu s aktualni verzi vychozi slozky pro fullbackup
            foreach (string filePath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories).OrderBy(f => new FileInfo(f).FullName))
            {
                FileInfo fileInfo = new FileInfo(filePath);

                foreach (LogModel item in oldLog)
                {
                    if (item.FilePath != new FileInfo(filePath).FullName)
                        continue;
                    //nezmeneno - EXISTS
                    if (fileInfo.FullName == item.FilePath && Convert.ToDateTime(fileInfo.LastWriteTime) == item.LastWriteTime)
                    {
                        break;
                    }
                    //zmeneno
                    else
                    {
                        foreach (string destinationPath in destinationPaths)
                        {
                            try
                            {
                                File.Copy(filePath, destinationPath + $"\\{fileInfo.Name}");
                            }
                            catch (Exception ex)
                            {
                                this.reportMaker.AddError(new ErrorDetails()
                                {
                                    Exception = ex.Message,
                                    Problem = "Backupper was unable to copy a file.",
                                    Path = sourcePath
                                });
                            }
                        }

                        reportMaker.AddFile(fileInfo);
                        break;
                    }
                }

            }

            this.backupCount++;
        }
        /// <summary>
        /// Porovná soubory a zjistí, jestli je jeden z nich starší
        /// </summary>
        private DateTime GetOldest(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            DateTime oldest = di.GetFiles()[0].LastWriteTime;
            foreach (string f in Directory.GetFiles(path, "*", SearchOption.AllDirectories))
            {
                if (oldest < File.GetLastWriteTime(f))
                {
                    oldest = File.GetLastWriteTime(f);
                }
            }
            foreach (string f in Directory.GetDirectories(path, "*", SearchOption.AllDirectories))
            {
                if (oldest < Directory.GetLastWriteTime(f))
                {
                    oldest = Directory.GetLastWriteTime(f);
                }
            }

            return oldest;
        }
    }
}