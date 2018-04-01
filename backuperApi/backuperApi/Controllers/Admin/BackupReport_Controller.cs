using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;

namespace backuperApi.Controllers
{
    public class BackupReport_Controller : ApiController
    {
        public Database backupReport = new Database();

        [HttpGet]
        [Route("api/admin/backup-reports")]
        public List<BackupReport> Get()
        {
            return this.backupReport.BackupReport.ToList();
        }
    }
}