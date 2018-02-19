using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daemon
{
    /// <summary>
    /// Informace o probehlych backupech pro tbBackup
    /// </summary>
    public class BackupInformation
    {
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public int Size { get; set; }
    }
}
