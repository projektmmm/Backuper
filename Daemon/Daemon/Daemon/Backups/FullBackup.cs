using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daemon
{
    public class FullBackup : ABackup
    {
        public FullBackup(PlannedBackups item) : base(item)
        {

        }

        protected override void Backup(string sourcePath, List<string> destinationPaths)
        {
            int Count = 0;

            /*
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
            */

            //Prespsani destination path
            for (int i = 0; i < destinationPaths.Count; i++)
            {
                destinationPaths[i] += $"\\{sourcePath.Substring(Functionality.IdentifyCharPosition('\\', sourcePath))}";
            }

            //Vytvoří podsložky
            foreach (string destinationPath in destinationPaths)
            {
                int index = 0;
                foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
                {
                    try
                    {
                        Directory.CreateDirectory(dirPath.Replace(sourcePath, destinationPath));
                    }
                    catch (Exception ex)
                    {
                        this.reportMaker.AddError(new ErrorDetails()
                        {
                            Exception = ex.Message,
                            Problem = "The program was unable to create folder.",
                            Path = dirPath
                        });
                    }
                    index++;
                }

                if (index == 0)
                {
                    try
                    {
                        Directory.CreateDirectory(destinationPath);
                    }
                    catch (Exception ex)
                    {
                        this.reportMaker.AddError(new ErrorDetails()
                        {
                            Exception = ex.Message,
                            Problem = "The program was unable to destination create folder.",
                            Path = destinationPath
                        });
                    }
                }
            }

            //Zkopíruje všechny soubory a přepíše existující, zanese o nich zaznamy do logu
            List<LogModel> newLog = new List<LogModel>();
            foreach (string destinationPath in destinationPaths)
            {
                foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories).OrderBy(f => new FileInfo(f).FullName))
                {
                    try
                    {
                        File.Copy(newPath, newPath.Replace(sourcePath, destinationPath), true);
                        Count++;
                        FileInfo fileInfo = new FileInfo(newPath);
                        newLog.Add(new LogModel() { Action = "EXI", FileName = fileInfo.Name, FilePath = fileInfo.FullName, LastWriteTime = Convert.ToDateTime(fileInfo.LastWriteTime) });
                        reportMaker.AddFile(new FileInfo(newPath));
                    }
                    catch (Exception ex)
                    {
                        this.reportMaker.AddError(new ErrorDetails()
                        {
                            Problem = "The file was not copied",
                            Path = newPath,
                            Exception = ex.Message,
                            AffectedFiles = 1
                        });
                    }
                }
            }

            //Zapsani logu do souboru souboru         
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(sourcePath + "\\BackupsLog.log", true))
                {
                    string toWrite = JsonConvert.SerializeObject(newLog);
                    streamWriter.Write(toWrite);
                }
            }
            catch (Exception ex)
            {
                this.reportMaker.AddError(new ErrorDetails()
                {
                    Exception = ex.Message,
                    Path = sourcePath + "\\BackupsLog.log",
                    Problem = "Can't create the log file. Future DIFF and INCR backups won't be possible. Check the folder settings and repeat the backup."
                });
            }

            /*
            FileInfo fi = new FileInfo(sourcePath + "\\BackupsLog.log");
            fi.Attributes = FileAttributes.ReadOnly;*/
                 



            if (this.backup.Rar)
                if (!BackupOperations.ZipFiles(destinationPaths))
                    this.reportMaker.AddError(new ErrorDetails() { Problem = "Could not ZIP the files." });
        }


    }
}
