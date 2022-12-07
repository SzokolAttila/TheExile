using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TheExileBasic
{
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
        public bool Fought { get; set; }

        public Fighter(int range, int[] pos, int attack, int hp, int xp = 0)
        {
            this.Fought = false;
            this.Inventory = new List<Item>();
            this.Names = new List<string>();
            this.Range = range;
            this.Pos = pos;
            this.Attack = attack;
            this.HP = hp;
            this.XP = xp;
            this.MaxHP = hp;
        }

        public string[,] Move(string direction, string[,] room)
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

        public void View(string[,] room)
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
}
