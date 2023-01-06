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
        public string CompletedText { get; set; }
        public bool Collected { get; set; }

        public NPC(string name, string type, string text, string completedText, string[,] room, int[] pos, int xp, int[] questPlaceFrom = null, int[] questPlaceTo = null, Item questItem = null, Enemy questEnemy=null, bool iscompleted = false)
        {
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
            this.CompletedText = completedText;
            this.Collected = false;

            room[this.Pos[0], this.Pos[1]] = "?";

            if(this.QuestItem != null)
                room[this.QuestItem.Pos[0], this.QuestItem.Pos[1]] = "0";
            if(this.QuestEnemy != null)
                room[this.QuestEnemy.Pos[0], this.QuestEnemy.Pos[1]] = "0";

            for (int i = 0; i < Fighter.Fighters.Count; i++)
            {
                Fighter.Fighters[i].NPCs.Add(this);
                Fighter.Fighters[i].Enemies.Remove(this.QuestEnemy);
                Fighter.Fighters[i].Items.Remove(this.QuestItem);
            }
        }

        public static void CheckQuest()
        {
            for (int i = 0; i < Fighter.Fighters.Count; i++)
            {
                for (int j = 0; j < Fighter.Fighters[i].NPCs.Count; j++)
                {
                    if (Fighter.Fighters[i].NPCs[j].HasTalked)
                    {
                        switch (Fighter.Fighters[i].NPCs[j].Type)
                        {
                            case "place":
                                if (Fighter.Fighters[i].Pos[0] >= Fighter.Fighters[i].NPCs[j].QuestPlaceFrom[0] && Fighter.Fighters[i].Pos[1] >= Fighter.Fighters[i].NPCs[j].QuestPlaceFrom[1] && Fighter.Fighters[i].Pos[0] <= Fighter.Fighters[i].NPCs[j].QuestPlaceTo[0] && Fighter.Fighters[i].Pos[1] <= Fighter.Fighters[i].NPCs[j].QuestPlaceTo[1])
                                    Fighter.Fighters[i].NPCs[j].IsCompleted = true;
                                break;
                            case "enemy":
                                if (!Fighter.Fighters[i].Enemies.Contains(Fighter.Fighters[i].NPCs[j].QuestEnemy))
                                    Fighter.Fighters[i].NPCs[j].IsCompleted = true;
                                break;
                            case "item":
                                if (Fighter.Fighters[i].Inventory.Contains(Fighter.Fighters[i].NPCs[j].QuestItem))
                                    Fighter.Fighters[i].NPCs[j].IsCompleted = true;
                                break;
                        }
                    }
                }
            }
        }

    }
}
