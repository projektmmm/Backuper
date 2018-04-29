using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daemon
{
    public static class DaemonSettings
    {
        public const int Id = 1;
        public const int UserId = 1;
        public const string Name = "PC01";
        public const string ApiAdress = "http://localhost:63324/";

        public const int AskInterval = 50000; //50s
    }
}
