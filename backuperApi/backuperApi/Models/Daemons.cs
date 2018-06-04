using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backuperApi
{
    public class Daemons
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DaemonPassword { get; set; }
        public bool Installed { get; set; }
        public bool Verified { get; set; }
        public bool SendSMS { get; set; }
        public bool SendEmail { get; set; }
    
    }
}