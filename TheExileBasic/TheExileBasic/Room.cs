using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheExileBasic
{
    internal class Room
    {
        public string[,] Map { get; set; }
        public static List<Room> Rooms = new List<Room>();
        public int[] StartPos { get; set; }

        public Room (StreamReader sr){
            int[] matrix = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
            int row = matrix[0];
            int col = matrix[1];

            this.StartPos = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

            this.Map = new string[row, col];
            Rooms.Add(this);

            string[] text = sr.ReadToEnd().Split('\n');
            GenerateMap(row, col, text);
        }

        private void GenerateMap (int row, int col, string[] text)
        {
            for (int i = 0; i < row; i++)
            {
                string temp = text[i].Replace("\r", "");
                for (int j = 0; j < col; j++)
                {
                    if (j < temp.Length)
                        this.Map[i, j] = temp[j].ToString();
                    else this.Map[i, j] = " ";
                }
            }
        }
        public void Show(string[,] room)
        {
            for (int h = 0; h < Fighter.Fighters.Count; h++)
            {
                List<NPC> quests = Fighter.Fighters[h].NPCs;
                for (int i = 0; i < room.GetLength(0); i++)
                {
                    for (int j = 0; j < room.GetLength(1); j++)
                    {
                        for (int k = 0; k < quests.Count; k++)
                        {
                            if (quests[k].HasTalked && quests[k].Type == "place" && i >= quests[k].QuestPlaceFrom[0] && j >= quests[k].QuestPlaceFrom[1] && i <= quests[k].QuestPlaceTo[0] && j <= quests[k].QuestPlaceTo[1])
                                Console.BackgroundColor = ConsoleColor.DarkMagenta;
                        }
                        Color.PickColor(room[i, j], Fighter.Fighters[h].Temp);
                        Console.Write(room[i, j]);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    Console.WriteLine();
                }
            }
        }

        public void View(Fighter fighter)
        {
            List<NPC> quests = fighter.NPCs;
            for (int i = -fighter.Range; i <= fighter.Range; i++)
            {
                for (int j = -fighter.Range; j <= fighter.Range; j++)
                {
                    int currI = fighter.Pos[0] + i;
                    int currJ = fighter.Pos[1] + j;
                    if (currI >= 0 && currI < this.Map.GetLength(0) && currJ >= 0 && currJ < this.Map.GetLength(1))
                    {
                        for (int k = 0; k < quests.Count; k++)
                        {
                            if (quests[k].HasTalked && quests[k].Type == "place" && fighter.Pos[0] + i >= quests[k].QuestPlaceFrom[0] && fighter.Pos[1] + j >= quests[k].QuestPlaceFrom[1] && fighter.Pos[0] + i <= quests[k].QuestPlaceTo[0] && fighter.Pos[1] + j <= quests[k].QuestPlaceTo[1])
                                Console.BackgroundColor = ConsoleColor.DarkMagenta;
                        }
                        Color.PickColor(this.Map[currI, currJ], fighter.Temp);
                        Console.SetCursorPosition(Limit.Check(0, Console.WindowWidth - 5 + j, fighter.Pos[1] + j), Limit.Check(0, Console.WindowHeight - 6 + i, fighter.Pos[0] + i + 4));
                        Console.Write(this.Map[fighter.Pos[0] + i, fighter.Pos[1] + j]);
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
