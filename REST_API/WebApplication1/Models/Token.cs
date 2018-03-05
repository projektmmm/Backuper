using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace WebApplication1.Models
{
    public class Token
    {
        public int IdAdmin { get; set; }
        public string Password { get; set; }

        public Token()
        {
        }

        public bool Verify(int idadmin, string password)
        {
            this.IdAdmin = idadmin;
            this.Password = password;

            string commandText = "SELECT * FROM tbAdminTokens WHERE IdAdmin=@IdAdmin AND Password=@Password";
            bool ret = false;
            using (MySqlConnection sConn = Sql.GetConnection())
            {
                sConn.Open();
                MySqlCommand command = new MySqlCommand(commandText, sConn);

                command.Parameters.Add("@IdAdmin", MySqlDbType.Int32);
                command.Parameters.Add("@Password", MySqlDbType.VarChar);
                command.Parameters["@IdAdmin"].Value = this.IdAdmin;
                command.Parameters["@Password"].Value = this.Password;

                MySqlDataReader sRead = command.ExecuteReader();

                if (sRead.Read())
                {
                    if (Convert.ToInt32(sRead[0]) == this.IdAdmin && sRead[1].ToString() == this.Password)
                        ret = true;
                    else
                        ret = false;
                }
                else
                    ret = false;                

                sConn.Close();
            }

            return ret;
        }
    }
}