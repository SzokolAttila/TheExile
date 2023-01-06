using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheExileBasic
{
    internal class Menus
    {
        public static void Help()
        {
            Console.Write("Movement: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("W");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" (up), ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("A");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" (left), ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("S");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" (down), ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("D");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" (right)\n");

            Console.Write("Open map: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("M\n");
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Map signs: ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("M");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" -> obstacle, ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("*");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" -> item, ");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("?");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" -> NPC, ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("!");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" -> enemy ");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("~");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" -> water, ");
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Write(" ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(" -> a quest's place\n");

            Console.Write("Character signs: ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("X");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" -> basic, ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("X");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" -> enemy, ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("X");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" -> item, ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("B");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" -> boat\n");

            Console.Write("Inventory / Stats: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("I\n");
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Interactions: (items, NPC-s) ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("E\n");
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Use heal: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("U\n");
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Press ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("X");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" to leave game");
        }
        public static void Header()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("The Exile\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Combat(string name, int mHP, int mMaxHP, int pHP, int pMaxHP, int attack, int mAttack)
        {
            Console.WriteLine("Now it's time to fight!");
            Console.WriteLine("\t" + name + "\t\t\t\t" + "You:\nAttack:\t" + mAttack + "\t\t\t\t"  + attack + "\nHP:\t" + mHP + "/" + mMaxHP + "\t\t\t\t" + pHP + "/" + pMaxHP +"\n");
        }

        public static void PressH()
        {
            Console.Write("Press ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("H");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" for help\n\n");
        }
    }
}
