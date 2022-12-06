using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace TheExileBasic
{
    class Enemy
    {
        public static List<Enemy> Enemies = new List<Enemy>();
        public int HP { get; set; }
        public int AP { get; set; }
        public string Name { get; set; }
        public int XP { get; set; }
        public int[] Pos { get; set; }
        public bool IsKilled { get; set; }
        public Enemy(int hp, int ap, string name, int xp, int[] pos, string[,] room, bool isKilled = false)
        {
            Enemies.Add(this);
            this.HP = hp;
            this.AP = ap;
            this.XP = xp;
            this.Name = name;
            this.Pos = pos;
            this.IsKilled = isKilled;
            room[this.Pos[0], this.Pos[1]] = "!";
        }
        public static void CheckPositions(Fighter fighter)
        {
            for (int i = 0; i < Enemies.Count; i++)
            {
                if (!Enemies[i].IsKilled)
                {
                    if ((fighter.Pos[0] == Enemies[i].Pos[0] && fighter.Pos[1] - 1 == Enemies[i].Pos[1]) || (fighter.Pos[0] == Enemies[i].Pos[0] && fighter.Pos[1] + 1 == Enemies[i].Pos[1]) || (fighter.Pos[0] - 1 == Enemies[i].Pos[0] && fighter.Pos[1] == Enemies[i].Pos[1]) || (fighter.Pos[0] + 1 == Enemies[i].Pos[0] && fighter.Pos[1] == Enemies[i].Pos[1]))
                        Console.WriteLine("\nYou found yourself in front of a(n) " + Enemies[i].Name + " with " + Enemies[i].HP + " Health Points and " + Enemies[i].AP + " Attack Points.\nStep on the same field for combat.");
                    else if (fighter.Pos[0] == Enemies[i].Pos[0] && fighter.Pos[1] == Enemies[i].Pos[1])
                    {
                        int[] result = Enemies[i].Combat(fighter);
                        fighter.HP = result[0];
                        fighter.XP += result[1];
                        fighter.Temp = "0";
                        Enemies[i].IsKilled = true;
                        Enemies.Remove(Enemies[i]);
                        Console.Clear();
                    }
                }
            }
        }
        public int[] Combat(Fighter fighter)
        {
            fighter.Fought = true;
            int currentHP = this.HP;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("The Exile\n");
                Console.WriteLine("Now it's time to fight!");
                Console.WriteLine(this.Name + "\t\t\t\t" + "Your stats:\n" + currentHP + "/" + this.HP + "\t\t\t\t" + "Attack: " + fighter.Attack + "\n\t\t\t\t" + "HP: " + fighter.HP);
                Console.WriteLine("It's your turn");
                currentHP -= fighter.Attack;
                if (currentHP <= 0)
                {
                    Console.Clear();
                    Console.WriteLine("The Exile\n");
                    Console.WriteLine("Now it's time to fight!");
                    Console.WriteLine(this.Name + "\t\t\t\t" + "Your stats:\n" + currentHP + "/" + this.HP + "\t\t\t\t" + "Attack: " + fighter.Attack + "\n\t\t\t\t" + "HP: " + fighter.HP);
                    Console.WriteLine("You have won!\nPress any key to end this scene:");
                    Console.ReadKey();
                    return new int[] { fighter.HP, this.XP };
                }
                Thread.Sleep(1000);
                Console.Clear();
                Console.WriteLine("The Exile\n");
                Console.WriteLine("Now it's time to fight!");
                Console.WriteLine(this.Name + "\t\t\t\t" + "Your stats:\n" + currentHP + "/" + this.HP + "\t\t\t\t" + "Attack: " + fighter.Attack + "\n\t\t\t\t" + "HP: " + fighter.HP);
                Console.WriteLine("It's the enemy's turn");
                fighter.HP -= this.AP;
                if (fighter.HP <= 0)
                {
                    Console.Clear();
                    Console.WriteLine("The Exile\n");
                    Console.WriteLine("Now it's time to fight!");
                    Console.WriteLine(this.Name + "\t\t\t\t" + "Your stats:\n" + currentHP + "/" + this.HP + "\t\t\t\t" + "Attack: " + fighter.Attack + "\n\t\t\t\t" + "HP: " + fighter.HP);
                    Console.WriteLine("You have lost!\nPress any key to end this scene:");
                    Console.ReadKey();
                    return new int[] { fighter.HP, 0 };
                }
                Thread.Sleep(1000);
            }

            Console.WriteLine("Press any key to end this scene:");
            Console.ReadKey(true);
            return new int[] { fighter.Attack, this.XP };
        }
    }
    class Item
    {
        public static List<Item> Items = new List<Item>();
        public string Type { get; set; }
        public string Name { get; set; }
        public int Attack { get; set; }
        public int HP { get; set; }
        public int Heal { get; set; }
        public string Rarity { get; set; }
        public string Desc { get; set; }
        public int[] Pos { get; set; }
        public bool IsObtained { get; set; }

        public Item(string type, string name, string rarity, string desc, int[] pos, string[,] room, bool isObtained = false, int attack = 0, int hp = 0, int heal = 0)
        {
            Items.Add(this);
            this.Type = type;
            this.Name = name;
            this.Rarity = rarity;
            this.Desc = desc;
            this.Pos = pos;
            this.IsObtained = isObtained;
            this.Attack = attack;
            this.HP = hp;
            this.Heal = heal;
            room[this.Pos[0], this.Pos[1]] = "*";
        }

        public static void CheckPositions(Fighter fighter)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if ((fighter.Pos[0] == Items[i].Pos[0] && fighter.Pos[1] - 1 == Items[i].Pos[1]) || (fighter.Pos[0] == Items[i].Pos[0] && fighter.Pos[1] + 1 == Items[i].Pos[1]) || (fighter.Pos[0] - 1 == Items[i].Pos[0] && fighter.Pos[1] == Items[i].Pos[1]) || (fighter.Pos[0] + 1 == Items[i].Pos[0] && fighter.Pos[1] == Items[i].Pos[1]))
                    Console.WriteLine($"\nName:\t{Items[i].Name}\nType:\t{Items[i].Type}\nRarity:\t{Items[i].Rarity}\nAttack:\t{Items[i].Attack}\nHP:\t{Items[i].HP}\nHeal:\t{Items[i].Heal}\nDescription:\t{Items[i].Desc}\nStep on the same field to pick up.");
                else if (fighter.Pos[0] == Items[i].Pos[0] && fighter.Pos[1] == Items[i].Pos[1])
                {
                    Console.WriteLine("\nItem successfully obtained.");
                    fighter.Attack += Items[i].Attack;
                    fighter.MaxHP += Items[i].HP;
                    fighter.Temp = "0";
                    fighter.Inventory.Add(Items[i]);
                    fighter.Names.Add(Items[i].Name);
                    Items.Remove(Items[i]);
                }
            }
        }

    }

    class Fighter
    {
        public List<Item> Inventory { get; set; }
        public List<string> Names { get; set; }
        public string Temp { get; set; }
        public int Range { get; set; }
        public int[] Pos { get; set; }
        public int Attack { get; set; }
        public int HP { get; set; }
        public int MaxHP { get; set; }
        public int XP { get; set; }
        public bool Fought { get; set; }

        public Fighter(int range, int[] pos, int attack, int hp, int xp = 0)
        {
            this.Fought = false;
            this.Inventory = new List<Item>();
            this.Names = new List<string>();
            this.Range = range;
            this.Pos = pos;
            this.Attack = attack;
            this.HP = hp;
            this.XP = xp;
            this.MaxHP = hp;
        }

        public string[,] Move (string direction, string[,] room)
        {
            room[this.Pos[0], this.Pos[1]] = this.Temp;

            switch (direction)
            {
                case "s":
                    if (this.Pos[0] < room.GetLength(0) - 1 && room[this.Pos[0] + 1, this.Pos[1]] != "M" && (room[this.Pos[0] + 1, this.Pos[1]] != "~" || this.Names.Contains("Boat")) && room[this.Pos[0] + 1, this.Pos[1]] != " ")
                        this.Pos[0]++;
                    break;
                case "w":
                    if (this.Pos[0] > 0 && room[this.Pos[0] - 1, this.Pos[1]] != "M" && (room[this.Pos[0] - 1, this.Pos[1]] != "~" || this.Names.Contains("Boat")) && room[this.Pos[0] - 1, this.Pos[1]] != " ")
                        this.Pos[0]--;
                    break;
                case "a":
                    if (this.Pos[1] > 0 && room[this.Pos[0], this.Pos[1] - 1] != "M" && (room[this.Pos[0], this.Pos[1] - 1] != "~" || this.Names.Contains("Boat")) && room[this.Pos[0], this.Pos[1] - 1] != " ")
                        this.Pos[1]--;
                    break;
                case "d":
                    if (this.Pos[1] < room.GetLength(1) - 1 && room[this.Pos[0], this.Pos[1] + 1] != "M" && (room[this.Pos[0], this.Pos[1] + 1] != "~" || this.Names.Contains("Boat")) && room[this.Pos[0], this.Pos[1] + 1] != " ")
                        this.Pos[1]++;
                    break;
                default:
                    break;
            }

            Thread.Sleep(1);
            if (room[this.Pos[0], this.Pos[1]] == "~")
            {
                this.Temp = room[this.Pos[0], this.Pos[1]];
                room[this.Pos[0], this.Pos[1]] = "B";
            }
            else
            {
                this.Temp = room[this.Pos[0], this.Pos[1]];
                room[this.Pos[0], this.Pos[1]] = "X";
            }
            Console.Clear();

            return room;
        }

        public void View (string[,] room)
        {
            for (int i = -this.Range; i <= this.Range; i++)
            {
                for (int j = -this.Range; j <= this.Range; j++)
                {
                    if (this.Pos[0] + i >= 0 && this.Pos[0] + i < room.GetLength(0) && this.Pos[1] + j >= 0 && this.Pos[1] + j < room.GetLength(1) && this.Pos[1] + j < room.GetLength(1))
                    {
                        switch (room[this.Pos[0] + i, this.Pos[1] + j])
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
                            default:
                                Console.ForegroundColor = ConsoleColor.White;
                                break;
                        }
                        Console.Write(room[this.Pos[0] + i, this.Pos[1] + j]);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        public void Map(string[,] room)
        {
            for (int i = 0; i < room.GetLength(0); i++)
            {
                for (int j = 0; j < room.GetLength(1); j++)
                {
                    switch (room[i, j])
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
                        default:
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                    }
                    Console.Write(room[i, j]);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine();
            }
        }
    }
    
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
            Item dagger = new Item("Dagger", "Dagger", "Common", "A dull-edged dagger", new int[] { 6, 17 }, room, attack: 24);
            Item boat = new Item("Utility", "Boat", "Rare", "The key of exploration, a boat. Now, set sails and ride waves!", new int[] { 13, 47 }, room);
            Item potion = new Item("Consumable", "Heal", "Rare", "Replenishes a small amount of missing health; Cannot exceed max HP.", new int[] { 7, 19 }, room, heal: 200);
            Enemy golem = new Enemy(2480, 50, "Golem", 250, new int[] { 11, 23 }, room);
            Enemy ent = new Enemy(400, 60, "Ent", 100, new int[] { 13, 46 }, room);

            string input = "";
            do
            {
                Console.WriteLine("The Exile\n");

                if (input == "m")
                    fighter.Map(room);
                else
                {
                    if (input == "i")
                    {
                        Console.WriteLine($"Your stats:\nHP:\t{fighter.HP}\nAttack:\t{fighter.Attack}\nEXP:\t{fighter.XP}\nInventory:\t{String.Join(", ", fighter.Names)}");
                    }
                    else if (input == "h")
                    {
                        Console.WriteLine("Movement: \"w\" (up), \"a\" (left), \"s\" (down), \"d\" (right)");
                        Console.WriteLine("Open map: \"m\" ");
                        Console.WriteLine("Signs: \"M\" -> obstacle, \"*\" -> item, \"!\" -> enemy \"~\" -> water");
                        Console.WriteLine("Inventory / Stats: \"i\"");
                        Console.WriteLine("Use heal: \"u\"");
                    }
                    else if (input == "u")
                    {
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
                    else
                        Console.WriteLine("Press \"h\" for help");
                    Console.WriteLine();
                    fighter.View(room);
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

                input = Console.ReadKey(true).KeyChar.ToString();
                room = fighter.Move(input, room);

            } while (input != "x");

            sr.Close();
        }
    }
}
