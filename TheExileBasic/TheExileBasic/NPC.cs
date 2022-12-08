using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheExileBasic
{
    class NPC
    {
        public static List<NPC> NPCs = new List<NPC>();
        public string Name { get; set; }
        public string Type { get; set; }
        public int[] QuestPlace { get; set; }
        public Item QuestItem { get; set; }
        public Enemy QuestEnemy { get; set; }
        public string Text { get; set; }
        public int[] Pos { get; set; }
        public bool isCompleted { get; set; }
        public int XP { get; set; }

        public NPC(string name, string type, string text, string[,] room, int[] pos, int xp, int[] questPlace = null, Item questItem = null, Enemy questEnemy=null, bool iscompleted = false)
        {
            NPCs.Add(this);
            this.Name = name;
            this.Type = type;
            this.Text = text;
            this.QuestPlace = questPlace;
            this.QuestItem = questItem;
            this.QuestEnemy = questEnemy;
            this.Pos = pos;
            this.isCompleted = iscompleted;
            this.XP = xp;
            room[this.Pos[0], this.Pos[1]] = "?";
        }

        public static void CheckPositions(Fighter fighter)
        {
            for (int i = 0; i < NPCs.Count; i++)
            {
                if ((fighter.Pos[0] == NPCs[i].Pos[0] && fighter.Pos[1] - 1 == NPCs[i].Pos[1]) || (fighter.Pos[0] == NPCs[i].Pos[0] && fighter.Pos[1] + 1 == NPCs[i].Pos[1]) || (fighter.Pos[0] - 1 == NPCs[i].Pos[0] && fighter.Pos[1] == NPCs[i].Pos[1]) || (fighter.Pos[0] + 1 == NPCs[i].Pos[0] && fighter.Pos[1] == NPCs[i].Pos[1]) || fighter.Pos[0] == NPCs[i].Pos[0] && fighter.Pos[1] == NPCs[i].Pos[1])
                {
                    if (!NPCs[i].isCompleted)
                    { 
                        Console.Write("Hello, my name is "+NPCs[i].Name+".\n"+NPCs[i].Text+"\nYour task is to ");
                        switch(NPCs[i].Type)
                        {
                            case "place":
                                Console.WriteLine("Get to a specific place. ("+string.Join(",", NPCs[i].QuestPlace)+")");
                                break;
                            case "enemy":
                                Console.WriteLine("You need to kill the "+NPCs[i].QuestEnemy.Name);
                                break;
                            case "item":
                                Console.WriteLine("You need to find the " + NPCs[i].QuestItem.Name);
                                break;
                        }
                        Console.WriteLine("Come back after you finished, so you'll gain "+NPCs[i].XP+" EXP in exchange. (Step on the same field)");
                    }
                    else if (NPCs[i].isCompleted)
                    {
                        if (NPCs[i].Pos[0] == fighter.Pos[0] && NPCs[i].Pos[1] == fighter.Pos[1])
                        {
                            Console.Write("The quest is ready to hand-in, press");
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write(" E ");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("to do so.");
                            if (Console.ReadKey(true).KeyChar=='e')
                            {
                                if (NPCs[i].Type == "item")
                                {
                                    fighter.Inventory.Remove(NPCs[i].QuestItem);
                                }
                                Console.WriteLine("\nYou succesfully completed your mission!\nYou gained " + NPCs[i].XP + " EXP.");
                                fighter.XP += NPCs[i].XP;
                                fighter.Temp = "0";
                                NPCs.Remove(NPCs[i]);
                            }
                        }
                    }
                }
            }
        }
        
        public static void checkQuest(Fighter fighter)
        {
            for (int i = 0; i<NPCs.Count; i++)
            {
                switch (NPCs[i].Type)
                {
                    case "place":
                        if (fighter.Pos[0] == NPCs[i].Pos[0] && fighter.Pos[1] == NPCs[i].Pos[1])
                        {
                            NPCs[i].isCompleted = true;
                        }
                        break;
                    case "enemy":
                        if (!Enemy.Enemies.Contains(NPCs[i].QuestEnemy))
                        {
                            NPCs[i].isCompleted = true;
                        }
                        break;
                    case "item":
                        if (fighter.Inventory.Contains(NPCs[i].QuestItem))
                        {
                            NPCs[i].isCompleted = true;
                        }
                        break;
                }
            }
        }

    }
}
