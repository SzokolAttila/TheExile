using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheExileBasic
{
    internal class LevelSystem
    {
        public const int Levels = 10;
        public static int[] LevelCap = new int[Levels] {100, 200, 500, 1000, 1500, 2000, 2500, 3000, 3500, 4000};

        public static void CheckLevel (Fighter fighter)
        {
            while (fighter.XP >= LevelCap[fighter.Level - 1])
                LevelUp(fighter);
        }

        private static void LevelUp (Fighter fighter)
        {
            Console.WriteLine("\nLevel up!");
            fighter.XP -= LevelCap[fighter.Level - 1];
            fighter.Level += 1;
            fighter.BaseAttack += Convert.ToInt32(Math.Ceiling(fighter.BaseAttack * 0.05));
            int bonusHP = Convert.ToInt32(Math.Ceiling(fighter.BaseHP * 0.05));
            fighter.BaseHP += bonusHP;
            fighter.HP += bonusHP;
        }
    }
}
