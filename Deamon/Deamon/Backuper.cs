using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Daemon
{
    public class Backuper
    {
        private FilesReportMaker reportMaker = new FilesReportMaker();
        private string FullBack = @"C:\ProjektMMM\To\FullBackup-1-\";
        private int FullCount;
        private string DiffBack;
        private List<string> InkrBack = new List<string>();
        public Settings Settings = new Settings();

        public DateTime GetOldest(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            DateTime oldest = di.GetFiles()[0].LastWriteTime;
            foreach (string f in Directory.GetFiles(path,"*",SearchOption.AllDirectories))
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

        
        /// <summary>
        /// Zkopíruje soubory z Settings.SourcePath do Settings.DestinationPath
        /// Když soubory v destinaci existují, overwritne je to
        /// </summary>
        /// <author>
        /// Musílek
        /// </author>
        /// Bugs: neznámé
        public void FullBackup()
        {
            this.reportMaker = new FilesReportMaker();
            this.DiffBack = null;
            this.InkrBack = new List<string>();
            int Count = 0;
            this.FullCount++;

            FileInfo[] RootFiles = new DirectoryInfo(Settings.SourcePath).GetFiles();
            foreach (FileInfo item in RootFiles)
            {
                reportMaker.AddFile(item);
            }

            //Vytvoří podsložky
            foreach (string dirPath in Directory.GetDirectories(Settings.SourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(Settings.SourcePath, Settings.DestinationPath));
                FileInfo[] Files = new DirectoryInfo(dirPath).GetFiles();

                //Informace o jednotlivych souborech pujdou do databaze
                foreach (FileInfo item in Files)
                {
                    reportMaker.AddFile(item);
                }
            }

            //Zkopíruje všechny soubory a přepíše existující
            foreach (string newPath in Directory.GetFiles(Settings.SourcePath, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(Settings.SourcePath, Settings.DestinationPath), true);
                Count++;
            }

            this.FullBack = Settings.DestinationPath + @"\FullBackup-" + this.FullCount + @"-\";
            Console.WriteLine("FullBackup completed!");
            Console.WriteLine(Count + " files copied");

            //Info o Backupu
            int Size = 0;
            foreach (FileInformation item in reportMaker.GetReport())
            {
                Size += Convert.ToInt32(item.Size);
            }

            BackupInformation backupInformation = new BackupInformation()
            {
                Date = DateTime.Now,
                Type = "FULL",
                Size = Size
            };
            List<string> toPost = new List<string>();
            toPost.Add(JsonConvert.SerializeObject(reportMaker.GetReport()));
            toPost.Add(JsonConvert.SerializeObject(backupInformation));

            //odesilani na API
            ApiCommunication.PostBackupReport(toPost, "api/daemon");
        }

        /// <summary>
        /// Jde o zálohu totožnou s fullem až na podmínku uvnitř foreachů
        /// </summary>
        /// Macek
        /// Bugs:Neznámé
        public void DifferentialBackup()
        {
            this.InkrBack = new List<string>();
            int Count = 0;
            
            if (this.FullBack!=null)
            {
                foreach (string dirPath in Directory.GetDirectories(Settings.SourcePath,"*",SearchOption.AllDirectories))
                {
                   if (Directory.GetLastWriteTime(dirPath)>this.GetOldest(this.FullBack))
                    {
                        Directory.CreateDirectory(dirPath.Replace(Settings.SourcePath,Settings.DestinationPath + @"\DiffBackupFor-" + this.FullCount + @"-\"));
                    }
                                                  
                }
                foreach (string filePath in Directory.GetFiles(Settings.SourcePath, "*.*", SearchOption.AllDirectories))
                {

                    if (File.GetLastWriteTime(filePath) > this.GetOldest(this.FullBack))
                    {
                        File.Copy(filePath, filePath.Replace(Settings.SourcePath, Settings.DestinationPath + @"\DiffBackupFor-" + this.FullCount + @"-\"), true);
                    }
                }
            }
            else
            {
                this.FullBackup();
            }
            Console.WriteLine("DifferentialBackup completed!");
            Console.WriteLine(Count + " files copied");
        }

        public void InkrementalBackup()
        {
            if(FullBack!=null)
            {
                DateTime oldest = (this.DiffBack == null ? this.GetOldest(this.FullBack) : this.GetOldest(DiffBack));

                foreach (string dirPath in Directory.GetDirectories(Settings.SourcePath, "*", SearchOption.AllDirectories))
                {
                    if (Directory.GetLastWriteTime(dirPath) > oldest)
                    {
                        Directory.CreateDirectory(dirPath.Replace(Settings.SourcePath, Settings.DestinationPath + @"\InkrBackupFor-" + this.FullCount + @"-\"));
                    }

                }
                foreach (string filePath in Directory.GetFiles(Settings.SourcePath, "*.*", SearchOption.AllDirectories))
                {

                    if (File.GetLastWriteTime(filePath) > oldest)
                    {
                        File.Copy(filePath, filePath.Replace(Settings.SourcePath, Settings.DestinationPath + @"\InkrBackupFor-" + this.FullCount + @"-\"), true);
                    }
                }
            }
            else
            {
                this.FullBackup();
            }

        }
    }
}
