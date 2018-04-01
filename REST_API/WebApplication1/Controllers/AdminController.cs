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
        /// <summary>
        /// POST z admina o novych nastavenich
        /// Validace tokenu
        /// </summary>

        [Route("api/admin/form")]
        public bool Post(CommandInformation incommingCommand)
        {
            //Token tokenHandler = new Token();
            //bool token = tokenHandler.Verify(info[1]);
            //if (token == false)
            //    return false;

            //CommandInformation incommingCommand = JsonConvert.DeserializeObject<CommandInformation>(info[0]);

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

            return true;
        }

        /// <summary>
        /// Registrace nove uzivatele, vrací boolean - zatim
        /// ToDo: Prideleni tokenu
        /// </summary>
        [Route("api/admin/register")]
        public bool Post2(User user)
        {
            //User user = JsonConvert.DeserializeObject<User>(info);
            //Kontrola, zda existuje
            using (MySqlConnection sConn = new MySqlConnection(Sql.ConnectionString))
            {
                try
                {
                    string commandText = "SELECT * FROM tbUsers WHERE Username = @Username";
                    sConn.Open();
                    MySqlCommand command = new MySqlCommand(commandText, sConn);

                    command.Parameters.Add("@Username", MySqlDbType.VarChar);
                    command.Parameters.Add("@Password", MySqlDbType.VarChar);
                    command.Parameters.Add("@Email", MySqlDbType.VarChar);

                    command.Parameters["@Username"].Value = user.Username;
                    command.Parameters["@Password"].Value = user.Password;
                    command.Parameters["@Email"].Value = user.Email;

                    command.ExecuteNonQuery();
                    sConn.Close();
                }
                catch
                {
                    return false;
                }
            }
            string commandText2 = "INSERT INTO tbUsers(Username, Password, Email) VALUES(@Username,@Password,@Email)";

            using (MySqlConnection sConn = Sql.GetConnection())
            {
                sConn.Open();
                MySqlCommand command = new MySqlCommand(commandText2, sConn);

                command.Parameters.Add("@Username", MySqlDbType.VarChar);
                command.Parameters.Add("@Password", MySqlDbType.VarChar);
                command.Parameters.Add("@Email", MySqlDbType.VarChar);

                command.Parameters["@Username"].Value = user.Username;
                command.Parameters["@Password"].Value = user.Password;
                command.Parameters["@Email"].Value = user.Email;

                //Sql.sComm.ExecuteNonQuery();
                command.ExecuteNonQuery();

                sConn.Close();
            }

            return true;
        }

        /// <summary>
        /// Login a overeni noveho uzivatele, vrací boolean - zatim
        /// ToDo: Validace dle tokenu
        /// </summary>
        [Route("api/admin/login")]
        public bool Post(User user)
        {
            //User user = JsonConvert.DeserializeObject<User>(info);
            string commandText = "SELECT * FROM tbUsers WHERE Username=@Username AND Password=@Password";

            using (MySqlConnection sConn = Sql.GetConnection())
            {
                sConn.Open();
                MySqlCommand sComm = new MySqlCommand(commandText, sConn);
                sComm.Parameters.Add("@Username", MySqlDbType.VarChar);
                sComm.Parameters.Add("@Password", MySqlDbType.VarChar);

                sComm.Parameters["@Username"].Value = user.Username;
                sComm.Parameters["@Password"].Value = user.Password;

                MySqlDataReader sRead = sComm.ExecuteReader();

                int Count = 0;

                while (sRead.Read())
                {
                    Count++;
                }



                sConn.Close();
                if (Count == 1)
                {
                    return true;
                }
                else
                    return false;
            }
        }

        [Route("api/admin")]
        public List<BackupInformation> Get()
        {
            List<BackupInformation> data = new List<BackupInformation>();
            string commandText = "SELECT * FROM tbBackupReport";

            using (MySqlConnection sConn = Sql.GetConnection())
            {
                sConn.Open();
                MySqlCommand sComm = new MySqlCommand(commandText, sConn);
                MySqlDataReader sRead = sComm.ExecuteReader();

                while (sRead.Read())
                {
                    data.Add(new BackupInformation()
                    {
                        Date = Convert.ToDateTime(sRead["Date"]),
                        Size = Convert.ToInt32(sRead["Size"]),
                        Type = sRead["Type"].ToString()
                    });
                }
                sConn.Close();
            }

            return data;

        }
    }
}