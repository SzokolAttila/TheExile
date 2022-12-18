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
            Lists.Items.Add(this);
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
        }

        public static void CheckPositions(Fighter fighter)
        {
            for (int i = 0; i < Lists.Items.Count; i++)
            {
                if (fighter.Pos[0] == Lists.Items[i].Pos[0] && fighter.Pos[1] == Lists.Items[i].Pos[1])
                {
                    Console.Write($"\nName:\t");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(Lists.Items[i].Name);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\nType:\t");
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write(Lists.Items[i].Type);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\nRarity:\t");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(Lists.Items[i].Rarity);
                    Console.ForegroundColor = ConsoleColor.White;
                    if (Lists.Items[i].HP > 0)
                    {
                        Console.Write("\nHP:\t");
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write(Lists.Items[i].HP);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    if (Lists.Items[i].Attack > 0)
                    {
                        Console.Write("\nAttack:\t");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write(Lists.Items[i].Attack);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    if (Lists.Items[i].Heal > 0)
                    {
                        Console.Write("\nHeal:\t");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(Lists.Items[i].Heal);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    if (Lists.Items[i].Range > 0)
                    {
                        Console.Write("\nRange:\t");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(Lists.Items[i].Range);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.Write("\nDescription:\t");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write(Lists.Items[i].Desc);
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
            for (int i = 0; i < Lists.Items.Count; i++)
            {
                if (fighter.Pos[0] == Lists.Items[i].Pos[0] && fighter.Pos[1] == Lists.Items[i].Pos[1])
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("The Exile\n");
                    Console.ForegroundColor = ConsoleColor.White;

                    fighter.Room[fighter.Pos[0], fighter.Pos[1]] = "X";
                    fighter.View(fighter.Room);

                    Console.WriteLine($"\n{Lists.Items[i].Name} successfully obtained.");

                    fighter.Attack += Lists.Items[i].Attack;
                    fighter.MaxHP += Lists.Items[i].HP;
                    fighter.Range += Lists.Items[i].Range;
                    fighter.Temp = "0";
                    if (Lists.Items[i].Type == "Consumable")
                    {
                        fighter.Consumables.Add(Lists.Items[i]);
                        fighter.ConsumableNames.Add(Lists.Items[i].Name);
                    }
                    else
                    {
                        fighter.Inventory.Add(Lists.Items[i]);
                        fighter.Names.Add(Lists.Items[i].Name);
                    }
                    Lists.Items.Remove(Lists.Items[i]);
                }
            }
        }

    }
}
