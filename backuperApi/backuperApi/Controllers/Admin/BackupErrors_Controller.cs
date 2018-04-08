using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace backuperApi.Controllers.Admin
{
    public class BackupErrors_Controller : ApiController
    {
        public Database database = new Database();

        [HttpGet]
        [Route("api/admin/backup-errors/{userId}-{daemonId}")]
        public List<ErrorDetails> Get(int userId, int daemonId)
        {
            string commandText = ErrorDetails.GetQuery();
            List<ErrorDetails> errorDetails = new List<ErrorDetails>();

            using (MySqlConnection sConn = new MySqlConnection(Database.ConnectionString))
            {
                sConn.Open();
                MySqlCommand command = new MySqlCommand(commandText, sConn);
                command.Parameters.Add("@UserId", MySqlDbType.Int32);
                command.Parameters.Add("@DaemonId", MySqlDbType.Int32);

                command.Parameters["@UserId"].Value = userId;
                command.Parameters["@DaemonId"].Value = daemonId;

                try
                {
                    MySqlDataReader sRead = command.ExecuteReader();
                    while (sRead.Read())
                    {
                        errorDetails.Add(new ErrorDetails()
                        {
                            Id = Convert.ToInt32(sRead[0]),
                            AffectedFiles = Convert.ToInt32(sRead[1]),
                            Problem = sRead[2].ToString(),
                            BackupId = Convert.ToInt32(sRead[3]),
                            DaemonId = Convert.ToInt32(sRead[4]),
                            DaemonName = sRead[5].ToString()
                        });
                    }

                    if (errorDetails.Count == 0)
                        errorDetails.Add(new ErrorDetails() { Problem = "N" }); 
                }
                catch
                {
                    errorDetails.Add(new ErrorDetails() { Problem = "N" });
                }

                sConn.Close();
            }

            return errorDetails;
        }
    }
}