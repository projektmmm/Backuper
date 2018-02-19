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
        public string Path { get; set; }
        public string Name { get; set; }
        public long Size { get; set; }
        public DateTime LastWriteTime { get; set; }

        public FileInformation(FileInfo f)
        {
            this.Path = f.DirectoryName;
            this.Name = f.Name;
            this.Size = f.Length;
            this.LastWriteTime = f.LastWriteTime;
        }
    }
}
