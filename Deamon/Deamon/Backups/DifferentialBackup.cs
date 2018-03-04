using Daemon;
using Daemon.Backups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Daemon
{
    public class DifferentialBackup : IBackup
    {
        private FilesReportMaker reportMaker { get; set; }
        private string SourcePath { get; set; }
        private string FullBackupPath { get; set; }
        private string DestinationPath { get; set; }

        public DifferentialBackup(string sourcePath, string fullBackupPath)
        {
            this.reportMaker = new FilesReportMaker();
            this.SourcePath = sourcePath;
            this.FullBackupPath = fullBackupPath;
        }

        public void Backup()
        {
            int fileCount = 0;
            int directoryCount = 0;

            //Vytvoreni slozky pro svuj diff backup
            foreach (string dirPath in Directory.GetDirectories(this.FullBackupPath + "\\diff_backups"))
            {
                directoryCount++;
            }
            this.DestinationPath = this.FullBackupPath + "\\diff_backups\\" + ((directoryCount).ToString());
            Directory.CreateDirectory(this.DestinationPath);

            //Prekopirovani novych slozek
            foreach (string dirPath in Directory.GetDirectories(this.SourcePath, "*", SearchOption.AllDirectories))
            {
                if (Directory.GetLastWriteTime(dirPath) > this.GetOldest(this.FullBackupPath))
                {
                    Directory.CreateDirectory(dirPath.Replace(this.SourcePath, this.DestinationPath));
                }
            }

            List<LogModel> oldLog = new List<LogModel>();
            //Nacetni stareho logu
            using (StreamReader streamReader = new StreamReader(this.DestinationPath.Substring(0, Application.IdentifyCharIndex(this.DestinationPath, '\\')) + '\\' + (0).ToString() + "\\FileLog.log"))
            {
                string logtext = streamReader.ReadToEnd();
                oldLog = JsonConvert.DeserializeObject<List<LogModel>>(logtext);
            }

            int index;
            //Porovnavani logu s aktualni verzi vychozi slozky pro fullbackup
            foreach (string filePath in Directory.GetFiles(this.SourcePath, "*.*", SearchOption.AllDirectories).OrderBy(f => new FileInfo(f).FullName))
            {
                FileInfo fileInfo = new FileInfo(filePath);
                index = 0;

                foreach (LogModel item in oldLog)
                {
                    //nezmeneno - EXISTS
                    if (fileInfo.FullName == item.FilePath && Convert.ToDateTime(fileInfo.LastWriteTime) == item.LastWriteTime)
                    {
                        oldLog.RemoveAt(index);
                        break;
                    }
                    else if(fileInfo.FullName == item.FilePath && Convert.ToDateTime(fileInfo.LastWriteTime) != item.LastWriteTime)
                    {
                        if (Application.CountChar(fileInfo.FullName, '\\') - 1 > Application.CountChar(this.DestinationPath, '\\') - 2)
                        {
                            string directoryName = fileInfo.FullName.Replace(this.SourcePath, this.DestinationPath);
                            string newdirname = directoryName.Substring(0, Application.IdentifyLastCharIndex(directoryName, '\\'));
                            Directory.CreateDirectory(newdirname);
                        }

                        File.Copy(filePath, filePath.Replace(this.SourcePath, this.DestinationPath), true);
                        oldLog.RemoveAt(index);
                        reportMaker.AddFile(fileInfo);
                        fileCount++;
                        break;
                    }
                    index++;
                }

            }

            Console.WriteLine("DifferentialBackup completed!");
            Console.WriteLine(fileCount + " files copied");
            //this.SendReport();
        }

        /// <summary>
        /// Odeslaní údajů o proběhlém backupu
        /// </summary>
        public void SendReport()
        {
            int Size = 0;
            foreach (FileInformation item in reportMaker.GetReport())
            {
                Size += Convert.ToInt32(item.Size);
            }

            BackupInformation backupInformation = new BackupInformation()
            {
                Date = DateTime.Now,
                Type = "DIFFERENCIAL",
                Size = Size
            };
            List<string> toPost = new List<string>();
            toPost.Add(JsonConvert.SerializeObject(backupInformation));

            //odesilani na API
            ApiCommunication.PostBackupReport(toPost, "api/daemon");
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
