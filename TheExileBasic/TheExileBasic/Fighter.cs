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
        public static List<Fighter> Fighters = new List<Fighter>();
        public List<Enemy> Enemies { get; set; }
        public List<Item> Items { get; set; }
        public List<NPC> NPCs { get; set; }
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

        public Fighter(int range, int[] pos, int attack, int hp, string[,] room)
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

            Enemies = new List<Enemy>();
            Items = new List<Item>();
            NPCs = new List<NPC>();

            Fighters.Add(this);
            this.Temp = room[pos[0], pos[1]];
            room[pos[0], pos[1]] = "X";
        }

        public string[,] Move(ConsoleKey direction, string[,] room)
        {
            room[this.Pos[0], this.Pos[1]] = this.Temp;
            int[] startPos = new int[] { this.Pos[0], this.Pos[1] };
            this.Moved = false;

            switch (direction)
            {
                case ConsoleKey.S:
                    if (this.Pos[0] < room.GetLength(0) - 1 && room[this.Pos[0] + 1, this.Pos[1]] != "M" && room[this.Pos[0] + 1, this.Pos[1]] != "?" && (room[this.Pos[0] + 1, this.Pos[1]] != "~" || this.Names.Contains("Boat")) && room[this.Pos[0] + 1, this.Pos[1]] != " " && (this.Temp != "!" || (this.Pos[0] + 1 == this.PrevPos[0] && this.Pos[1] == this.PrevPos[1])))
                        this.Pos[0]++;
                    break;
                case ConsoleKey.W:
                    if (this.Pos[0] > 0 && room[this.Pos[0] - 1, this.Pos[1]] != "M" && room[this.Pos[0] - 1, this.Pos[1]] != "?" && (room[this.Pos[0] - 1, this.Pos[1]] != "~" || this.Names.Contains("Boat")) && room[this.Pos[0] - 1, this.Pos[1]] != " " && (this.Temp != "!" || (this.Pos[0] - 1 == this.PrevPos[0] && this.Pos[1] == this.PrevPos[1])))
                        this.Pos[0]--;
                    break;
                case ConsoleKey.A:
                    if (this.Pos[1] > 0 && room[this.Pos[0], this.Pos[1] - 1] != "M" && room[this.Pos[0], this.Pos[1] - 1] != "?" && (room[this.Pos[0], this.Pos[1] - 1] != "~" || this.Names.Contains("Boat")) && room[this.Pos[0], this.Pos[1] - 1] != " " && (this.Temp != "!" || (this.Pos[0] == this.PrevPos[0] && this.Pos[1] - 1 == this.PrevPos[1])))
                        this.Pos[1]--;
                    break;
                case ConsoleKey.D:
                    if (this.Pos[1] < room.GetLength(1) - 1 && room[this.Pos[0], this.Pos[1] + 1] != "M" && room[this.Pos[0], this.Pos[1] + 1] != "?" && (room[this.Pos[0], this.Pos[1] + 1] != "~" || this.Names.Contains("Boat")) && room[this.Pos[0], this.Pos[1] + 1] != " " && (this.Temp != "!" || (this.Pos[0] == this.PrevPos[0] && this.Pos[1] + 1 == this.PrevPos[1])))
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
                this.PrevPos = startPos;
            }

            this.Temp = room[this.Pos[0], this.Pos[1]];
            switch (room[this.Pos[0], this.Pos[1]])
            {
                case "~":
                    room[this.Pos[0], this.Pos[1]] = "B";
                    break;
                case "?":
                    room[this.Pos[0], this.Pos[1]] = "N";
                    break;
                default:
                    room[this.Pos[0], this.Pos[1]] = "X";
                    break;
            }
            
            return room;
        }

        public void View(string[,] room)
        {
            List<NPC> quests = this.NPCs;
            for (int i = -this.Range; i <= this.Range; i++)
            {
                for (int j = -this.Range; j <= this.Range; j++)
                {
                    int currI = this.Pos[0] + i;
                    int currJ = this.Pos[1] + j;
                    if (currI >= 0 && currI < room.GetLength(0) && currJ >= 0 && currJ < room.GetLength(1))
                    {
                        for (int k = 0; k  < quests.Count; k++)
                        {
                            if (quests[k].HasTalked && quests[k].Type == "place" && this.Pos[0]+i >= quests[k].QuestPlaceFrom[0] && this.Pos[1] + j >= quests[k].QuestPlaceFrom[1] && this.Pos[0] + i <= quests[k].QuestPlaceTo[0] && this.Pos[1] + j <= quests[k].QuestPlaceTo[1])
                                Console.BackgroundColor = ConsoleColor.DarkMagenta;
                        }
                        Color.PickColor(room[currI, currJ], this.Temp);
                        Console.SetCursorPosition(Limit.Check(0, Console.WindowWidth - 5 + j, this.Pos[1] + j), Limit.Check(0, Console.WindowHeight - 6 + i, this.Pos[0] + i + 4));
                        Console.Write(room[this.Pos[0] + i, this.Pos[1] + j]);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
    }
}
