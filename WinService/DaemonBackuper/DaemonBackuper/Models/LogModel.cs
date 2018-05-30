using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daemon
{
    public class LogModel
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime LastWriteTime { get; set; }
        public string Action { get; set; }
    }
}
