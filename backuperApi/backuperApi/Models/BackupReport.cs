using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backuperApi
{
    public class BackupReport
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public int Size { get; set; }
        public int ConnectedTo { get; set; }
        public int DaemonId { get; set; }
    }
}