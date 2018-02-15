using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Daemon
{
    public class FileInformation
    {
        public string Path;
        public string Name;
        public long Size; //in bytes
        public DateTime LastWriteTime;
        public FileInformation(FileInfo f)
        {
            this.Path = f.DirectoryName;
            this.Name = f.Name;
            this.Size = f.Length;
            this.LastWriteTime = f.LastWriteTime;
        }
    }
}
