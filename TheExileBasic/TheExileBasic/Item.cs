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
    }
}
