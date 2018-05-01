using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backuperApi
{
    public class Paths
    {
        public int Id { get; set; }
        public int BackupId { get; set; }
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
        public int FTPId { get; set; }
        public int SSHId { get; set; }

    }
}