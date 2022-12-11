using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

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

            Fighter fighter = new Fighter(3, startPos, 100, 1000)
            {
                Room = new string[matrix[0], matrix[1]]
            };
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
            NPC jani = new NPC("Jani", "enemy", "The Golem keeps my village in fear, please defend us from it!", fighter.Room, new int[] { 5, 5 }, 1000, questEnemy: golem);
            NPC erwin = new NPC("Erwin","place", "By the way...you'd better visit that isle!", fighter.Room, new int[] {13, 8}, 300, new int[] {5, 16}, new int[] {7,19});
            Item glasses = new Item("Utility", "A pair of glasses", "Uncommon", "To see further than the tip of your toes",  new int[] { 31, 5 }, fighter.Room, range: 1); ;
            
            char input;
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
                input = Char.ToLower(Convert.ToChar(Console.ReadKey(true).KeyChar));
                fighter.Room = fighter.Move(input, fighter.Room);

                if (input == 'm')
                {
                    if (!map)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("The Exile\n");
                        Console.ForegroundColor = ConsoleColor.White;

                        fighter.Map(fighter.Room);
                        map = true;
                        help = false;
                        inventory = false;
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
                        Enemy.CheckPositions(fighter);
                        Item.CheckPositions(fighter);
                        NPC.CheckPositions(fighter);
                        map = false;
                    }
                }
                else
                {
                    if (input == 'i')
                    {
                        if (!inventory)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("The Exile\n");
                            Console.ForegroundColor = ConsoleColor.White;

                            Console.WriteLine($"Your stats:\nHP:\t{fighter.HP} / {fighter.MaxHP}\nAttack:\t{fighter.Attack}\nEXP:\t{fighter.XP}\nInventory:\t{String.Join(", ", fighter.Names)}\nConsumables:\t{String.Join(", ", fighter.ConsumableNames)}");
                            inventory = true;
                            map = false;
                            help = false;
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
                            Enemy.CheckPositions(fighter);
                            Item.CheckPositions(fighter);
                            NPC.CheckPositions(fighter);
                            inventory = false;
                        }
                    }
                    else if (input == 'h')
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
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write("B");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(" -> boat, ");
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            Console.Write("E");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(" -> enemy, ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("N");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(" -> NPC, ");
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
                            map = false;
                            inventory = false;
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
                            Enemy.CheckPositions(fighter);
                            Item.CheckPositions(fighter);
                            NPC.CheckPositions(fighter);
                            help = false;
                        }
                    }
                    else if (input == 'u')
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("The Exile\n");
                        Console.ForegroundColor = ConsoleColor.White;

                        if (fighter.Consumables.Contains(potion))
                        {
                            if (fighter.HP + potion.Heal > fighter.MaxHP)
                                fighter.HP = fighter.MaxHP;
                            else fighter.HP += potion.Heal;
                            Console.WriteLine($"Healed player for {potion.Heal} HP.\n");
                            fighter.Consumables.Remove(potion);
                            fighter.ConsumableNames.Remove(potion.Name);
                        }
                        else Console.WriteLine("No heal potions available!\n");

                        fighter.View(fighter.Room);
                        Enemy.CheckPositions(fighter);
                        Item.CheckPositions(fighter);
                        NPC.CheckPositions(fighter);
                    }
                    else if (input == 'e')
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
                        NPC.CheckPositions(fighter);
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
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                }
                NPC.CheckQuest(fighter);

            } while (input != 'x');

            Console.ReadKey(true);
            sr.Close();
        }
    }
}
