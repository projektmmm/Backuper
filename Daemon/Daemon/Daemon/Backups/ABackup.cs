using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
    }
}
