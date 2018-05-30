using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Threading;

namespace Daemon
{
    public abstract class ABackup
    {
        public ReportMaker reportMaker = new ReportMaker();
        public Communicator communicator = new Communicator();
        protected PlannedBackups backup { get; set; }
        protected List<string> SourcePaths;
        protected List<string> DestinationPaths;
        protected List<ErrorDetails> ErrorDetails = new List<ErrorDetails>();
        protected List<LogModel> newLog;
        protected int backupCount = 0;


        public ABackup(PlannedBackups item)
        {
            Console.WriteLine($"{item.BackupType} backup in progress");
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

            this.backup = item;
            this.Start();
        }

        protected abstract void Backup(string sourcePath, List<string> destinationPaths);

        public virtual void Start()
        {
            foreach (string item in this.SourcePaths)
            {
                if (this.backup.BackupType == "FULL")
                    this.CreateLog(item);
                this.Backup(item, new List<string>(this.DestinationPaths));
                this.backupCount++;
            }
        }

        public virtual void SendReport()
        {
            Console.WriteLine("Sending the report");
            BackupReport report = new BackupReport()
            {
                UserId = DaemonSettings.UserId,
                DaemonId = DaemonSettings.Id,
                Date = DateTime.Now,
                Type = this.backup.BackupType,
                Size = this.reportMaker.GetFileSize(),
                BackupId = this.backup.Id,
            };

            this.communicator.PostBackupReport(report, this.reportMaker.GetErrors());
        }

        public virtual void CreateLog(string sourcePath)
        {
            this.newLog = new List<LogModel>();
            foreach (string item in Directory.GetFiles(sourcePath, "*", SearchOption.AllDirectories))
            {

                FileInfo fileInfo = new FileInfo(item);
                this.newLog.Add(new LogModel() { Action = "EXI", FileName = fileInfo.Name, FilePath = fileInfo.FullName, LastWriteTime = Convert.ToDateTime(fileInfo.LastWriteTime) });
            }

            try
            {
                using (StreamWriter streamWriter = new StreamWriter(sourcePath + "\\BackupsLog.log", false))
                {
                    string toWrite = JsonConvert.SerializeObject(this.newLog);
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
        }

        public virtual void Operations()
        {
            if (this.backup.Ftp)
            {
                this.communicator.GetFtpSettings();
                Thread.Sleep(20000);

                if (!BackupOperations.Ftp(this.DestinationPaths[0]))
                {
                    this.reportMaker.AddError(new ErrorDetails()
                    {
                        Problem = "Could not upload files to FTP. Check the connection settings.",
                    });
                }
            }
            if (this.backup.Ssh)
            {
                this.communicator.GetSshSettings();
                Thread.Sleep(20000);

                if (!BackupOperations.Ssh(this.DestinationPaths[0]))
                {
                    this.reportMaker.AddError(new ErrorDetails()
                    {
                        Problem = "Could not upload files to SSH. Check the connection settings.",
                    });
                }
            }
        }
    }
}
