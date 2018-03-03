using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daemon
{
    /// <summary>
    /// Trida pro univerzalni funkce, pouzitelne kdekoliv
    /// </summary>
    public class Application
    {
        /// <summary>
        /// Vraci cislo indexu vybraneho znaku
        /// </summary>
        public static int IdentifyCharIndex(string input, char chr)
        {
            int index = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == chr)
                    index = i;
            }
            return index;
        }

        public static int CountChar(string input, char chr)
        {
            int count = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == chr)
                    count++;
            }

            return count;
        }

        public static int IdentifyLastCharIndex(string input, char chr)
        {
            int index = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == chr)
                    index = i;
            }

            return index;
        }
    }
}
