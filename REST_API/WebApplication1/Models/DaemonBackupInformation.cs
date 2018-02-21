using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class DaemonBackupInformation
    {
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public int Size { get; set; }
    }
}