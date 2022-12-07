using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TheExileBasic
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "map.txt";
            StreamReader sr = new StreamReader(path);
            int[] matrix = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
            int[] startPos = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

            Fighter fighter = new Fighter(3, startPos, 100, 1000);

            string[,] room = new string[matrix[0], matrix[1]];
            string[] text = sr.ReadToEnd().Split('\n');
            
            for (int i = 0; i < matrix[0]; i++)
            {
                string temp = text[i].Replace("\r", "");
                for (int j = 0; j < matrix[1]; j++)
                {
                    if (j < temp.Length)
                        room[i, j] = temp[j].ToString();
                    else room[i, j] = " ";
                }
            }

            fighter.Temp = room[fighter.Pos[0], fighter.Pos[1]];
            room[fighter.Pos[0], fighter.Pos[1]] = "X";
            Item dagger = new Item("Weapon", "Dagger", "Common", "A dull-edged dagger", new int[] { 6, 17 }, room, attack: 24);
            Item boat = new Item("Utility", "Boat", "Rare", "The key of exploration, a boat. Now, set sails and ride waves!", new int[] { 13, 47 }, room);
            Item potion = new Item("Consumable", "Heal", "Rare", "Replenishes a small amount of missing health; Cannot exceed max HP.", new int[] { 7, 19 }, room, heal: 200);
            Enemy golem = new Enemy(2480, 50, "Golem", 250, new int[] { 11, 23 }, room);
            Enemy ent = new Enemy(400, 60, "Ent", 100, new int[] { 13, 46 }, room);

            string input = ""; 
            Console.WriteLine("The Exile\n\nPress \"wasd\" key to start");
            do
            {
                input = Console.ReadKey(true).KeyChar.ToString();
                room = fighter.Move(input, room);

                if (input == "m")
                {
                    Console.Clear();
                    Console.WriteLine("The Exile\n"); 
                    fighter.Map(room);
                }
                else
                {
                    if (input == "i")
                    {
                        Console.Clear();
                        Console.WriteLine("The Exile\n");
                        Console.WriteLine($"Your stats:\nHP:\t{fighter.HP} / {fighter.MaxHP}\nAttack:\t{fighter.Attack}\nEXP:\t{fighter.XP}\nInventory:\t{String.Join(", ", fighter.Names)}");
                    }
                    else if (input == "h")
                    { 
                        Console.Clear();
                        Console.WriteLine("The Exile\n");
                        Console.WriteLine("Movement: \"w\" (up), \"a\" (left), \"s\" (down), \"d\" (right)");
                        Console.WriteLine("Open map: \"m\" ");
                        Console.WriteLine("Signs: \"M\" -> obstacle, \"*\" -> item, \"!\" -> enemy \"~\" -> water");
                        Console.WriteLine("Inventory / Stats: \"i\"");
                        Console.WriteLine("Use heal: \"u\"");
                    }
                    else if (input == "u")
                    {
                        Console.Clear();
                        Console.WriteLine("The Exile\n");
                        if (fighter.Inventory.Contains(potion))
                        {
                            if (fighter.HP + potion.Heal > fighter.MaxHP)
                                fighter.HP = fighter.MaxHP;
                            else fighter.HP += potion.Heal;
                            Console.WriteLine($"Healed player for {potion.Heal} HP.");
                            fighter.Inventory.Remove(potion);
                            fighter.Names.Remove(potion.Name);
                        }
                        else Console.WriteLine("No heal potions available!");
                    }
                    if (fighter.Moved)
                    {
                        Console.WriteLine("The Exile\n");
                        Console.WriteLine("Press \"h\" for help");
                        Console.WriteLine();
                        fighter.View(room);
                    }
                }

                Enemy.CheckPositions(fighter);
                Item.CheckPositions(fighter);
                if (fighter.Fought)
                {
                    Console.Clear();
                    fighter.Fought = false;
                    continue;
                }

                if (fighter.HP <= 0)
                {
                    Console.Clear();
                    Console.WriteLine("The Exile");
                    Console.WriteLine($"\nGame Over! You died with {fighter.XP} XP.");
                    break;
                }

            } while (input != "x");

            sr.Close();
        }
    }
}
