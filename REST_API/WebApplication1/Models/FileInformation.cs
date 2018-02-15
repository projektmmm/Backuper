using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class FileInformation
    {
        public string Path;
        public string Name;
        public long Size; //in bytes
        public DateTime LastWriteTime;
    }
}