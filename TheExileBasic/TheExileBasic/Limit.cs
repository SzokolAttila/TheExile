using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheExileBasic
{
    internal class Limit
    {
        public static int Check(int min, int max, int value)
        {
            if (value > max)
                return max;
            else if (value < min)
                return min;
            else return value;
        }
    }
}
