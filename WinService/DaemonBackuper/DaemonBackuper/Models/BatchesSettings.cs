using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daemon
{
    public class BatchesSettings
    {
        public int Id { get; set; }
        public string LocalPath { get; set; }
        public string CommandText { get; set; }
        public string Type { get; set; }
        public string Time { get; set; }
        public int BackupId { get; set; }
    }
}
