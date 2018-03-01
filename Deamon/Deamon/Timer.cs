using Daemon;
using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private  void SetTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(this.settings.AskInterval);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += SendRequest;
            this.SetTimer();
        }

        //Pošle request do databáze aby zjistil nové nastanení a následně ho změní všude kde je třeba
        //pokuď již nastal čas backupu zpustí ho
        private  void SendRequest(Object source, ElapsedEventArgs e)
        {
            ApiCommunication.GetNextRunSetting("api/daemon");
            //Přidat kontrolu jestli se něco změnilo
            this.settings = this.ApiCommunication.nextRunSettings.OverrideSettings();
            this.BackMenu.ChangeBackupSettings(this.settings.BackupType);
            //Kontrola jestli nastal čas backupu 
            if (DateTime.Now < this.settings.RunAt)
            {
                this.BackMenu.StartBackup(this.settings.SourcePath, this.settings.DestinationPath);
            }
        }
    
    }
}
