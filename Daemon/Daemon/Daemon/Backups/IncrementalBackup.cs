﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Daemon
{
    public class IncrementalBackup : ABackup
    {
        public IncrementalBackup(PlannedBackups item) : base(item)
        {
            
        }

        protected override void Backup(string sourcePath, List<string> destinationPaths)
        {
            string newLogPath = "";
            this.newLog = new List<LogModel>();

            //Zmena destinationPath I
            for (int i = 0; i < destinationPaths.Count; i++)
            {
                destinationPaths[i] += $"\\{sourcePath.Substring(Functionality.IdentifyCharPosition('\\', sourcePath))}";
            }

            //Zjisteni slozek
            int cnt = 0;
            string[] sourceFolder = Directory.GetDirectories(sourcePath, "*", SearchOption.TopDirectoryOnly);
            foreach (string item in sourceFolder)
            {
                if (item.Contains("incr_backup"))
                    cnt++;
            }

            //Nalezeni posledniho logu
            List<LogModel> oldLog = new List<LogModel>();
            string oldLogPath = "";

            //vytvoreni slozek v sourcePath pro log
            if (cnt == 0)
            {
                try
                {
                    Directory.CreateDirectory(sourcePath + "\\incr_backup_0");
                    oldLogPath = sourcePath + "\\BackupsLog.log";
                }
                catch (Exception ex)
                {
                    this.reportMaker.AddError(new ErrorDetails()
                    {
                        Exception = ex.Message,
                        Path = sourcePath + "\\incr_backup_0",
                        Problem = "Can't create the folder for incremental backup. Backup hasn't been done."
                    });
                    return;
                }
            }
            else
            {
                try
                {
                    newLogPath = sourcePath + $"\\incr_backup_{cnt}";
                    Directory.CreateDirectory(newLogPath);
                    oldLogPath = sourcePath + $"\\incr_backup_{cnt-1}\\BackupsLog.log";
                }
                catch (Exception ex)
                {
                    this.reportMaker.AddError(new ErrorDetails()
                    {
                    Exception = ex.Message,
                    Path = sourcePath + $"\\incr_backup_{cnt}",
                    Problem = "Can't create the folder for incremental backup. Backup hasn't been done."
                    });
                    return;
                }
            }

            //uprava cesty na DestinationPath II
            for (int i = 0; i < destinationPaths.Count; i++)
            {
                destinationPaths[i] += $"\\incr_backup_{cnt}";
            }

            //Nacteni logu
            try
            {
                using (StreamReader streamReader = new StreamReader(oldLogPath, false))
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
                    Problem = "The program didn't find the full backup log. DIFF backup for this source folder hadn't been done.",
                    Path = sourcePath
                });
                return;
            }

            foreach (string filePath in Directory.GetFiles(sourcePath, "*", SearchOption.AllDirectories))
            {
                FileInfo fileInfo = new FileInfo(filePath);             
                bool found = false;

                foreach (LogModel item in oldLog)
                {
                    if (item.FilePath != new FileInfo(filePath).FullName)
                        continue;

                    //nezmeneno - EXISTS
                    if (fileInfo.FullName == item.FilePath && Convert.ToDateTime(fileInfo.LastWriteTime) == item.LastWriteTime)
                    {
                        this.newLog.Add(new LogModel() { Action = "EXI", FileName = fileInfo.Name, FilePath = fileInfo.FullName, LastWriteTime = Convert.ToDateTime(fileInfo.LastWriteTime) });
                        found = true;
                        break;
                    }
                    //zmena zapisu - MODIFIED
                    else if (fileInfo.FullName == item.FilePath && Convert.ToDateTime(fileInfo.LastWriteTime) != item.LastWriteTime)
                    {
                        this.newLog.Add(new LogModel() { Action = "MOD", FileName = fileInfo.Name, FilePath = fileInfo.FullName, LastWriteTime = Convert.ToDateTime(fileInfo.LastWriteTime) });

                        foreach (string destinationPath in destinationPaths)
                        {
                            string directoryName = fileInfo.FullName.Replace(sourcePath, destinationPath);
                            directoryName = directoryName.Substring(0, Functionality.IdentifyCharPosition('\\', directoryName) - 1);
                            if (!Directory.Exists(directoryName))
                            {
                                try
                                {
                                    Directory.CreateDirectory(directoryName.Substring(0, Functionality.IdentifyCharPosition('\\', directoryName)-1));
                                }
                                catch (Exception ex)
                                {
                                    this.reportMaker.AddError(new ErrorDetails()
                                    {
                                        Exception = ex.Message,
                                        Problem = "The program was unable to create directory.",
                                        Path = sourcePath
                                    });
                                    continue;
                                }
                            }


                            try
                            {
                                File.Copy(filePath, filePath.Replace(sourcePath, destinationPath), true);
                                reportMaker.AddFile(new FileInfo(filePath));
                            }
                            catch (Exception ex)
                            {
                                this.reportMaker.AddError(new ErrorDetails()
                                {
                                    Exception = ex.Message,
                                    Problem = "The program was unable to copy file.",
                                    Path = sourcePath
                                });
                                continue;
                            }
                        }
                        found = true;
                    }
                }
                if (found == true)
                    continue;

                //nenalezene v minulem logu - NEW
                foreach (string destinationPath in destinationPaths)
                {
                    string directoryName = fileInfo.FullName.Replace(sourcePath, destinationPath);
                    directoryName = directoryName.Substring(0, Functionality.IdentifyCharPosition('\\', directoryName) - 1);
                    if (!Directory.Exists(directoryName))
                    {
                        try
                        {
                            Directory.CreateDirectory(directoryName);
                        }
                        catch (Exception ex)
                        {
                            this.reportMaker.AddError(new ErrorDetails()
                            {
                                Exception = ex.Message,
                                Problem = "Backupper was unable to create destination path.",
                                Path = directoryName
                            });
                        }
                    }
                    try
                    {
                        File.Copy(filePath, filePath.Replace(sourcePath, destinationPath), true);
                        reportMaker.AddFile(new FileInfo(filePath));
                        this.newLog.Add(new LogModel() { Action = "NEW", FileName = fileInfo.Name, FilePath = fileInfo.FullName, LastWriteTime = Convert.ToDateTime(fileInfo.LastWriteTime) });
                    }
                    catch (Exception ex)
                    {
                        this.reportMaker.AddError(new ErrorDetails()
                        {
                            Exception = ex.Message,
                            Problem = "The program was unable to copy file.",
                            Path = sourcePath
                        });
                    }
                }               
            }

            //zapsani logu
            if (this.backupCount == 0)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(sourcePath + $"\\incr_backup_{cnt}" + "\\BackupsLog.log", false))
                    {

                        string toWrite = JsonConvert.SerializeObject(this.newLog);
                        sw.Write(toWrite);

                        sw.Flush();
                    }
                }
                catch (Exception ex)
                {
                    this.reportMaker.AddError(new ErrorDetails()
                    {
                        Exception = ex.Message,
                        Problem = "The program was unable to create log. Future Incremental backups won't be possible",
                        Path = sourcePath
                    });
                }
            }

            this.backupCount++;
        }
    }
}
