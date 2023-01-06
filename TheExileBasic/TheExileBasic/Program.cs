﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TheExileBasic
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleKey input;
            bool help = false;
            bool map = false;
            bool inventory = false;
            string path = "map.txt";

            StreamReader sr = new StreamReader(path);
            Room start = new Room(sr);
            Fighter fighter = new Fighter(3, start.StartPos, 100, 1000, start.Map);

            Item dagger = new Item("Weapon", "Dagger", "Common", "A dull-edged dagger", new int[] { 6, 17 }, start.Map, attack: 24);
            Item boat = new Item("Utility", "Boat", "Rare", "The key of exploration, a boat. Now, set sails and ride waves!", new int[] { 13, 47 }, start.Map);
            Item potion = new Item("Consumable", "Heal", "Rare", "Replenishes a small amount of missing health; Cannot exceed max HP.", new int[] { 7, 19 }, start.Map, heal: 200);
            Item glasses = new Item("Utility", "A pair of glasses", "Uncommon", "To see further than the tip of your toes", new int[] { 31, 5 }, start.Map, range: 1); ;

            Enemy golem = new Enemy(2480, 50, "Golem", 250, new int[] { 11, 23 }, start.Map);
            Enemy ent = new Enemy(400, 60, "Ent", 100, new int[] { 13, 46 }, start.Map);

            NPC villageChief = new NPC("Village Chief", "enemy", "The Golem keeps my village in fear, please defend us from it!", "Thank you for saving my village, mighty warrior!", start.Map, new int[] { 5, 5 }, 1000, questEnemy: golem);
            NPC erwin = new NPC("Erwin", "place", "By the way...you'd better visit that isle!", "So, how was the island?", start.Map, new int[] { 13, 8 }, 300, new int[] { 5, 16 }, new int[] { 7, 19 });

            Menus.Header();
            Console.Write("Press ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("WASD");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" to start");

            do
            {
                LevelSystem.CheckLevel(fighter);

                Input.ClearKeyBuffer();
                input = Console.ReadKey(true).Key;
                if (!(map || help || inventory))
                    start.Map = fighter.Move(input, start.Map);

                DetectInput.FindKeyPressed(input, map, help, inventory, start, fighter, potion);

                if (fighter.Moved)
                {
                    Menus.Header();
                    Menus.PressH();

                    start.View(fighter);
                    Positions.Check();
                    help = false;
                    inventory = false;
                    map = false;
                }

                if (fighter.HP <= 0 || input == ConsoleKey.X)
                {
                    Menus.Header();

                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"\nGame Over! You died with {fighter.OverallXP} XP.");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                }
                NPC.CheckQuest();

            } while (input != ConsoleKey.X);

            Console.ReadKey(true);
            sr.Close();
        }
    }
}
