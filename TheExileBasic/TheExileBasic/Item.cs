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
}
