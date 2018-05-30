using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using NCrontab;

namespace Daemon
{
    public class Application
    {
        private CrontabSchedule Cron;
        private System.Timers.Timer Timer; //System definice kvuli using System.Threading
        private Communicator communicator = new Communicator();

        /// <summary>
        /// Nastavi novy Timer
        /// </summary>
        public void SetTimer()
        {
            Console.WriteLine("New Timer was set");
            this.Timer = new System.Timers.Timer(DaemonSettings.AskInterval);
            this.Timer.Enabled = true;
            this.Timer.Elapsed += this.CheckTimer;
        }

        /// <summary>
        /// Po uplynuti ask intervalu nejdrive zanalyzuje backupy, a pote se odesle request na api. Pote dojde k anulovani timeru.
        /// </summary>
        private void CheckTimer(Object sender, EventArgs e)
        {
            Console.WriteLine("Checking the database for new backups");
            if (DaemonSettings.plannedBackups != null)
                this.AnalyzeBackups();

            communicator.GetNextRunSetting();

            Thread.Sleep(20000); //vycka 20s, kvuli asynchronni metode

            if (DaemonSettings.plannedBackups.Count != 0)
                DaemonSettings.plannedBackups.OrderBy(r => r.NextRun);


            this.Timer.Stop();
            this.SetTimer();
        }


        /// <summary>
        /// Zanalyzuje vsechny backupy, cyklus skonci, pokud cas bude vetsi nez aktulani (OrderBy).
        /// </summary>
        public void AnalyzeBackups()
        {
            Console.WriteLine("Backups analyzation");
            foreach (PlannedBackups item in DaemonSettings.plannedBackups)
            {
                if (item.NextRun.Date == DateTime.Now.Date && item.NextRun.Hour == DateTime.Now.Hour && item.NextRun.Minute == DateTime.Now.Minute)
                {
                    switch (item.BackupType)
                    {
                        case "FULL":
                            this.DoBackup(new FullBackup(item));
                            break;
                        case "DIFF":
                            this.DoBackup(new DifferentialBackup(item));
                            break;
                        case "INCR":
                            this.DoBackup(new IncrementalBackup(item));
                            break;
                    }
                }
                else if (item.NextRun > DateTime.Now)
                    break;
            }
        }

        private void DoBackup(ABackup backup)
        {
            backup.Start();
            backup.Operations();
            backup.SendReport();
        }
    }
}
