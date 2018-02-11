using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deamon
{
    class Program
    {
        static void Main(string[] args)
        {
            Backuper b = new Backuper();

            b.FullBackup();

            Console.ReadLine();
        }
    }
}
