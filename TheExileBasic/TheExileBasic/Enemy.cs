using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TheExileBasic
{
    class Enemy
    {
        public int HP { get; set; }
        public int AP { get; set; }
        public string Name { get; set; }
        public int XP { get; set; }
        public int[] Pos { get; set; }
        public Enemy(int hp, int ap, string name, int xp, int[] pos, string[,] room)
        {
            this.HP = hp;
            this.AP = ap;
            this.XP = xp;
            this.Name = name;
            this.Pos = pos;
            room[this.Pos[0], this.Pos[1]] = "!";
            for (int i = 0; i < Fighter.Fighters.Count; i++)
                Fighter.Fighters[i].Enemies.Add(this);
        }
   
        public int[] Combat(Fighter fighter)
        {
            int currentHP = this.HP;
            int wait;

            if (fighter.HP/this.AP>this.HP/fighter.Attack)
                wait = 5000 / (this.HP / fighter.Attack);
            else wait = 5000 / (fighter.HP / this.AP);

            while (true)
            {
                Menus.Header();
                Menus.Combat(this.Name, currentHP, this.HP, fighter.HP, fighter.MaxHP, fighter.Attack, this.AP);
                Console.WriteLine("It's your turn.");

                currentHP -= fighter.Attack;
                if (currentHP <= 0)
                {
                    Menus.Header();
                    Menus.Combat(this.Name, currentHP, this.HP, fighter.HP, fighter.MaxHP, fighter.Attack, this.AP);
                    Console.WriteLine("You have won!");

                    return new int[] { fighter.HP, this.XP };
                }
                Thread.Sleep(wait);

                Menus.Header();
                Menus.Combat(this.Name, currentHP, this.HP, fighter.HP, fighter.MaxHP, fighter.Attack, this.AP);
                Console.WriteLine($"It's the {this.Name}'s turn.");

                fighter.HP -= this.AP;
                if (fighter.HP <= 0)
                {
                    Menus.Header();
                    Menus.Combat(this.Name, currentHP, this.HP, fighter.HP, fighter.MaxHP, fighter.Attack, this.AP);
                    Console.WriteLine("You have lost!");

                    return new int[] { fighter.HP, 0 };
                }
                Thread.Sleep(wait);
            }
        }
    }
}
