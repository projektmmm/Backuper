using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaemonBackuper.Models
{
    public class Databases
    {
        public int Id { get; set; }
        public string ServerName { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int DaemonId { get; set; }
    }
}
