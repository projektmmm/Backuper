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
            //Prespsani destination path
            for (int i = 0; i < destinationPaths.Count; i++)
            {
                destinationPaths[i] += $"\\{sourcePath.Substring(Functionality.IdentifyCharPosition('\\', sourcePath))}";
            }

            //Vytvoří podsložky
            foreach (string destinationPath in destinationPaths)
            {
                int index = 0;
                if (Directory.Exists(destinationPath) && backup.Override)
                {
                    try
                    {
                        Directory.Delete(destinationPath, true);
                    }
                    catch (Exception ex)
                    {
                        this.reportMaker.AddError(new ErrorDetails()
                        {
                            Exception = ex.Message,
                            Problem = "Could not delete the old folder. Override failed",
                            Path = destinationPath
                        });
                    }
                }

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
            foreach (string destinationPath in destinationPaths)
            {
                foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories).OrderBy(f => new FileInfo(f).FullName))
                {
                    try
                    {
                        File.Copy(newPath, newPath.Replace(sourcePath, destinationPath), true);
                        FileInfo fileInfo = new FileInfo(newPath);
                        //*newLog.Add(new LogModel() { Action = "EXI", FileName = fileInfo.Name, FilePath = fileInfo.FullName, LastWriteTime = Convert.ToDateTime(fileInfo.LastWriteTime) });
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

            if (this.backup.Rar)
                if (!BackupOperations.ZipFiles(destinationPaths))
                    this.reportMaker.AddError(new ErrorDetails() { Problem = "Could not ZIP the files." });
        }


    }
}