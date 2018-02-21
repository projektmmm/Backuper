using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AdminController : ApiController
    {
        [Route("api/admin")]
        public void Post([FromBody]List<string> info)
        {
            AdminCommandInformation command = JsonConvert.DeserializeObject<AdminCommandInformation>(info[0]);

            using (MySqlConnection sConn = Sql.GetConnection())
            {
                sConn.Open();
                Sql.SetCommand($"INSERT INTO tbBackups(RunAt, DaemonId, BackupType, SourcePath, DestinationPath) VALUES('{command.RunAt.ToString("yyyy-MM-dd HH:mm:ss.fff")}','{command.DaemonId}','{command.BackupType}','{command.SourcePath}','{command.DestinationPath}')");
                Sql.sComm.ExecuteNonQuery();
                sConn.Close();
            }
        }
    }
}