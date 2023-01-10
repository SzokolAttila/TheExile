using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheExileBasic
{
    internal class Positions
    {
        public static void Check()
        {
            for (int i = 0; i < Fighter.Fighters.Count; i++)
            {
                for (int j = 0; j < Fighter.Fighters[i].Enemies.Count; j++)
                {
                    if (Fighter.Fighters[i].Pos[0] == Fighter.Fighters[i].Enemies[j].Pos[0] && Fighter.Fighters[i].Pos[1] == Fighter.Fighters[i].Enemies[j].Pos[1])
                    {
                        Console.Write("\nYou found yourself in front of a(n) ");
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write(Fighter.Fighters[i].Enemies[j].Name);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" with ");
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write(Fighter.Fighters[i].Enemies[j].HP);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" Health Points and ");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write(Fighter.Fighters[i].Enemies[j].AP);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" Attack Points.\nPress ");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("E");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" for combat.");
                    }
                }

                for (int j = 0; j < Fighter.Fighters[i].Items.Count; j++)
                {
                    if (Fighter.Fighters[i].Pos[0] == Fighter.Fighters[i].Items[j].Pos[0] && Fighter.Fighters[i].Pos[1] == Fighter.Fighters[i].Items[j].Pos[1])
                    {
                        Console.Write($"\nName:\t");
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write(Fighter.Fighters[i].Items[j].Name);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("\nType:\t");
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write(Fighter.Fighters[i].Items[j].Type);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("\nRarity:\t");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(Fighter.Fighters[i].Items[j].Rarity);
                        Console.ForegroundColor = ConsoleColor.White;
                        if (Fighter.Fighters[i].Items[j].HP > 0)
                        {
                            Console.Write("\nHP:\t");
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write(Fighter.Fighters[i].Items[j].HP);
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        if (Fighter.Fighters[i].Items[j].Attack > 0)
                        {
                            Console.Write("\nAttack:\t");
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write(Fighter.Fighters[i].Items[j].Attack);
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        if (Fighter.Fighters[i].Items[j].Heal > 0)
                        {
                            Console.Write("\nHeal:\t");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(Fighter.Fighters[i].Items[j].Heal);
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        if (Fighter.Fighters[i].Items[j].Range > 0)
                        {
                            Console.Write("\nRange:\t");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write(Fighter.Fighters[i].Items[j].Range);
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        Console.Write("\nDescription:\t");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write(Fighter.Fighters[i].Items[j].Desc);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("\nPress ");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("E");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" to pick up");
                    }
                }

                for (int j = 0; j < Fighter.Fighters[i].NPCs.Count; j++)
                {
                    if (((Fighter.Fighters[i].Pos[0] - 1 == Fighter.Fighters[i].NPCs[j].Pos[0] || Fighter.Fighters[i].Pos[0] + 1 == Fighter.Fighters[i].NPCs[j].Pos[0]) && Fighter.Fighters[i].Pos[1] == Fighter.Fighters[i].NPCs[j].Pos[1]) || ((Fighter.Fighters[i].Pos[1] - 1 == Fighter.Fighters[i].NPCs[j].Pos[1] || Fighter.Fighters[i].Pos[1] + 1 == Fighter.Fighters[i].NPCs[j].Pos[1]) && Fighter.Fighters[i].Pos[0] == Fighter.Fighters[i].NPCs[j].Pos[0]))
                    {
                        if (!Fighter.Fighters[i].NPCs[j].IsCompleted)
                        {
                            Console.Write("\nWelcome, traveler! I am " + Fighter.Fighters[i].NPCs[j].Name + ".\n" + Fighter.Fighters[i].NPCs[j].Text + "\nYour task is to ");
                            switch (Fighter.Fighters[i].NPCs[j].Type)
                            {
                                case "place":
                                    Console.WriteLine("get to a specific place. Signed with the question mark's color.");
                                    break;
                                case "enemy":
                                    Console.WriteLine("kill the " + Fighter.Fighters[i].NPCs[j].QuestEnemy.Name);
                                    if (!Fighter.Fighters[i].NPCs[j].HasTalked)
                                    {
                                        Fighter.Fighters[i].Enemies.Add(Fighter.Fighters[i].NPCs[j].QuestEnemy);
                                        Room.Rooms[0].Map[Fighter.Fighters[i].NPCs[j].QuestEnemy.Pos[0], Fighter.Fighters[i].NPCs[j].QuestEnemy.Pos[1]] = "!";
                                    }
                                    break;
                                case "item":
                                    Console.WriteLine("find the " + Fighter.Fighters[i].NPCs[j].QuestItem.Name);
                                    if (!Fighter.Fighters[i].NPCs[j].HasTalked)
                                    {
                                        Fighter.Fighters[i].Items.Add(Fighter.Fighters[i].NPCs[j].QuestItem);
                                        Room.Rooms[0].Map[Fighter.Fighters[i].NPCs[j].QuestItem.Pos[0], Fighter.Fighters[i].NPCs[j].QuestItem.Pos[1]] = "*";
                                    }
                                    break;
                            }
                            Console.WriteLine("Come back after you have finished, so you'll get " + Fighter.Fighters[i].NPCs[j].Gold + " gold as a reward for your troubles.");
                            Fighter.Fighters[i].NPCs[j].HasTalked = true;
                        }
                        else
                        {
                            Console.WriteLine("\n" + Fighter.Fighters[i].NPCs[j].CompletedText);
                            if (!Fighter.Fighters[i].NPCs[j].Collected)
                            {
                                Console.Write("The quest is ready to hand-in, press");
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.Write(" E ");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("to do so.");
                            }
                        }
                    }
                }
            }
        }
    }
}
