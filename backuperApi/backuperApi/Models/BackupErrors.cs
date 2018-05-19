using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backuperApi
{
    public class BackupErrors
    {
        public int Id { get; set; }
        public int BackupReportId { get; set; }
        public int AffectedFiles { get; set; }
        public string Problem { get; set; }
        public string ProblemPath { get; set; }
        public bool Solved { get; set; }
    }
}