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
        public static List<string[,]> Rooms = new List<string[,]>();
        public int[] StartPos { get; set; }

        public Room (StreamReader sr){
            int[] matrix = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
            int row = matrix[0];
            int col = matrix[1];

            this.StartPos = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

            this.Map = new string[row, col];
            Rooms.Add(Map);

            string[] text = sr.ReadToEnd().Split('\n');
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
    }
}
