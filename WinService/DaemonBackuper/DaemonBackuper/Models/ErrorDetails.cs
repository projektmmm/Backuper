using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daemon
{
    public class ErrorDetails
    {
        public int AffectedFiles { get; set; } = -1;
        public string ProblemPath { get; set; }
        public string Problem { get; set; }
        public string Exception { get; set; }
        public bool Solved { get; set; } = false;
    }
}
