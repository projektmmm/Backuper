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
            List<FileInformation> fileinfo = JsonConvert.DeserializeObject<List<FileInformation>>(info[0]);
            BackupInformation backupinfo = JsonConvert.DeserializeObject<BackupInformation>(info[1]);


            Sql.Open();
            foreach (FileInformation item in fileinfo)
            {
                Sql.SetCommand($"INSERT INTO tbFiles(Backup, Path, Name, Date, Size) VALUES('0', '{item.Path}', '{item.Name}', '{item.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss.fff")}', '{item.Size}')");
                Sql.sComm.ExecuteNonQuery();
            }

            Sql.SetCommand($"INSERT INTO tbBackup(Date, Type, Size) VALUES('{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}','{backupinfo.Type}','{backupinfo.Size}')");
            Sql.sComm.ExecuteNonQuery();
            Sql.Close();
        }
    }
}