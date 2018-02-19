using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class DaemonFileInfo
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public int Size { get; set; } //MB
        public int BackupId { get; set; }
    }
}