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
            Lists.NPCs.Add(this);
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
            Lists.Enemies.Remove(this.QuestEnemy);
            Lists.Items.Remove(this.QuestItem);
            if(this.QuestItem != null)
                room[this.QuestItem.Pos[0], this.QuestItem.Pos[1]] = "0";
            if(this.QuestEnemy != null)
                room[this.QuestEnemy.Pos[0], this.QuestEnemy.Pos[1]] = "0";
        }

        public static void CheckPositions(Fighter fighter)
        {
            for (int i = 0; i < Lists.NPCs.Count; i++)
            {
                if (fighter.Pos[0] == Lists.NPCs[i].Pos[0] && fighter.Pos[1] == Lists.NPCs[i].Pos[1])
                {
                    if (!Lists.NPCs[i].IsCompleted)
                    { 
                        Console.Write("\nHello, my name is "+ Lists.NPCs[i].Name+".\n"+ Lists.NPCs[i].Text+"\nYour task is to ");
                        switch(Lists.NPCs[i].Type)
                        {
                            case "place":
                                Console.WriteLine("get to a specific place. Signed with the question mark's color.");
                                break;
                            case "enemy":
                                Console.WriteLine("kill the "+ Lists.NPCs[i].QuestEnemy.Name);
                                if (!Lists.NPCs[i].HasTalked)
                                {
                                    Lists.Enemies.Add(Lists.NPCs[i].QuestEnemy);
                                    fighter.Room[Lists.NPCs[i].QuestEnemy.Pos[0], Lists.NPCs[i].QuestEnemy.Pos[1]] = "!";
                                }
                                break;
                            case "item":
                                Console.WriteLine("find the " + Lists.NPCs[i].QuestItem.Name);
                                if (!Lists.NPCs[i].HasTalked)
                                {
                                    Lists.Items.Add(Lists.NPCs[i].QuestItem);
                                    fighter.Room[Lists.NPCs[i].QuestItem.Pos[0], Lists.NPCs[i].QuestItem.Pos[1]] = "*";
                                }
                                break;
                        }
                        Console.WriteLine("Come back after you have finished, so you'll gain "+ Lists.NPCs[i].XP+" EXP in exchange.");
                        Lists.NPCs[i].HasTalked = true;
                    }
                    else
                    {
                        Console.Write("\nThe quest is ready to hand-in, press");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write(" E ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("to do so.");
                    }
                }
            }
        }
        public static void CollectReward (Fighter fighter)
        {
            for (int i = 0; i < Lists.NPCs.Count; i++)
            {
                if (Lists.NPCs[i].Pos[0] == fighter.Pos[0] && Lists.NPCs[i].Pos[1] == fighter.Pos[1] && Lists.NPCs[i].IsCompleted)
                {
                    if (Lists.NPCs[i].Type == "item")
                        fighter.Inventory.Remove(Lists.NPCs[i].QuestItem);

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("The Exile\n");
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.Write("Press ");
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("H");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" for help\n\n");

                    fighter.View(fighter.Room);

                    Console.WriteLine("\nYou succesfully completed your mission!\nYou gained " + Lists.NPCs[i].XP + " EXP.");
                    fighter.XP += Lists.NPCs[i].XP;
                    fighter.Temp = "0";
                    Lists.NPCs.Remove(Lists.NPCs[i]);
                }
            }
        }
        public static void CheckQuest(Fighter fighter)
        {
            for (int i = 0; i< Lists.NPCs.Count; i++)
            {
                if (Lists.NPCs[i].HasTalked)
                {
                    switch (Lists.NPCs[i].Type)
                    {
                        case "place":
                            if (fighter.Pos[0] >= Lists.NPCs[i].QuestPlaceFrom[0] && fighter.Pos[1] >= Lists.NPCs[i].QuestPlaceFrom[1] && fighter.Pos[0] <= Lists.NPCs[i].QuestPlaceTo[0] && fighter.Pos[1] <= Lists.NPCs[i].QuestPlaceTo[1])
                                Lists.NPCs[i].IsCompleted = true;
                            break;
                        case "enemy":
                            if (!Lists.Enemies.Contains(Lists.NPCs[i].QuestEnemy))
                                Lists.NPCs[i].IsCompleted = true;
                            break;
                        case "item":
                            if (fighter.Inventory.Contains(Lists.NPCs[i].QuestItem))
                                Lists.NPCs[i].IsCompleted = true;
                            break;
                    }
                }
            }
        }

    }
}
