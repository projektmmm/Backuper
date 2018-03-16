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
        /// <summary>
        /// POST do tbBackupReport - informace o probehlem backupu
        /// </summary>
        [Route("api/daemon")]
        public void Post([FromBody]List<string> info)
        {
            BackupInformation backupinfo = JsonConvert.DeserializeObject<BackupInformation>(info[0]);
            string commandText = "INSERT INTO tbBackupReport(Date, Type, Size) VALUES(@Date,@Type,@Size)";

            using (MySqlConnection sConn = Sql.GetConnection())
            {
                sConn.Open();
                MySqlCommand command = new MySqlCommand(commandText, sConn);

                command.Parameters.Add("@Date", MySqlDbType.DateTime);
                command.Parameters["@Date"].Value = backupinfo.Date.ToString("yyyy-MM-dd HH:mm:ss.fff");

                command.Parameters.Add("@Type", MySqlDbType.VarChar);
                command.Parameters["@Type"].Value = backupinfo.Type;

                command.Parameters.Add("@Size", MySqlDbType.Int32);
                command.Parameters["@Size"].Value = backupinfo.Size;

                //Sql.sComm.ExecuteNonQuery();
                command.ExecuteNonQuery();

                sConn.Close();
            }
        }

        /// <summary>
        /// GET z tbBackups o planovanych backupech
        /// </summary>
        [Route("api/daemon")]
        public NextRunSettings Get()
        {
            using (MySqlConnection sConn = Sql.GetConnection())
            {
                sConn.Open();
                Sql.SetCommand("SELECT RunAt, Cron, BackupType, SourcePath, DestinationPath FROM tbBackups WHERE Id=1");
                MySqlDataReader sRead = Sql.sComm.ExecuteReader();
                sRead.Read();
                NextRunSettings ret = new NextRunSettings()
                {
                    RunAt = Convert.ToDateTime(sRead[0]),
                    Cron = sRead[1].ToString(),
                    BackupType = sRead[2].ToString(),
                    SourcePath = sRead[3].ToString(),
                    DestinationPath = sRead[4].ToString()
                };
                sConn.Close();
                return ret;
            }
        }
    }
}