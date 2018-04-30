using NCrontab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backuperApi
{
    public class CronController
    {
        public CrontabSchedule Schedule;
        private Database database = new Database();

        public async void UpdateCron(List<Backups> toCheck)
        {
            foreach (Backups item in toCheck)
            {
                Schedule = CrontabSchedule.Parse(item.Cron);

                Backups dbRecord = this.FindById(item.Id);
                dbRecord.NextRun = Schedule.GetNextOccurrence(DateTime.Now);
                this.database.SaveChanges();
            }
        }

        private Backups FindById(int id)
        {
            return this.database.Backups.Find(id);
        }
    }
}