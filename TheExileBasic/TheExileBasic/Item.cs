using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int Range { get; set; }
        public Item(string type, string name, string rarity, string desc, int[] pos, string[,] room, int attack = 0, int hp = 0, int heal = 0, int range = 0)
        {
            this.Type = type;
            this.Name = name;
            this.Rarity = rarity;
            this.Desc = desc;
            this.Pos = pos;
            this.Attack = attack;
            this.HP = hp;
            this.Range = range;
            this.Heal = heal;
            room[this.Pos[0], this.Pos[1]] = "*";
            for (int i = 0; i < Fighter.Fighters.Count; i++)
                Fighter.Fighters[i].Items.Add(this);
        }

        public static void CheckPositions()
        {
            for (int i = 0; i < Fighter.Fighters.Count; i++)
            {
                for (int j = 0; j < Fighter.Fighters[i].Items.Count; j++)
                {
                    if (Fighter.Fighters[i].Pos[0] == Fighter.Fighters[i].Items[j].Pos[0] && Fighter.Fighters[i].Pos[1] == Fighter.Fighters[i].Items[j].Pos[1])
                    {
                        Console.Write($"\nName:\t");
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write(Fighter.Fighters[i].Items[j].Name);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("\nType:\t");
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write(Fighter.Fighters[i].Items[j].Type);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("\nRarity:\t");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(Fighter.Fighters[i].Items[j].Rarity);
                        Console.ForegroundColor = ConsoleColor.White;
                        if (Fighter.Fighters[i].Items[j].HP > 0)
                        {
                            Console.Write("\nHP:\t");
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write(Fighter.Fighters[i].Items[j].HP);
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        if (Fighter.Fighters[i].Items[i].Attack > 0)
                        {
                            Console.Write("\nAttack:\t");
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write(Fighter.Fighters[i].Items[j].Attack);
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        if (Fighter.Fighters[i].Items[i].Heal > 0)
                        {
                            Console.Write("\nHeal:\t");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(Fighter.Fighters[i].Items[j].Heal);
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        if (Fighter.Fighters[i].Items[i].Range > 0)
                        {
                            Console.Write("\nRange:\t");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write(Fighter.Fighters[i].Items[j].Range);
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        Console.Write("\nDescription:\t");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write(Fighter.Fighters[i].Items[j].Desc);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("\nPress ");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("E");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" to pick up");
                    }
                }
            }
        }

        public static void PickUp ()
        {
            for (int i = 0; i < Fighter.Fighters.Count; i++)
            {
                for (int j = 0; j < Fighter.Fighters[i].Items.Count; j++)
                {
                    if (Fighter.Fighters[i].Pos[0] == Fighter.Fighters[i].Items[j].Pos[0] && Fighter.Fighters[i].Pos[1] == Fighter.Fighters[i].Items[j].Pos[1])
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("The Exile\n");
                        Console.ForegroundColor = ConsoleColor.White;

                        Room.Rooms[0][Fighter.Fighters[i].Pos[0], Fighter.Fighters[i].Pos[1]] = "X";
                        Fighter.Fighters[i].View(Room.Rooms[0]);

                        Console.WriteLine($"\n{Fighter.Fighters[i].Items[j].Name} successfully obtained.");

                        Fighter.Fighters[i].Attack += Fighter.Fighters[i].Items[j].Attack;
                        Fighter.Fighters[i].MaxHP += Fighter.Fighters[i].Items[j].HP;
                        Fighter.Fighters[i].Range += Fighter.Fighters[i].Items[j].Range;
                        Fighter.Fighters[i].Temp = "0";
                        if (Fighter.Fighters[i].Items[j].Type == "Consumable")
                        {
                            Fighter.Fighters[i].Consumables.Add(Fighter.Fighters[i].Items[j]);
                            Fighter.Fighters[i].ConsumableNames.Add(Fighter.Fighters[i].Items[j].Name);
                        }
                        else
                        {
                            Fighter.Fighters[i].Inventory.Add(Fighter.Fighters[i].Items[j]);
                            Fighter.Fighters[i].Names.Add(Fighter.Fighters[i].Items[j].Name);
                        }
                        Fighter.Fighters[i].Items.Remove(Fighter.Fighters[i].Items[j]);
                    }
                }
            }
        }

    }
}
