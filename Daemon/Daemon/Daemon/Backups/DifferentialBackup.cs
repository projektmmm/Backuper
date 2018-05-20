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
            int fileCount = 0;
            int directoryCount = 0;

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
                    Problem = "The program didn't find the full backup log. DIFF backup for this source folder hasn't been done.",
                    Path = sourcePath
                });
                return;
            }

            //Prespsani destination path
            for (int i = 0; i < destinationPaths.Count; i++)
            {
                destinationPaths[i] += $"\\{sourcePath.Substring(Functionality.IdentifyCharPosition('\\', sourcePath))}";
                string[] directories = Directory.GetDirectories(destinationPaths[i], "diff_backup", SearchOption.TopDirectoryOnly);
                destinationPaths[i] += $"\\diff_backup{directories.Length}";
            }

            /*
            //Vytvoreni podslozek pro svuj diferencialni backup - v pripade opakovani
            if (this.backupCount == 0)
            {
                for (int i = 0; i < destinationPaths.Count; i++)
                {
                    int lenght = 0;
                    try
                    {
                        string[] paths = Directory.GetDirectories(destinationPaths[i], "*", SearchOption.TopDirectoryOnly);
                        lenght = paths.Length;
                    }
                    catch
                    {
                        lenght = 1;
                    }
                    try
                    {
                        Directory.CreateDirectory(this.DestinationPaths[i]);
                    }
                    catch (Exception ex)
                    {
                        this.reportMaker.AddError(new ErrorDetails()
                        {
                            Exception = ex.Message,
                            Problem = "The problam was unable to create folder for differential backup.",
                            Path = this.DestinationPaths[i]
                        });
                    }
                }
            }*/

            //Vytvoreni podslozek
            foreach (string destinationPath in destinationPaths)
            {
                foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
                {
                    int i = 0;
                    try
                    {
                        Directory.CreateDirectory(dirPath.Replace(sourcePath, destinationPath));
                    }
                    catch (Exception ex)
                    {
                        this.reportMaker.AddError(new ErrorDetails()
                        {
                            Exception = ex.Message,
                            Problem = "The program was unable to create a subfolder.",
                            Path = dirPath
                        });
                    }

                    if (i == 0)
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
            }

            //Prekopirovani novych slozek
            foreach (string destinationPath in destinationPaths)
            {
                foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
                {
                    if (Directory.GetLastWriteTime(dirPath) > this.GetOldest(sourcePath))
                    {
                        try
                        {
                            Directory.CreateDirectory(dirPath.Replace(dirPath, destinationPath));
                        }
                        catch (Exception ex)
                        {
                            this.reportMaker.AddError(new ErrorDetails()
                            {
                                Exception = ex.Message,
                                Path = destinationPath,
                                Problem = "The program was unable to create a subfolder."
                            });
                        }
                    }
                }
            }

            

            int index;
            //Porovnavani logu s aktualni verzi vychozi slozky pro fullbackup
            foreach (string filePath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories).OrderBy(f => new FileInfo(f).FullName))
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
                    //zmeneno
                    else if (fileInfo.FullName == item.FilePath && Convert.ToDateTime(fileInfo.LastWriteTime) != item.LastWriteTime)
                    {
                        foreach (string destinationPath in destinationPaths)
                        {
                            try
                            {
                                File.Copy(filePath, destinationPath);
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

                        /*
                        if (Application.CountChar(fileInfo.FullName, '\\') - 1 > Application.CountChar(this.DestinationPath, '\\') - 2)
                        {
                            string directoryName = fileInfo.FullName.Replace(this.SourcePath, this.DestinationPath);
                            string newdirname = directoryName.Substring(0, Application.IdentifyLastCharIndex(directoryName, '\\'));
                            Directory.CreateDirectory(newdirname);
                        }

                        File.Copy(filePath, filePath.Replace(this.SourcePath, this.DestinationPath), true);*/
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
            this.backupCount++;
            //this.SendReport();
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