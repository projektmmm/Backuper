using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Daemon
{
    class Program
    {
        static void Main(string[] args)
        {
            Backuper b = new Backuper();

            b.FullBackup();

            //b.DifferentialBackup();

            Console.ReadLine();
        }
    }
}
