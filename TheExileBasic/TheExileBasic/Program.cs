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
        public int Heal { get; set; }
        public string Rarity { get; set; }
        public string Desc { get; set; }
        public int[] Pos { get; set; }
        public bool IsObtained { get; set; }

        public string[,] Place(string[,] room)
        {
            room[this.Pos[0], this.Pos[1]] = "*";
            return room;
        }
        public Item(string type, string name, string rarity, string desc, int[] pos, bool isObtained = false, int attack = 0, int hp = 0, int heal = 0)
        {
            this.Type = type;
            this.Name = name;
            this.Rarity = rarity;
            this.Desc = desc;
            this.Pos = pos;
            this.IsObtained = isObtained;
            this.Attack = attack;
            this.HP = hp;
            this.Heal = heal;
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

        public Fighter(int range, int[] pos, int attack, int hp, int xp = 0)
        {
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
            Item dagger = new Item("Dagger", "Dagger", "Common", "A dull-edged dagger", new int[] {6, 17}, attack: 24);
            Item boat = new Item("Utility", "Boat", "Rare", "The key of exploration, a boat. Now, set sails and ride waves!", new int[] { 13, 47 });
            Item potion = new Item("Consumable", "Heal", "Rare", "Restores missing health", new int[] { 7, 19 }, heal: fighter.MaxHP);
            Enemy golem = new Enemy(2480, 50, "Golem", 250, new int[] { 11, 23 });
            Enemy ent = new Enemy(400, 60, "Ent", 100, new int[] {13, 46});

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
            boat.Place(room);
            potion.Place(room);
            ent.Place(room);
            golem.Place(room);

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
                    }
                    else
                        Console.WriteLine("Press \"h\" for help");
                    Console.WriteLine();
                    fighter.View(room);
                }

                if (!golem.IsKilled)
                {
                    if ((fighter.Pos[0] == golem.Pos[0] && fighter.Pos[1] - 1 == golem.Pos[1]) || (fighter.Pos[0] == golem.Pos[0] && fighter.Pos[1] + 1 == golem.Pos[1]) || (fighter.Pos[0] - 1 == golem.Pos[0] && fighter.Pos[1] == golem.Pos[1]) || (fighter.Pos[0] + 1 == golem.Pos[0] && fighter.Pos[1] == golem.Pos[1]))
                        Console.WriteLine("\nYou found yourself in front of a(n) " + golem.Name + " with " + golem.HP + " Health Points and " + golem.AP + " Attack Points.\nStep on the same field for combat.");
                    else if (fighter.Pos[0] == golem.Pos[0] && fighter.Pos[1] == golem.Pos[1])
                    {
                        int[] result = golem.Combat(fighter.HP, fighter.Attack);
                        fighter.HP = result[0];
                        fighter.XP += result[1];
                        fighter.Temp = "0";
                        golem.IsKilled = true;
                        Console.Clear();
                        continue;
                    }
                }
                if (!ent.IsKilled)
                {
                    if ((fighter.Pos[0] == ent.Pos[0] && fighter.Pos[1] - 1 == ent.Pos[1]) || (fighter.Pos[0] == ent.Pos[0] && fighter.Pos[1] + 1 == ent.Pos[1]) || (fighter.Pos[0] - 1 == ent.Pos[0] && fighter.Pos[1] == ent.Pos[1]) || (fighter.Pos[0] + 1 == ent.Pos[0] && fighter.Pos[1] == ent.Pos[1]))
                        Console.WriteLine("\nYou found yourself in front of a(n) " + ent.Name + " with " + ent.HP + " Health Points and " + ent.AP + " Attack Points.\nStep on the same field for combat.");
                    else if (fighter.Pos[0] == ent.Pos[0] && fighter.Pos[1] == ent.Pos[1])
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
                        Console.WriteLine($"\nName:\t{dagger.Name}\nType:\t{dagger.Type}\nRarity:\t{dagger.Rarity}\nAttack:\t{dagger.Attack}\nHP:\t{dagger.HP}\nHeal:\t{dagger.Heal}\nDescription:\t{dagger.Desc}\nStep on the same field to pick up.");
                    else if (fighter.Pos[0] == dagger.Pos[0] && fighter.Pos[1] == dagger.Pos[1])
                    {
                        Console.WriteLine("\nItem successfully obtained.");
                        fighter.Attack += dagger.Attack;
                        fighter.MaxHP += dagger.HP;
                        fighter.HP += dagger.Heal;
                        fighter.Temp = "0";
                        dagger.IsObtained = true;
                        fighter.Inventory.Add(dagger);
                        fighter.Names.Add(dagger.Name);
                    }
                }
                if (!potion.IsObtained)
                {
                    if ((fighter.Pos[0] == potion.Pos[0] && fighter.Pos[1] - 1 == potion.Pos[1]) || (fighter.Pos[0] == potion.Pos[0] && fighter.Pos[1] + 1 == potion.Pos[1]) || (fighter.Pos[0] - 1 == potion.Pos[0] && fighter.Pos[1] == potion.Pos[1]) || (fighter.Pos[0] + 1 == potion.Pos[0] && fighter.Pos[1] == potion.Pos[1]))
                        Console.WriteLine($"\nName:\t{potion.Name}\nType:\t{potion.Type}\nRarity:\t{potion.Rarity}\nAttack:\t{potion.Attack}\nHP:\t{potion.HP}\nHeal:\t{potion.Heal}\nDescription:\t{potion.Desc}\nStep on the same field to pick up.");
                    else if (fighter.Pos[0] == potion.Pos[0] && fighter.Pos[1] == potion.Pos[1])
                    {
                        Console.WriteLine("\nItem successfully obtained.");
                        fighter.Attack += potion.Attack;
                        fighter.MaxHP += potion.HP;
                        fighter.HP += potion.Heal - fighter.HP;
                        fighter.Temp = "0";
                        potion.IsObtained = true;
                        fighter.Inventory.Add(potion);
                        fighter.Names.Add(potion.Name);
                    }
                }
                if (!boat.IsObtained)
                {
                    if ((fighter.Pos[0] == boat.Pos[0] && fighter.Pos[1] - 1 == boat.Pos[1]) || (fighter.Pos[0] == boat.Pos[0] && fighter.Pos[1] + 1 == boat.Pos[1]) || (fighter.Pos[0] - 1 == boat.Pos[0] && fighter.Pos[1] == boat.Pos[1]) || (fighter.Pos[0] + 1 == boat.Pos[0] && fighter.Pos[1] == boat.Pos[1]))
                        Console.WriteLine($"\nName:\t{boat.Name}\nType:\t{boat.Type}\nRarity:\t{boat.Rarity}\nAttack:\t{boat.Attack}\nHP:\t{boat.HP}\nHeal:\t{boat.Heal}\nDescription:\t{boat.Desc}\nStep on the same field to pick up.");
                    else if (fighter.Pos[0] == boat.Pos[0] && fighter.Pos[1] == boat.Pos[1])
                    {
                        Console.WriteLine("\nItem successfully obtained.");
                        fighter.Attack += boat.Attack;
                        fighter.MaxHP += boat.HP;
                        fighter.HP += boat.Heal;
                        fighter.Temp = "0";
                        boat.IsObtained = true;
                        fighter.Inventory.Add(boat);
                        fighter.Names.Add(boat.Name);
                    }
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
