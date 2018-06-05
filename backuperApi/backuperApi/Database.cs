using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.ComponentModel;
using backuperApi.Models;

namespace backuperApi
{
    public class Database : DbContext
    {
        public DbSet<BackupReport> BackupReport { get; set; }
        public DbSet<Backups> Backups { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Daemons> Daemons { get; set; }
        public DbSet<BackupErrors> BackupErrors { get; set; }
        public DbSet<Paths> Paths { get; set; }
        public DbSet<FTPSettings> FtpSettings { get; set; }
        public DbSet<SSHSettings> SshSettings { get; set; }
<<<<<<< HEAD
        public DbSet<Databases> Databases { get; set; }
=======
>>>>>>> 4ec0bac1a3e487221c71023e912948ae9952ed08
        public DbSet<Batches> Batches { get; set; }

        public static string ConnectionString = "Server=mysqlstudenti.litv.sssvt.cz; Database=3b2_macekdaniel_db2; Uid=macekdaniel; Pwd=123456";

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

    }
}