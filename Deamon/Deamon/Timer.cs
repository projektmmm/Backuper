using Daemon;
using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deamon
{
    public class Timer //Macek
    {
        private Backuper back = new Backuper();
        private static System.Timers.Timer aTimer;

        public  void Start()
        {
            SetTimer();
        }

        private  void SetTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(this.back.Settings.AskInterval);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += SendRequest;
            this.SetTimer();
        }

        private  void SendRequest(Object source, ElapsedEventArgs e)
        {
            
            
            //Dotáže se do Databáze jestli nejsou nové setting & nebo jestli neměl nastat backup (V případě že by někdo změnil setting na čas jenž již nastal)
            //nechám to pro jistotu na tobě Mullere ...


            //Kontrola jestli nastal čas backupu 
            if (DateTime.Now < this.back.Settings.RunAt)
            {
                //provede backup dle nastavení ... ještě dohodneme. 
                
            }
        }
    
    }
}
