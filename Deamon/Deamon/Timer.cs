using Daemon;
using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Daemon
{
    public class Timer //Macek
    {
        private BackupMenu BackMenu = new BackupMenu();
        private Settings settings = new Settings();
        private ApiCommunication ApiCommunication = new ApiCommunication();
        private static System.Timers.Timer aTimer;
        
        //Private List<Settings>

        public  void Start()
        {
            SetTimer();
        }

        //Nastaví timer na interval danný v settings 
        private void SetTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(this.settings.AskInterval);
            
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += SendRequest;

            aTimer.Enabled = true;
        }

        //Pošle request do databáze aby zjistil nové nastanení a následně ho změní všude kde je třeba
        //pokuď již nastal čas backupu zpustí ho
        private void SendRequest(Object source, ElapsedEventArgs e)
        {
            ApiCommunication.GetNextRunSetting("api/daemon");
            //Přidat kontrolu jestli se něco změnilo
            Thread.Sleep(10000);
            this.settings = this.ApiCommunication.nextRunSettings.OverrideSettings();
            
            //Kontrola jestli nastal čas backupu 
            if (DateTime.Now < this.settings.RunAt)
            {
                this.BackMenu.StartBackup(this.settings.SourcePath, this.settings.DestinationPath);
            }

            this.UnparseCron();

            SetTimer();
        }

        private void UnparseCron()
        {
            string Cron = settings.Cron;
            int index = Cron.IndexOf('/') + 1;

            if (index == 1)
            {
                
                this.settings.AskInterval = Convert.ToInt32(Cron[index]) * 60 * 1000;
            }
            else if (index == 3)
            {
                this.settings.AskInterval = Convert.ToInt32(Cron[index]) * 60 * 60 * 1000;
            }
            else if (index == 5)
            {
                this.settings.AskInterval = Convert.ToInt32(Cron[index]) * 60 * 60 * 24 * 1000;
            }
            else
            {

            }
        }
    }
}
