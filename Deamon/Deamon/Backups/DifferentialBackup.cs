using Daemon;
using Daemon.Backups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Deamon.Backups
{
    public class DifferentialBackup : IBackup
    {
        private FilesReportMaker reportMaker { get; set; }

        public DifferentialBackup()
        {
            this.reportMaker = new FilesReportMaker();
            //this.Backup();
        }

        /// <summary>
        /// Diferenciální backup. Porovná výchozí složku s destinací a překopíruje soubory, co chybí
        /// </summary>
        /// <ToDo>
        /// Možnost nakopírovat to do úplně nové složky. Vymazat složky a soubory, co už tam nejsou
        /// </ToDo>
        public void Backup(string SourcePath,string DestinationPath)
        {
            int Count = 0;
            foreach (string dirPath in Directory.GetDirectories(SourcePath, "*", SearchOption.AllDirectories))
            {
                if (Directory.GetLastWriteTime(dirPath) > this.GetOldest(DestinationPath))
                {
                    Directory.CreateDirectory(dirPath.Replace(SourcePath, DestinationPath));
                }
            }

            foreach (string filePath in Directory.GetFiles(SourcePath, "*.*", SearchOption.AllDirectories))
            {
                if (File.GetLastWriteTime(filePath) > this.GetOldest(DestinationPath))
                {
                    File.Copy(filePath, filePath.Replace(SourcePath, DestinationPath), true);
                    Count++;
                    reportMaker.AddFile(new FileInfo(filePath));
                }
            }

            Console.WriteLine("DifferentialBackup completed!");
            Console.WriteLine(Count + " files copied");
            this.SendReport();
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
