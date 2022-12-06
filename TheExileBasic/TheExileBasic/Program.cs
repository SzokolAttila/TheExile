using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace TheExileBasic
{
    class Item
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public int Attack { get; set; }
        public int HP { get; set; }
        public string Rarity { get; set; }
        public string Desc { get; set; }
        public int[] Pos { get; set; }
        public bool IsObtained { get; set; }

        public string[,] Place(string[,] room)
        {
            room[this.Pos[0], this.Pos[1]] = "*";
            return room;
        }
        public Item(string type, string name, string rarity, string desc, int[] pos, bool isObtained = false, int attack = 0, int hp = 0)
        {
            this.Type = type;
            this.Name = name;
            this.Rarity = rarity;
            this.Desc = desc;
            this.Pos = pos;
            this.IsObtained = isObtained;
            this.Attack = attack;
            this.HP = hp;
        }

    }
    class Fighter
    {
        public string Temp { get; set; }
        public int Range { get; set; }
        public int[] Pos { get; set; }
        public int Attack { get; set; }
        public int HP { get; set; }
        public int XP { get; set; }

        public Fighter(int range, int[] pos, int attack, int hp, int xp = 0)
        {
            this.Range = range;
            this.Pos = pos;
            this.Attack = attack;
            this.HP = hp;
            this.XP = xp;
        }

        public string[,] Move (string direction, string[,] room)
        {
            room[this.Pos[0], this.Pos[1]] = this.Temp;

            switch (direction)
            {
                case "s":
                    if (this.Pos[0] < room.GetLength(0) - 1 && room[this.Pos[0] + 1, this.Pos[1]] != "M" && room[this.Pos[0] + 1, this.Pos[1]] != " ")
                        this.Pos[0]++;
                    break;
                case "w":
                    if (this.Pos[0] > 0 && room[this.Pos[0] - 1, this.Pos[1]] != "M" && room[this.Pos[0] - 1, this.Pos[1]] != " ")
                        this.Pos[0]--;
                    break;
                case "a":
                    if (this.Pos[1] > 0 && room[this.Pos[0], this.Pos[1] - 1] != "M" && room[this.Pos[0], this.Pos[1] - 1] != " ")
                        this.Pos[1]--;
                    break;
                case "d":
                    if (this.Pos[1] < room.GetLength(1) - 1 && room[this.Pos[0], this.Pos[1] + 1] != "M" && room[this.Pos[0], this.Pos[1] + 1] != " ")
                        this.Pos[1]++;
                    break;
                default:
                    break;
            }

            Thread.Sleep(1);
            this.Temp = room[this.Pos[0], this.Pos[1]];
            room[this.Pos[0], this.Pos[1]] = "X";
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

    class Enemy
    {
        public int HP { get; set; }
        public int AP { get; set; }
        public string Name { get; set; }
        public int XP { get; set; }
        public int[] Pos { get; set; }
        public bool IsKilled { get; set; }
        public Enemy(int hp, int ap, string name, int xp, int[] pos, bool isKilled = false)
        {
            this.HP = hp;
            this.AP = ap;
            this.XP = xp;
            this.Name = name;
            this.Pos = pos;
            this.IsKilled = isKilled;
        }
        public string[,] Place(string[,] room)
        {
            room[this.Pos[0], this.Pos[1]] = "!";
            return room;
        }
        public int[] Combat(int fighterHp, int fighterAttack)
        {
            int currentHP = this.HP;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("The Exile\n");
                Console.WriteLine("Now it's time to fight!");
                Console.WriteLine(this.Name + "\t\t\t\t" + "Your stats:\n" + currentHP + "/" + this.HP + "\t\t\t\t" + "Attack: " + fighterAttack + "\n\t\t\t\t" + "HP: " + fighterHp);
                Console.WriteLine("It's your turn");
                currentHP -= fighterAttack;
                if (currentHP <= 0)
                {
                    Console.Clear();
                    Console.WriteLine("The Exile\n");
                    Console.WriteLine("Now it's time to fight!");
                    Console.WriteLine(this.Name + "\t\t\t\t" + "Your stats:\n" + currentHP + "/" + this.HP + "\t\t\t\t" + "Attack: " + fighterAttack + "\n\t\t\t\t" + "HP: " + fighterHp);
                    Console.WriteLine("You have won!\nPress any key to end this scene:");
                    Console.ReadKey();
                    return new int[] { fighterHp, this.XP };
                }
                Thread.Sleep(1000);
                Console.Clear();
                Console.WriteLine("The Exile\n");
                Console.WriteLine("Now it's time to fight!");
                Console.WriteLine(this.Name + "\t\t\t\t" + "Your stats:\n" + currentHP + "/" + this.HP + "\t\t\t\t" + "Attack: " + fighterAttack + "\n\t\t\t\t" + "HP: " + fighterHp);
                Console.WriteLine("It's the enemy's turn");
                fighterHp -= this.AP;
                if (fighterHp <= 0)
                {
                    Console.Clear();
                    Console.WriteLine("The Exile\n");
                    Console.WriteLine("Now it's time to fight!");
                    Console.WriteLine(this.Name + "\t\t\t\t" + "Your stats:\n" + currentHP + "/" + this.HP + "\t\t\t\t" + "Attack: " + fighterAttack + "\n\t\t\t\t" + "HP: " + fighterHp);
                    Console.WriteLine("You have lost!\nPress any key to end this scene:");
                    Console.ReadKey();
                    return new int[] { fighterHp, 0 };
                }
                Thread.Sleep(1000);
            }

            Console.WriteLine("Press any key to end this scene:");
            Console.ReadKey(true);
            return new int[] { fighterHp, this.XP };
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
            Item dagger = new Item("Dagger", "Dagger", "Common", "A dull-edged dagger", new int[] {9, 39}, attack: 24);
            Enemy ent = new Enemy(400, 60, "Ent", 100, new int[] {13, 47});

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
            dagger.Place(room);
            ent.Place(room);

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
                        Console.WriteLine($"Your stats:\nHP:\t{fighter.HP}\nAttack:\t{fighter.Attack}\nEXP:\t{fighter.XP}");
                    }
                    else if (input == "h")
                    {
                        Console.WriteLine("Movement: \"w\" (up), \"a\" (left), \"s\" (down), \"d\" (right)");
                        Console.WriteLine("Open map: \"m\" ");
                        Console.WriteLine("Signs: \"M\" -> obstacle, \" * \" -> item, \"!\" -> enemy");
                        Console.WriteLine("Inventory / Stats: \"i\"");
                    }
                    else
                        Console.WriteLine("Press \"h\" for help");
                    Console.WriteLine();
                    fighter.View(room);
                }

                if (!ent.IsKilled)
                {
                    if ((fighter.Pos[0] == ent.Pos[0] && fighter.Pos[1] - 1 == ent.Pos[1]) || (fighter.Pos[0] == ent.Pos[0] && fighter.Pos[1] + 1 == ent.Pos[1]) || (fighter.Pos[0] - 1 == ent.Pos[0] && fighter.Pos[1] == ent.Pos[1]) || (fighter.Pos[0] + 1 == ent.Pos[0] && fighter.Pos[1] == ent.Pos[1]))
                        Console.WriteLine("\nYou found yourself in front of a(n) " + ent.Name + " with " + ent.HP + " Health Points and " + ent.AP + " Attack Points.\nStep on the same field for combat.");
                    else if (fighter.Temp == "!")
                    {
                        int[] result = ent.Combat(fighter.HP, fighter.Attack);
                        fighter.HP = result[0];
                        fighter.XP += result[1];
                        fighter.Temp = "0";
                        ent.IsKilled = true;
                        Console.Clear();
                        continue;
                    }
                }
                if (!dagger.IsObtained)
                {
                    if ((fighter.Pos[0] == dagger.Pos[0] && fighter.Pos[1] - 1 == dagger.Pos[1]) || (fighter.Pos[0] == dagger.Pos[0] && fighter.Pos[1] + 1 == dagger.Pos[1]) || (fighter.Pos[0] - 1 == dagger.Pos[0] && fighter.Pos[1] == dagger.Pos[1]) || (fighter.Pos[0] + 1 == dagger.Pos[0] && fighter.Pos[1] == dagger.Pos[1]))
                        Console.WriteLine($"\nName:\t{dagger.Name}\nType:\t{dagger.Type}\nRarity:\t{dagger.Rarity}\nAttack:\t{dagger.Attack}\nHP:\t{dagger.HP}\nDescription:\t{dagger.Desc}\nStep on the same field to pick up.");
                    else if (fighter.Temp == "*")
                    {
                        Console.WriteLine("\nItem successfully obtained.");
                        fighter.Attack += dagger.Attack;
                        fighter.HP += dagger.HP;
                        fighter.Temp = "0";
                        dagger.IsObtained = true;
                    }
                }
                
                input = Console.ReadKey(true).KeyChar.ToString();
                room = fighter.Move(input, room);

            } while (input != "x");

            sr.Close();
        }
    }
}
