using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TheExileBasic
{
    class NPC
    {
        public static List<NPC> NPCs = new List<NPC>();
        public string Name { get; set; }
        public string Type { get; set; }
        public int[] QuestPlaceFrom { get; set; }
        public int[] QuestPlaceTo { get; set; }
        public Item QuestItem { get; set; }
        public Enemy QuestEnemy { get; set; }
        public string Text { get; set; }
        public int[] Pos { get; set; }
        public bool IsCompleted { get; set; }
        public int XP { get; set; }
        public bool HasTalked { get; set; }

        public NPC(string name, string type, string text, string[,] room, int[] pos, int xp, int[] questPlaceFrom = null, int[] questPlaceTo = null, Item questItem = null, Enemy questEnemy=null, bool iscompleted = false)
        {
            NPCs.Add(this);
            this.Name = name;
            this.HasTalked = false;
            this.Type = type;
            this.Text = text;
            this.QuestPlaceFrom = questPlaceFrom;
            this.QuestPlaceTo = questPlaceTo;
            this.QuestItem = questItem;
            this.QuestEnemy = questEnemy;
            this.Pos = pos;
            this.IsCompleted = iscompleted;
            this.XP = xp;
            room[this.Pos[0], this.Pos[1]] = "?";
            Enemy.Enemies.Remove(this.QuestEnemy);
            Item.Items.Remove(this.QuestItem);
            if(this.QuestItem != null)
            {
                room[this.QuestItem.Pos[0], this.QuestItem.Pos[1]] = "0";
            }
            if(this.QuestEnemy != null)
            {
                room[this.QuestEnemy.Pos[0], this.QuestEnemy.Pos[1]] = "0";
            }
        }

        public static void CheckPositions(Fighter fighter)
        {
            for (int i = 0; i < NPCs.Count; i++)
            {
                if (fighter.Pos[0] == NPCs[i].Pos[0] && fighter.Pos[1] == NPCs[i].Pos[1])
                {
                    if (!NPCs[i].IsCompleted)
                    { 
                        Console.Write("\nHello, my name is "+NPCs[i].Name+".\n"+NPCs[i].Text+"\nYour task is to ");
                        switch(NPCs[i].Type)
                        {
                            case "place":
                                Console.WriteLine("get to a specific place. Signed with the question mark's color.");
                                break;
                            case "enemy":
                                Console.WriteLine("kill the "+NPCs[i].QuestEnemy.Name);
                                Enemy.Enemies.Add(NPCs[i].QuestEnemy);
                                fighter.Room[NPCs[i].QuestEnemy.Pos[0], NPCs[i].QuestEnemy.Pos[1]] = "!";
                                break;
                            case "item":
                                Console.WriteLine("find the " + NPCs[i].QuestItem.Name);
                                Item.Items.Add(NPCs[i].QuestItem);
                                fighter.Room[NPCs[i].QuestItem.Pos[0], NPCs[i].QuestItem.Pos[1]] = "*";
                                break;
                        }
                        Console.WriteLine("Come back after you have finished, so you'll gain "+NPCs[i].XP+" EXP in exchange.");
                        NPCs[i].HasTalked = true;
                    }
                    else if(NPCs[i].Pos[0] == fighter.Pos[0] && NPCs[i].Pos[1] == fighter.Pos[1])
                        {
                        Console.Write("\nThe quest is ready to hand-in, press");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write(" E ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("to do so.");
                        if (Char.ToLower(Convert.ToChar(Console.ReadKey(true).KeyChar)) == 'e')
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

        public static void CheckQuest(Fighter fighter)
        {
            for (int i = 0; i<NPCs.Count; i++)
            {
                if (NPCs[i].HasTalked)
                {
                    switch (NPCs[i].Type)
                    {
                        case "place":
                            if (fighter.Pos[0] >= NPCs[i].QuestPlaceFrom[0] && fighter.Pos[1] >= NPCs[i].QuestPlaceFrom[1] && fighter.Pos[0] <= NPCs[i].QuestPlaceTo[0] && fighter.Pos[1] <= NPCs[i].QuestPlaceTo[1])
                                NPCs[i].IsCompleted = true;
                            break;
                        case "enemy":
                            if (!Enemy.Enemies.Contains(NPCs[i].QuestEnemy))
                                NPCs[i].IsCompleted = true;
                            break;
                        case "item":
                            if (fighter.Inventory.Contains(NPCs[i].QuestItem))
                                NPCs[i].IsCompleted = true;
                            break;
                    }
                }
            }
        }

    }
}
