using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daemon
{
    public class FileInformation
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public DateTime LastWriteTime { get; set; }

        public FileInformation(FileInfo f)
        {
            this.Path = f.DirectoryName;
            this.Name = f.Name;
            this.Size = (int)f.Length;
            this.LastWriteTime = f.LastWriteTime;
        }
    }
}
