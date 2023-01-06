using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheExileBasic
{
    internal class Interactions
    {
        public static void Interact()
        {
            for (int i = 0; i < Fighter.Fighters.Count; i++)
            {
                for (int j = 0; j < Fighter.Fighters[i].Enemies.Count; j++)
                {
                    if (Fighter.Fighters[i].Pos[0] == Fighter.Fighters[i].Enemies[j].Pos[0] && Fighter.Fighters[i].Pos[1] == Fighter.Fighters[i].Enemies[j].Pos[1])
                    {
                        int[] result = Fighter.Fighters[i].Enemies[j].Combat(Fighter.Fighters[i]);
                        Fighter.Fighters[i].HP = result[0];
                        Fighter.Fighters[i].XP += result[1];
                        Fighter.Fighters[i].OverallXP += result[1];
                        Fighter.Fighters[i].Temp = "0";
                        Fighter.Fighters[i].Enemies.Remove(Fighter.Fighters[i].Enemies[j]);

                        Console.WriteLine("Press any key to end this scene: \n");

                        if (Fighter.Fighters[i].HP > 0)
                        {
                            Console.ReadKey(true);

                            Menus.Header();
                            Menus.PressH();

                            Room.Rooms[0].Map[Fighter.Fighters[i].Pos[0], Fighter.Fighters[i].Pos[1]] = "X";
                            Room.Rooms[0].View(Fighter.Fighters[i]);
                        }

                    }
                }

                for (int j = 0; j < Fighter.Fighters[i].Items.Count; j++)
                {
                    if (Fighter.Fighters[i].Pos[0] == Fighter.Fighters[i].Items[j].Pos[0] && Fighter.Fighters[i].Pos[1] == Fighter.Fighters[i].Items[j].Pos[1])
                    {
                        Menus.Header();

                        Room.Rooms[0].Map[Fighter.Fighters[i].Pos[0], Fighter.Fighters[i].Pos[1]] = "X";
                        Room.Rooms[0].View(Fighter.Fighters[i]);

                        Console.WriteLine($"\n{Fighter.Fighters[i].Items[j].Name} successfully obtained.");

                        Fighter.Fighters[i].Attack += Fighter.Fighters[i].Items[j].Attack;
                        Fighter.Fighters[i].MaxHP += Fighter.Fighters[i].Items[j].HP;
                        Fighter.Fighters[i].Range += Fighter.Fighters[i].Items[j].Range;
                        Fighter.Fighters[i].Temp = "0";
                        if (Fighter.Fighters[i].Items[j].Type == "Consumable")
                        {
                            Fighter.Fighters[i].Consumables.Add(Fighter.Fighters[i].Items[j]);
                            Fighter.Fighters[i].ConsumableNames.Add(Fighter.Fighters[i].Items[j].Name);
                        }
                        else
                        {
                            Fighter.Fighters[i].Inventory.Add(Fighter.Fighters[i].Items[j]);
                            Fighter.Fighters[i].Names.Add(Fighter.Fighters[i].Items[j].Name);
                        }
                        Fighter.Fighters[i].Items.Remove(Fighter.Fighters[i].Items[j]);
                    }
                }

                for (int j = 0; j < Fighter.Fighters[i].NPCs.Count; j++)
                {
                    if (((Fighter.Fighters[i].Pos[0] - 1 == Fighter.Fighters[i].NPCs[j].Pos[0] || Fighter.Fighters[i].Pos[0] + 1 == Fighter.Fighters[i].NPCs[j].Pos[0]) && Fighter.Fighters[i].Pos[1] == Fighter.Fighters[i].NPCs[j].Pos[1]) || ((Fighter.Fighters[i].Pos[1] - 1 == Fighter.Fighters[i].NPCs[j].Pos[1] || Fighter.Fighters[i].Pos[1] + 1 == Fighter.Fighters[i].NPCs[j].Pos[1]) && Fighter.Fighters[i].Pos[0] == Fighter.Fighters[i].NPCs[j].Pos[0]) && Fighter.Fighters[i].NPCs[j].IsCompleted)
                    {
                        if (Fighter.Fighters[i].NPCs[j].Type == "item")
                            Fighter.Fighters[i].Inventory.Remove(Fighter.Fighters[i].NPCs[j].QuestItem);

                        Menus.Header();
                        Menus.PressH();

                        Room.Rooms[0].View(Fighter.Fighters[i]);

                        Console.WriteLine("\nYou succesfully completed your mission!\nYou gained " + Fighter.Fighters[i].NPCs[j].Gold + " gold.");
                        Fighter.Fighters[i].Gold += Fighter.Fighters[i].NPCs[j].Gold;
                    }
                }
            }
        }
    }
}
