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

        protected override void Backup(string sourcePath)
        {
            int fileCount = 0;
            int directoryCount = 0;

            //Vytvoreni podslozek pro svuj diferencialni backup - v pripade opakovani
            if (this.backupCount == 0)
            {
                for (int i = 0; i < this.DestinationPaths.Count; i++)
                {
                    int lenght = 0;
                    try
                    {
                        string[] paths = Directory.GetDirectories(this.DestinationPaths[i], "*", SearchOption.TopDirectoryOnly);
                        lenght = paths.Length;
                    }
                    catch
                    {
                        lenght = 1;
                    }

                    this.DestinationPaths[i] += $"\\diff_backup_{DateTime.Now.ToString("dd.MM.yyyy - HH-mm")}";
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
            }

            //Vytvoreni podslozek
            foreach (string destinationPath in this.DestinationPaths)
            {
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
                            Problem = "The program was unable to create a subfolder.",
                            Path = dirPath
                        });
                    }
                }
            }

            //Prekopirovani novych slozek
            foreach (string destinationPath in this.DestinationPaths)
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

            List<LogModel> oldLog = new List<LogModel>();
            //Nacetni stareho logu
            try
            {
                using (StreamReader streamReader = new StreamReader(sourcePath + "\\diff_backups\\0\\FileLog.log", false))
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
                        foreach (string destinationPath in this.DestinationPaths)
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