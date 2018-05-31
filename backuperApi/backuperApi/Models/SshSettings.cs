using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backuperApi
{
    public class SSHSettings
    {
        public int Id { get; set; }
        public int BackupId { get; set; }
        public string HostOrIp { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}