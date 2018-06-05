using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backuperApi
{
    public class Batches
    {
        public int Id { get; set; }
        public string LocalPath { get; set; }
        public string CommandText { get; set; }
        public string Type { get; set; }
        public string Time { get; set; }
        public int BackupId { get; set; }
    }
}