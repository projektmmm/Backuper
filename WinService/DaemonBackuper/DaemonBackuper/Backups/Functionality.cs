using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daemon
{
    public static class Functionality
    {
        public static int IdentifyCharPosition(char chr, string text)
        {
            int position = 0;
            int i = 0;
            foreach (char item in text)
            {
                i++;
                if (item == chr)
                    position = i;
            }

            return position;
        }
    }
}
