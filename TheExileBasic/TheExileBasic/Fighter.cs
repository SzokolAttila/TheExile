﻿using System;
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
        public bool Moved { get; set; }
        public int[] PrevPos { get; set; }

        public string[,] Room { get; set; }

        public Fighter(int range, int[] pos, int attack, int hp)
        {
            this.Moved = true;
            this.Inventory = new List<Item>();
            this.Names = new List<string>();
            this.Range = range;
            this.Pos = pos;
            this.Attack = attack;
            this.HP = hp;
            this.XP = 0;
            this.MaxHP = hp;
            this.PrevPos = pos;
        }

        public string[,] Move(string direction, string[,] room)
        {
            room[this.Pos[0], this.Pos[1]] = this.Temp;
            int[] startPos = new int[] { this.Pos[0], this.Pos[1] };
            this.Moved = false;

            switch (direction)
            {
                case "s":
                    if (this.Pos[0] < room.GetLength(0) - 1 && room[this.Pos[0] + 1, this.Pos[1]] != "M" && (room[this.Pos[0] + 1, this.Pos[1]] != "~" || this.Names.Contains("Boat")) && room[this.Pos[0] + 1, this.Pos[1]] != " " && (this.Temp != "!" || (this.Pos[0] + 1 == this.PrevPos[0] && this.Pos[1] == this.PrevPos[1])))
                        this.Pos[0]++;
                    break;
                case "w":
                    if (this.Pos[0] > 0 && room[this.Pos[0] - 1, this.Pos[1]] != "M" && (room[this.Pos[0] - 1, this.Pos[1]] != "~" || this.Names.Contains("Boat")) && room[this.Pos[0] - 1, this.Pos[1]] != " " && (this.Temp != "!" || (this.Pos[0] - 1 == this.PrevPos[0] && this.Pos[1] == this.PrevPos[1])))
                        this.Pos[0]--;
                    break;
                case "a":
                    if (this.Pos[1] > 0 && room[this.Pos[0], this.Pos[1] - 1] != "M" && (room[this.Pos[0], this.Pos[1] - 1] != "~" || this.Names.Contains("Boat")) && room[this.Pos[0], this.Pos[1] - 1] != " " && (this.Temp != "!" || (this.Pos[0] == this.PrevPos[0] && this.Pos[1] - 1 == this.PrevPos[1])))
                        this.Pos[1]--;
                    break;
                case "d":
                    if (this.Pos[1] < room.GetLength(1) - 1 && room[this.Pos[0], this.Pos[1] + 1] != "M" && (room[this.Pos[0], this.Pos[1] + 1] != "~" || this.Names.Contains("Boat")) && room[this.Pos[0], this.Pos[1] + 1] != " " && (this.Temp != "!" || (this.Pos[0] == this.PrevPos[0] && this.Pos[1] + 1 == this.PrevPos[1])))
                        this.Pos[1]++;
                    break;
                default:
                    break;
            }
            int[] currentPos = new int[] { this.Pos[0], this.Pos[1] };

            if (startPos[0] != currentPos[0] || startPos[1] != currentPos[1])
            {
                this.Moved = true;
                Thread.Sleep(1);
                Console.Clear();
                this.PrevPos = startPos;
            }

            if (room[this.Pos[0], this.Pos[1]] == "~")
            {
                this.Temp = room[this.Pos[0], this.Pos[1]];
                room[this.Pos[0], this.Pos[1]] = "B";
            }
            else if (room[this.Pos[0], this.Pos[1]] == "*")
            {
                this.Temp = room[this.Pos[0], this.Pos[1]];
                room[this.Pos[0], this.Pos[1]] = "I";
            }
            else if (room[this.Pos[0], this.Pos[1]] == "!")
            {
                this.Temp = room[this.Pos[0], this.Pos[1]];
                room[this.Pos[0], this.Pos[1]] = "E";
            }
            else
            {
                this.Temp = room[this.Pos[0], this.Pos[1]];
                room[this.Pos[0], this.Pos[1]] = "X";
            }
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
                            case "E":
                                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                break;
                            case "I":
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
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
                        case "E":
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            break;
                        case "I":
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
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
