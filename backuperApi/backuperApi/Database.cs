using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace backuperApi
{
    public class Database : DbContext
    {
        public DbSet<BackupReport> BackupReport { get; set; }
        public DbSet<Backups> Backups { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Daemons> Daemons { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
        }

    }
}