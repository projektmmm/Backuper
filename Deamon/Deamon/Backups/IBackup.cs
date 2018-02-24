using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daemon.Backups
{
    public interface IBackup
    {
        void Backup(string SourcePath, string DestinationPath);
        void SendReport();
    }
}
