using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daemon
{
    class Program
    {
        static void Main(string[] args)
        {
            Communicator cm = new Communicator();
            cm.GetNextRunSetting();

            Console.ReadLine();
        }
    }
}
