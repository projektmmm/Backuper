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
    public class BackupController : ApiController
    {
        [Route("api/admin/backups")]
        public List<CommandInformation> Get()
        {
            List<CommandInformation> data = new List<CommandInformation>();
            string commandText = "SELECT * FROM tbBackups";

            using (MySqlConnection sConn = Sql.GetConnection())
            {
                sConn.Open();
                MySqlCommand sComm = new MySqlCommand(commandText, sConn);
                MySqlDataReader sRead = sComm.ExecuteReader();

                while (sRead.Read())
                {
                    data.Add(new CommandInformation()
                    {
                        RunAt = Convert.ToDateTime(sRead[1]),
                        Cron = sRead[2].ToString(),
                        DaemonId = Convert.ToInt32(sRead[3]),
                        BackupType = sRead[4].ToString(),
                        SourcePath = sRead[5].ToString(),
                        DestinationPath = sRead[6].ToString()
                    });
                }
                sConn.Close();
            }

            return data;

        }
    }
}