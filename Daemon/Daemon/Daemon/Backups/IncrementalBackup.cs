using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daemon
{
    public class IncrementalBackup : IBackup
    {
        public IncrementalBackup(PlannedBackups backup)
        {

        }

        public void SendReport(int count)
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }
    }
}
