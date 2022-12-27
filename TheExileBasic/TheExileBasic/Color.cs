using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheExileBasic
{
    internal class Color
    {
        public static void PickColor (string currChar)
        {
            switch (currChar)
            {
                case "X":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case "0":
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case "M":
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                case "!":
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case "*":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case "~":
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    break;
                case "B":
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case "N":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "E":
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    break;
                case "I":
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    break;
                case "?":
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }

    }
}
