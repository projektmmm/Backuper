using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class NextRunSettings
    {
        public DateTime RunAt { get; set; }
        public string Cron { get; set; }
        public string BackupType { get; set; }
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
    }
}