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
        private string SourcePath { get; set; }
        private string DestinationPath { get; set; }

        public FullBackup(string sourcePath, string destinationPath)
        {
            this.SourcePath = sourcePath;
            this.DestinationPath = destinationPath;
        }


        /// <summary>
        /// Zkopíruje soubory z Settings.SourcePath do Settings.DestinationPath
        /// Když soubory v destinaci existují, přepíše to.
        /// </summary>
        /// ToDo: Udelat lepsi pojmenovavani slozky pro diff backupy
        public void Backup()
        {           
            this.reportMaker = new FilesReportMaker();
            int Count = 0;

            //Vytvori slozky pro diferencialni backupy
            DirectoryInfo directoryInfo = Directory.CreateDirectory(this.DestinationPath + "\\diff_backups");
            directoryInfo.Attributes = FileAttributes.Hidden;
            Directory.CreateDirectory(this.DestinationPath + "\\diff_backups\\0");

            //Vytvoří podsložky
            foreach (string dirPath in Directory.GetDirectories(this.SourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(this.SourcePath, this.DestinationPath));
            }

            //Zkopíruje všechny soubory a přepíše existující, zanese o nich zaznamy do logu
            List<LogModel> newLog = new List<LogModel>();
            foreach (string newPath in Directory.GetFiles(this.SourcePath, "*.*", SearchOption.AllDirectories).OrderBy(f => new FileInfo(f).FullName))
            {
                File.Copy(newPath, newPath.Replace(this.SourcePath, this.DestinationPath), true);
                Count++;
                FileInfo fileInfo = new FileInfo(newPath);
                newLog.Add(new LogModel() { Action = "EXI", FileName = fileInfo.Name, FilePath = fileInfo.FullName, LastWriteTime = Convert.ToDateTime(fileInfo.LastWriteTime)});
                reportMaker.AddFile(new FileInfo(newPath));
            }

            //Zapsani logu do souboru souboru
            using (StreamWriter streamWriter = new StreamWriter(this.DestinationPath + "\\diff_backups\\0\\FileLog.log", false))
            {
                string toWrite = JsonConvert.SerializeObject(newLog);
                streamWriter.Write(toWrite);
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

            BackupReport backupInformation = new BackupReport()
            {
                Date = DateTime.Now,
                //Type = "FULL','25'); DROP TABLE tbBackupReport; -- ",
                Type = "FULL",
                Size = Size
            };
            //List<string> toPost = new List<string>();
            //toPost.Add(JsonConvert.SerializeObject(backupInformation));

            //odesilani na API
            ApiCommunication.PostBackupReport(backupInformation, "api/daemon");
        }
    }
}
