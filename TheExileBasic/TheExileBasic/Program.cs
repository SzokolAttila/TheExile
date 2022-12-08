using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TheExileBasic
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool help = false;
            bool map = false;
            bool inventory = false;
            string path = "map.txt";
            StreamReader sr = new StreamReader(path);
            int[] matrix = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
            int[] startPos = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

            Fighter fighter = new Fighter(3, startPos, 100, 1000);

            fighter.Room = new string[matrix[0], matrix[1]];
            string[] text = sr.ReadToEnd().Split('\n');
            
            for (int i = 0; i < matrix[0]; i++)
            {
                string temp = text[i].Replace("\r", "");
                for (int j = 0; j < matrix[1]; j++)
                {
                    if (j < temp.Length)
                        fighter.Room[i, j] = temp[j].ToString();
                    else fighter.Room[i, j] = " ";
                }
            }

            fighter.Temp = fighter.Room[fighter.Pos[0], fighter.Pos[1]];
            fighter.Room[fighter.Pos[0], fighter.Pos[1]] = "X";
            Item dagger = new Item("Weapon", "Dagger", "Common", "A dull-edged dagger", new int[] { 6, 17 }, fighter.Room, attack: 24);
            Item boat = new Item("Utility", "Boat", "Rare", "The key of exploration, a boat. Now, set sails and ride waves!", new int[] { 13, 47 }, fighter.Room);
            Item potion = new Item("Consumable", "Heal", "Rare", "Replenishes a small amount of missing health; Cannot exceed max HP.", new int[] { 7, 19 }, fighter.Room, heal: 200);
            Enemy golem = new Enemy(2480, 50, "Golem", 250, new int[] { 11, 23 }, fighter.Room);
            Enemy ent = new Enemy(400, 60, "Ent", 100, new int[] { 13, 46 }, fighter.Room);

            string input = "";
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("The Exile\n\n");
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Press ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("WASD");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" to start");

            do
            {
                input = Console.ReadKey(true).KeyChar.ToString();
                fighter.Room = fighter.Move(input, fighter.Room);

                if (input == "m")
                {
                    if (!map)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("The Exile\n");
                        Console.ForegroundColor = ConsoleColor.White;

                        fighter.Map(fighter.Room);
                        map = true;
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("The Exile\n");
                        Console.ForegroundColor = ConsoleColor.White;

                        Console.Write("Press ");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("H");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" for help\n\n");
                        fighter.View(fighter.Room);
                        map = false;
                    }
                }
                else
                {
                    if (input == "i")
                    {
                        if (!inventory)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("The Exile\n");
                            Console.ForegroundColor = ConsoleColor.White;

                            Console.WriteLine($"Your stats:\nHP:\t{fighter.HP} / {fighter.MaxHP}\nAttack:\t{fighter.Attack}\nEXP:\t{fighter.XP}\nInventory:\t{String.Join(", ", fighter.Names)}");
                            inventory = true;
                        }
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("The Exile\n");
                            Console.ForegroundColor = ConsoleColor.White;

                            Console.Write("Press ");
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write("H");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(" for help\n\n");
                            fighter.View(fighter.Room);
                            inventory = false;
                        }
                    }
                    else if (input == "h")
                    {
                        if (!help)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("The Exile\n");
                            Console.ForegroundColor = ConsoleColor.White;

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
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("!");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(" -> enemy ");
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.Write("~");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(" -> water\n");

                            Console.Write("Character signs: ");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write("X");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(" -> basic, ");
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write("B");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(" -> boat, ");
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            Console.Write("E");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(" -> enemy, ");
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.Write("I");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(" -> item\n");

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
                            help = true;
                        }
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("The Exile\n");
                            Console.ForegroundColor = ConsoleColor.White;

                            Console.Write("Press ");
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write("H");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(" for help\n\n");
                            fighter.View(fighter.Room);
                            help = false;
                        }
                    }
                    else if (input == "u")
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("The Exile\n");
                        Console.ForegroundColor = ConsoleColor.White;

                        if (fighter.Inventory.Contains(potion))
                        {
                            if (fighter.HP + potion.Heal > fighter.MaxHP)
                                fighter.HP = fighter.MaxHP;
                            else fighter.HP += potion.Heal;
                            Console.WriteLine($"Healed player for {potion.Heal} HP.\n");
                            fighter.Inventory.Remove(potion);
                            fighter.Names.Remove(potion.Name);
                        }
                        else Console.WriteLine("No heal potions available!\n");
                        fighter.View(fighter.Room);
                    }
                    else if (input == "e")
                    { 
                        Enemy.StartCombat(fighter);
                        Item.PickUp(fighter);
                        continue;

                    }
                    if (fighter.Moved)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("The Exile\n");
                        Console.ForegroundColor = ConsoleColor.White;

                        Console.Write("Press ");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("H");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" for help\n\n");

                        fighter.View(fighter.Room);
                        Enemy.CheckPositions(fighter);
                        Item.CheckPositions(fighter);
                        help = false;
                        inventory = false;
                        map = false;
                    }
                }
                
                if (fighter.HP <= 0)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("The Exile\n");
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"\nGame Over! You died with {fighter.XP} XP.");
                    break;
                }

            } while (input != "x");

            sr.Close();
        }
    }
}
