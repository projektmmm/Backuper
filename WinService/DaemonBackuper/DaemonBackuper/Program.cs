using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace DaemonBackuper
{
    static class Program
    {
        /// <summary>
        /// Hlavní vstupní bod aplikace.
        /// </summary>
        static void Main()
        {
#if DEBUG
            DaemonBackuper Daemon = new DaemonBackuper();
            Daemon.OnDebug();
            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new DaemonBackuper()
            };
            ServiceBase.Run(ServicesToRun);
#endif
        }
    }
}
