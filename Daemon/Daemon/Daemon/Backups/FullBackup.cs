﻿using Newtonsoft.Json;
using SharpCompress.Archives;
using SharpCompress.Archives.Zip;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daemon
{
    public class FullBackup : IBackup
    {
        ReportMaker reportMaker = new ReportMaker();
        private List<string> SourcePaths;
        private List<string> DestinationPaths;
        private int backupCount = 0;
        private bool Rar;
        public string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public FullBackup(PlannedBackups item)
        {
            try
            {
                this.SourcePaths = JsonConvert.DeserializeObject<List<string>>(item.SourcePath);
            }
            catch
            {
                this.SourcePaths = new List<string>() { item.SourcePath };
            }

            try
            {
                this.DestinationPaths = JsonConvert.DeserializeObject<List<string>>(item.DestinationPath);
            }
            catch
            {
                this.DestinationPaths = new List<string>() { item.DestinationPath };
            }

            this.Rar = item.Rar;
            this.Start();
        }

        public void Start()
        {
            foreach (string item in this.SourcePaths)
            {
                this.Backup(item);
                this.backupCount++;
            }
        }

        public void SendReport()
        {
            Console.WriteLine("FullBackup completed!");
        }

        private void Backup(string sourcePath)
        {
            int Count = 0;

            //Vytvori slozky pro diferencialni backupy
            foreach (string destinationPath in this.DestinationPaths)
            {
                if (this.backupCount == 0)
                {
                    DirectoryInfo directoryInfo = Directory.CreateDirectory(destinationPath + "\\diff_backups");
                    directoryInfo.Attributes = FileAttributes.Hidden;
                    Directory.CreateDirectory(destinationPath + "\\diff_backups\\0");
                }
            }

            //Vytvoří podsložky
            foreach (string destinationPath in this.DestinationPaths)
            {
                foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(sourcePath, destinationPath));
                }
            }

            //Zkopíruje všechny soubory a přepíše existující, zanese o nich zaznamy do logu
            List<LogModel> newLog = new List<LogModel>();
            foreach (string destinationPath in this.DestinationPaths)
            {
                foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories).OrderBy(f => new FileInfo(f).FullName))
                {
                    File.Copy(newPath, newPath.Replace(sourcePath, destinationPath), true);
                    Count++;
                    FileInfo fileInfo = new FileInfo(newPath);
                    newLog.Add(new LogModel() { Action = "EXI", FileName = fileInfo.Name, FilePath = fileInfo.FullName, LastWriteTime = Convert.ToDateTime(fileInfo.LastWriteTime) });
                    reportMaker.AddFile(new FileInfo(newPath));
                }
            }

            //Zapsani logu do souboru souboru
            foreach (string destinationPath in this.DestinationPaths)
            {
                using (StreamWriter streamWriter = new StreamWriter(destinationPath + "\\diff_backups\\0\\FileLog.log", false))
                {
                    string toWrite = JsonConvert.SerializeObject(newLog);
                    streamWriter.Write(toWrite);
                }
            }

            if (this.Rar)
                this.ZipFiles();            

            this.SendReport();
        }

        private void ZipFiles()
        {
            foreach (string destinationPath in this.DestinationPaths)
            {
                try
                {
                    using (var archive = ZipArchive.Create())
                    {
                        archive.AddAllFromDirectory(destinationPath);
                        archive.SaveTo(destinationPath, CompressionType.Deflate);
                    }
                }
                catch
                {
                    throw new Exception("Could not save to .RAR format");
                }
            }
        }

    }
}