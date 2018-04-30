using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using NCrontab;

namespace Daemon
{
    public class Application
    {
        private CrontabSchedule Cron;
        private Timer Timer;
        private Communicator communicator = new Communicator();

        public void SetTimer()
        {
            this.Timer = new Timer(DaemonSettings.AskInterval);
            this.Timer.Enabled = true;
            this.Timer.Elapsed += this.SendRequest;
        }

        private void SendRequest(Object sender, EventArgs e)
        {
            //List<PlannedBackups> backups =  communicator.GetNextRunSetting();

        }
    }
}
