﻿using MySql.Data.MySqlClient;
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
        /// <summary>
        /// POST z admina o novych nastavenich
        /// </summary>
        [Route("api/admin")]
        public void Post([FromBody]List<string> info)
        {
            AdminCommandInformation incommingCommand = JsonConvert.DeserializeObject<AdminCommandInformation>(info[0]);
            string commandText = "INSERT INTO tbBackups(RunAt, Cron, DaemonId, BackupType, SourcePath, DestinationPath) VALUES(@RunAt,@Cron,@DaemonId,@BackupType,@SourcePath,@DestinationPath)";

            using (MySqlConnection sConn = new MySqlConnection(Sql.ConnectionString))
            {
                sConn.Open();
                MySqlCommand command = new MySqlCommand(commandText, sConn);
                command.Parameters.Add("@RunAt", MySqlDbType.DateTime);
                command.Parameters.Add("@Cron", MySqlDbType.VarChar);
                command.Parameters.Add("@DaemonId", MySqlDbType.Int32);
                command.Parameters.Add("@BackupType", MySqlDbType.VarChar);
                command.Parameters.Add("@SourcePath", MySqlDbType.VarChar);
                command.Parameters.Add("@DestinationPath", MySqlDbType.VarChar);

                command.Parameters["@RunAt"].Value = incommingCommand.RunAt.ToString("yyyy-MM-dd HH:mm:ss.fff");
                command.Parameters["@Cron"].Value = incommingCommand.Cron;
                command.Parameters["@DaemonId"].Value = incommingCommand.DaemonId;
                command.Parameters["@BackupType"].Value = incommingCommand.BackupType;
                command.Parameters["@SourcePath"].Value = incommingCommand.SourcePath;
                command.Parameters["@DestinationPath"].Value = incommingCommand.DestinationPath;

                command.ExecuteNonQuery();
                sConn.Close();
            }
        }
    }
}