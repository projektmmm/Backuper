using Daemon;
using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using NCrontab;

namespace Daemon
{
    public class Timer //Macek
    {
        private BackupMenu BackMenu = new BackupMenu();
        private Settings settings = new Settings();
        private ApiCommunication ApiCommunication = new ApiCommunication();
        private static System.Timers.Timer aTimer;
        private DateTime NextRunAt;
        private CrontabSchedule Schedule;

        //Private List<Settings>

        public void Start()
        {
            SetTimer();
        }

        //Nastaví timer na interval danný v settings 
        private void SetTimer()
        {
            aTimer = new System.Timers.Timer(this.settings.AskInterval);
            aTimer.Enabled = true;

            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += SendRequest;
        }

        //Pošle request do databáze aby zjistil nové nastavení a následně ho změní všude kde je třeba
        //pokuď již nastal čas backupu spustí ho
        private void SendRequest(Object source, ElapsedEventArgs e)
        {
            //Console.WriteLine("getting nextrun settings");
            ApiCommunication.GetNextRunSetting("api/daemon");

            //Console.WriteLine("waiting 10 sec");
            Thread.Sleep(10000);

            //Console.WriteLine("overriding settings");
            this.settings = this.ApiCommunication.nextRunSettings.OverrideSettings();

            Schedule = CrontabSchedule.Parse(this.settings.Cron);
            NextRunAt = Schedule.GetNextOccurrence(DateTime.Now.AddSeconds(-70));
            //Console.WriteLine("Now: " + DateTime.Now);
            //Console.WriteLine("NextRunAt: " + NextRunAt.ToString());

            //Kontrola jestli nastal čas backupu 
            if (DateTime.Now >= this.NextRunAt)
            {
                //Console.WriteLine("--BACKUP STARTED--");
                this.BackMenu.StartBackup(this.settings.SourcePath, this.settings.DestinationPath);
            }


            //Console.WriteLine("Setting timer");
            aTimer.Stop();
            SetTimer();
        }
    }
}
