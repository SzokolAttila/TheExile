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
        public bool Moved { get; set; }
        public int[] PrevPos { get; set; }
        public List<Item> Consumables { get; set; }
        public List<string> ConsumableNames { get; set; }
        public string[,] Room { get; set; }

        public Fighter(int range, int[] pos, int attack, int hp)
        {
            this.Moved = true;
            this.Inventory = new List<Item>();
            this.Consumables = new List<Item>();
            this.Names = new List<string>();
            this.ConsumableNames = new List<string>();
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
            else if (room[this.Pos[0], this.Pos[1]] == "?")
            {
                this.Temp = room[this.Pos[0], this.Pos[1]];
                room[this.Pos[0], this.Pos[1]] = "N";
            }
            else
            {
                this.Temp = room[this.Pos[0], this.Pos[1]];
                room[this.Pos[0], this.Pos[1]] = "X";
            }
            return room;
        }

        static int limit(int min, int max, int value)
        {
            if (value > max)
            {
                return max;
            }
            else if (value < min)
            {
                return min;
            }
            else
            {
                return value;
            }
        }

        public void View(string[,] room)
        {
            List<NPC> quests = NPC.NPCs;
            for (int i = -this.Range; i <= this.Range; i++)
            {
                for (int j = -this.Range; j <= this.Range; j++)
                {
                    if (this.Pos[0] + i >= 0 && this.Pos[0] + i < room.GetLength(0) && this.Pos[1] + j >= 0 && this.Pos[1] + j < room.GetLength(1) && this.Pos[1] + j < room.GetLength(1))
                    {
                        for (int a = 0; a<quests.Count; a++)
                        {
                            if (quests[a].Type == "place" && this.Pos[0]+i >= quests[a].QuestPlaceFrom[0] && this.Pos[1] + j >= quests[a].QuestPlaceFrom[1] && this.Pos[0] + i <= quests[a].QuestPlaceTo[0] && this.Pos[1] + j <= quests[a].QuestPlaceTo[1])
                            {
                                Console.BackgroundColor = ConsoleColor.DarkMagenta;
                            }
                        }

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
                            case "N":
                                Console.ForegroundColor = ConsoleColor.Green;
                                break;
                            case "I":
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                break;
                            case "?":
                                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                break;
                            default:
                                Console.ForegroundColor = ConsoleColor.White;
                                break;
                        }
                        Console.SetCursorPosition(Fighter.limit(0, Console.WindowWidth - 5 + j, this.Pos[1] + j), Fighter.limit(0, Console.WindowHeight - 5 + i, this.Pos[0] + i + 4));
                        Console.Write(room[this.Pos[0] + i, this.Pos[1] + j]);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        public void Map(string[,] room)
        {
            List<NPC> quests = NPC.NPCs;
            for (int i = 0; i < room.GetLength(0); i++)
            {
                for (int j = 0; j < room.GetLength(1); j++)
                {
                    for (int a = 0; a < quests.Count; a++)
                    {
                        if (quests[a].Type == "place" && i >= quests[a].QuestPlaceFrom[0] && j >= quests[a].QuestPlaceFrom[1] && i <= quests[a].QuestPlaceTo[0] && j <= quests[a].QuestPlaceTo[1])
                        {
                            Console.BackgroundColor = ConsoleColor.DarkMagenta;
                        }
                    }
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
                        case "N":
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        case "E":
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            break;
                        case "I":
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            break;
                        case "?":
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                    }
                    Console.Write(room[i, j]);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.WriteLine();
            }
        }
    }
}
