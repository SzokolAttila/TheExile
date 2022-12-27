using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheExileBasic
{
    internal class Room
    {
        public string[,] Map { get; set; }
        public static List<string[,]> Rooms = new List<string[,]>();

        public Room (int row, int col){
            Map = new string[row, col];
            Rooms.Add(Map);
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
                        Color.PickColor(room[i, j]);
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
