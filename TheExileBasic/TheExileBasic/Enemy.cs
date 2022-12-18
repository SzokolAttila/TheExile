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
            Lists.Enemies.Add(this);
            this.HP = hp;
            this.AP = ap;
            this.XP = xp;
            this.Name = name;
            this.Pos = pos;
            room[this.Pos[0], this.Pos[1]] = "!";
        }
        public static void CheckPositions(Fighter fighter)
        {
            for (int i = 0; i < Lists.Enemies.Count; i++)
            {
                if (fighter.Pos[0] == Lists.Enemies[i].Pos[0] && fighter.Pos[1] == Lists.Enemies[i].Pos[1])
                {
                    Console.Write("\nYou found yourself in front of a(n) ");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(Lists.Enemies[i].Name);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" with ");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(Lists.Enemies[i].HP);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" Health Points and ");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write(Lists.Enemies[i].AP);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" Attack Points.\nPress ");
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("E");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" for combat.");
                }
            }
        }

        public static void StartCombat(Fighter fighter)
        {
            for (int i = 0; i < Lists.Enemies.Count; i++)
            {
                if (fighter.Pos[0] == Lists.Enemies[i].Pos[0] && fighter.Pos[1] == Lists.Enemies[i].Pos[1])
                {
                    int[] result = Lists.Enemies[i].Combat(fighter);
                    fighter.HP = result[0];
                    fighter.XP += result[1];
                    fighter.Temp = "0";
                    Lists.Enemies.Remove(Lists.Enemies[i]);

                    Console.WriteLine("Press any key to end this scene: \n");

                    if (fighter.HP > 0)
                    {
                        Console.ReadKey(true);
                        Console.Clear();

                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("The Exile\n");
                        Console.ForegroundColor = ConsoleColor.White;

                        Console.Write("Press ");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("H");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" for help\n\n");

                        fighter.Room[fighter.Pos[0], fighter.Pos[1]] = "X";
                        fighter.View(fighter.Room);
                    }

                }
            }
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
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("The Exile\n");
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("Now it's time to fight!");
                Console.WriteLine(this.Name + "\t\t\t\t" + "Your stats:\n" + currentHP + "/" + this.HP + "\t\t\t\t" + "Attack: " + fighter.Attack + "\n\t\t\t\t" + "HP: " + fighter.HP);
                Console.WriteLine("It's your turn");
                currentHP -= fighter.Attack;
                if (currentHP <= 0)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("The Exile\n");
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.WriteLine("Now it's time to fight!");
                    Console.WriteLine(this.Name + "\t\t\t\t" + "Your stats:\n" + currentHP + "/" + this.HP + "\t\t\t\t" + "Attack: " + fighter.Attack + "\n\t\t\t\t" + "HP: " + fighter.HP);
                    Console.WriteLine("You have won!");
                    
                    return new int[] { fighter.HP, this.XP };
                }
                Thread.Sleep(wait);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("The Exile\n");
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("Now it's time to fight!");
                Console.WriteLine(this.Name + "\t\t\t\t" + "Your stats:\n" + currentHP + "/" + this.HP + "\t\t\t\t" + "Attack: " + fighter.Attack + "\n\t\t\t\t" + "HP: " + fighter.HP);
                Console.WriteLine("It's the enemy's turn");
                fighter.HP -= this.AP;
                if (fighter.HP <= 0)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("The Exile\n");
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.WriteLine("Now it's time to fight!");
                    Console.WriteLine(this.Name + "\t\t\t\t" + "Your stats:\n" + currentHP + "/" + this.HP + "\t\t\t\t" + "Attack: " + fighter.Attack + "\n\t\t\t\t" + "HP: " + fighter.HP);
                    Console.WriteLine("You have lost!");
                    
                    return new int[] { fighter.HP, 0 };
                }
                Thread.Sleep(wait);
            }
        }
    }
}
