using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer.Management.Common;
using DaemonBackuper.Models;
using Daemon;

namespace Daemon
{
    public class DatabaseDump
    {
        public DatabaseDump()
        {
            Communicator c = new Communicator();
            
        }
        public List<Databases> ListDatabases;

        public void FullBackup()
        {
            foreach (Databases item in ListDatabases)
            {
                Server dbServer = new Server(new ServerConnection(item.ServerName, item.Name, item.Password));
                Backup dbbackup = new Backup() { Action = BackupActionType.Database, Database = item.Name };
                dbbackup.Devices.AddDevice(@"C:\Data\" + item.Name + @".bak", DeviceType.File);
                dbbackup.Initialize = true;
                dbbackup.SqlBackupAsync(dbServer);
            }
        }
    }
}