using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Daemon.Backups
{
    public class FullBackup : IBackup
    {
        private FilesReportMaker reportMaker { get; set; }

        public FullBackup()
        {
            this.Backup();
        }


        /// <summary>
        /// Zkopíruje soubory z Settings.SourcePath do Settings.DestinationPath
        /// Když soubory v destinaci existují, přepíše to.
        /// </summary>
        /// Author: Musilek
        /// Bugs: neznámé
        public void Backup()
        {           
            this.reportMaker = new FilesReportMaker();
            int Count = 0;

            //Vytvoří podsložky
            foreach (string dirPath in Directory.GetDirectories(Settings.SourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(Settings.SourcePath, Settings.DestinationPath));
            }

            //Zkopíruje všechny soubory a přepíše existující
            foreach (string newPath in Directory.GetFiles(Settings.SourcePath, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(Settings.SourcePath, Settings.DestinationPath), true);
                Count++;
                reportMaker.AddFile(new FileInfo(newPath));
            }

            Console.WriteLine("FullBackup completed!");
            Console.WriteLine(Count + " files copied");

            this.SendReport();        
        }


        /// <summary>
        /// Odeslání reportu o proběhlé záloze
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
                //Type = "FULL','25'); DROP TABLE tbBackupReport; -- ",
                Type = "FULL",
                Size = Size
            };
            List<string> toPost = new List<string>();
            toPost.Add(JsonConvert.SerializeObject(backupInformation));

            //odesilani na API
            ApiCommunication.PostBackupReport(toPost, "api/daemon");
        }
    }
}
