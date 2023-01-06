using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheExileBasic
{
    internal class Input
    {
        public static void ClearKeyBuffer()
        {
            while (Console.KeyAvailable)
                Console.ReadKey(true);
        }
    }
}
