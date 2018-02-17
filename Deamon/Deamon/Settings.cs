using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daemon
{
    public static class Settings
    { 
        public static string DaemonId { get; private set; }
        public static DateTime RunAt { get; set; }
        public static string SourcePath { get; set; } = @"C:\ProjektMMM\From\";
        public static string DestinationPath { get; set; } = @"C:\ProjektMMM\To\";
    }
}
