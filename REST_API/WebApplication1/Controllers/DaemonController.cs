using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebApplication1.Models;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace WebApplication1.Controllers
{
    public class DaemonController : ApiController
    {

        [Route("api/daemon")]
        public void Post([FromBody]List<string> info)
        {
            //List<FileInformation> fileinfo = JsonConvert.DeserializeObject<List<FileInformation>>(info[0]);
            DaemonBackupInformation backupinfo = JsonConvert.DeserializeObject<DaemonBackupInformation>(info[0]);

            using (MySqlConnection sConn = Sql.GetConnection())
            {
                sConn.Open();

                //foreach (FileInformation item in fileinfo)
                //{
                //    Sql.SetCommand($"INSERT INTO tbFiles(Backup, Path, Name, Date, Size) VALUES('0', '{item.Path}', '{item.Name}', '{item.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss.fff")}', '{item.Size}')");
                //    Sql.sComm.ExecuteNonQuery();
                //}

                Sql.SetCommand($"INSERT INTO tbBackupReport(Date, Type, Size) VALUES('{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}','{backupinfo.Type}','{backupinfo.Size}')");
                Sql.sComm.ExecuteNonQuery();
                sConn.Close();
            }
        }

        [Route("api/daemon")]
        public DaemonNextRunSettings Get()
        {
            using (MySqlConnection sConn = Sql.GetConnection())
            {
                sConn.Open();
                Sql.SetCommand("SELECT RunAt, BackupType, SourcePath, DestinationPath FROM tbBackups WHERE Id=1");
                MySqlDataReader sRead = Sql.sComm.ExecuteReader();
                sRead.Read();
                DaemonNextRunSettings ret = new DaemonNextRunSettings()
                {
                    RunAt = Convert.ToDateTime(sRead[0]),
                    BackupType = Convert.ToInt32(sRead[1]),
                    SourcePath = sRead[2].ToString(),
                    DestinationPath = sRead[3].ToString()
                };
                sConn.Close();
                return ret;
            }
        }
    }
}