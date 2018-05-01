using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daemon
{
    public class DifferentialBackup : IBackup
    {
        public DifferentialBackup(PlannedBackups backup)
        {

        }

        public void SendReport()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }
    }
}
