using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheExileBasic
{
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

        public Item(string type, string name, string rarity, string desc, int[] pos, string[,] room, int attack = 0, int hp = 0, int heal = 0)
        {
            Items.Add(this);
            this.Type = type;
            this.Name = name;
            this.Rarity = rarity;
            this.Desc = desc;
            this.Pos = pos;
            this.Attack = attack;
            this.HP = hp;
            this.Heal = heal;
            room[this.Pos[0], this.Pos[1]] = "*";
        }

        public static void CheckPositions(Fighter fighter)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (fighter.Pos[0] == Items[i].Pos[0] && fighter.Pos[1] == Items[i].Pos[1])
                {
                    Console.Write($"\nName:\t");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(Items[i].Name);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\nType:\t");
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write(Items[i].Type);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\nRarity:\t");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(Items[i].Rarity);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\nAttack:\t");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write(Items[i].Attack);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\nHP:\t");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(Items[i].HP);
                    Console.ForegroundColor = ConsoleColor. White;
                    Console.Write("\nHeal:\t");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(Items[i].Heal);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\nDescription:\t");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write(Items[i].Desc);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\nPress ");
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("E");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" to pick up");
                }
            }
        }

        public static void PickUp (Fighter fighter)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (fighter.Pos[0] == Items[i].Pos[0] && fighter.Pos[1] == Items[i].Pos[1])
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("The Exile\n");
                    Console.ForegroundColor = ConsoleColor.White;

                    fighter.Room[fighter.Pos[0], fighter.Pos[1]] = "X";
                    fighter.View(fighter.Room);

                    Console.WriteLine($"\n{Items[i].Name} successfully obtained.");

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
}
