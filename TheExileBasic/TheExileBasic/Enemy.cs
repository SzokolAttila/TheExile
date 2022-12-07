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
        public static List<Enemy> Enemies = new List<Enemy>();
        public int HP { get; set; }
        public int AP { get; set; }
        public string Name { get; set; }
        public int XP { get; set; }
        public int[] Pos { get; set; }
        public Enemy(int hp, int ap, string name, int xp, int[] pos, string[,] room)
        {
            Enemies.Add(this);
            this.HP = hp;
            this.AP = ap;
            this.XP = xp;
            this.Name = name;
            this.Pos = pos;
            room[this.Pos[0], this.Pos[1]] = "!";
        }
        public static void CheckPositions(Fighter fighter)
        {
            for (int i = 0; i < Enemies.Count; i++)
            {
                if ((fighter.Pos[0] == Enemies[i].Pos[0] && fighter.Pos[1] - 1 == Enemies[i].Pos[1]) || (fighter.Pos[0] == Enemies[i].Pos[0] && fighter.Pos[1] + 1 == Enemies[i].Pos[1]) || (fighter.Pos[0] - 1 == Enemies[i].Pos[0] && fighter.Pos[1] == Enemies[i].Pos[1]) || (fighter.Pos[0] + 1 == Enemies[i].Pos[0] && fighter.Pos[1] == Enemies[i].Pos[1]))
                    Console.WriteLine("\nYou found yourself in front of a(n) " + Enemies[i].Name + " with " + Enemies[i].HP + " Health Points and " + Enemies[i].AP + " Attack Points.\nStep on the same field for combat.");
                else if (fighter.Pos[0] == Enemies[i].Pos[0] && fighter.Pos[1] == Enemies[i].Pos[1])
                {
                    int[] result = Enemies[i].Combat(fighter);
                    fighter.HP = result[0];
                    fighter.XP += result[1];
                    fighter.Temp = "0";
                    Enemies.Remove(Enemies[i]);
                    Console.Clear();
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
}
