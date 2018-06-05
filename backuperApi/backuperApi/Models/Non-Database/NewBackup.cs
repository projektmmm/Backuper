using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backuperApi
{
    public class NewBackup
    {
        public int DaemonId { get; set; }
        public string Cron { get; set; }
        public string BackupType { get; set; }
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
        public string FTPServerAdress { get; set; }
        public int FTPPort { get; set; }
        public string FTPUsername { get; set; }
        public string FTPPassword { get; set; }
        public string SSHServerAdress { get; set; }
        public int SSHPort { get; set; }
        public string SSHUsername { get; set; }
        public string SSHPassword { get; set; }
        public bool Override { get; set; }
        public bool Rar { get; set; }
        public string BatchBeforePath { get; set; }
        public string BatchAfterPath { get; set; }
        public string BatchBeforeCode { get; set; }
        public string BatchAfterCode { get; set; }
    }
}