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
        public List<Item> Consumables { get; set; }
        public List<string> ConsumableNames { get; set; }
        public string Temp { get; set; }
        public int Range { get; set; }
        public int[] Pos { get; set; }
        public int Attack { get; set; }
        public int HP { get; set; }
        public int MaxHP { get; set; }
        public int Gold { get; set; }
        public int XP { get; set; }
        public int OverallXP { get; set; }
        public int Level { get; set; }
        public bool Moved { get; set; }
        private int[] PrevPos { get; set; }

        public Fighter(int range, int[] pos, int attack, int hp, string[,] room)
        {
            this.Level = 1;
            this.Gold = 0;
            this.Moved = true;
            this.Inventory = new List<Item>();
            this.Consumables = new List<Item>();
            this.Names = new List<string>();
            this.ConsumableNames = new List<string>();
            this.Range = range;
            this.Pos = pos;
            this.Attack = attack;
            this.OverallXP = 0;
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
                    CheckMovement(room, 0, 0, 1);
                    break;
                case ConsoleKey.W:
                    CheckMovement(room, 0, 0, -1);
                    break;
                case ConsoleKey.A:
                    CheckMovement(room, 1, -1, 0);
                    break;
                case ConsoleKey.D:
                    CheckMovement(room, 1, 1, 0);
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

        private void CheckMovement(string[,] room, int index, int directionX, int directionY)
        {
            if (this.Pos[index] + directionY + directionX >= 0 && this.Pos[index] + directionX + directionY < room.GetLength(index))
            {
                string nextTile = room[this.Pos[0] + directionY, this.Pos[1] + directionX];
                if (nextTile != "M" && nextTile != "?" && (nextTile != "~" || this.Names.Contains("Boat")) && nextTile != " " && (this.Temp != "!" || (this.Pos[0] + directionY == this.PrevPos[0] && this.Pos[1] + directionX == this.PrevPos[1])))
                    this.Pos[index] += directionX + directionY;
            }
        }
    }
}
