using Daemon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace DaemonBackuper
{
    public partial class DaemonBackuper : ServiceBase
    {
        public DaemonBackuper()
        {
            InitializeComponent();
        }

        public void OnDebug()
        {
            this.OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            Application app = new Application();
            app.SetTimer();
        }

        protected override void OnStop()
        {
        }
    }
}
