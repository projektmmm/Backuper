using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backuperApi
{
    public class Backups
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime RunAt { get; set; }
        public string Cron { get; set; }
        public int DaemonId { get; set;}
        public string BackupType { get; set; }
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
    }
}