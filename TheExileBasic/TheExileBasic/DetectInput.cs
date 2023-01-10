using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheExileBasic
{
    internal class DetectInput
    {
        public static bool Inventory { get; set; }
        public static bool Map { get; set; }
        public static bool Help { get; set; }
        public static void FindKeyPressed (ConsoleKey input, Room start, Fighter fighter, Item potion)
        {
            switch (input)
            {
                case ConsoleKey.M:
                    Menus.Header();

                    if (!Map)
                    {
                        start.Show(start.Map);
                        Map = true;
                        Inventory = false;
                        Help = false;
                    }
                    else
                    {
                        Menus.PressH();

                        start.View(fighter);
                        Positions.Check();
                        
                    }
                    break;

                case ConsoleKey.I:
                    Menus.Header();

                    if (!Inventory)
                    {
                        Menus.Inventory(fighter);
                        Inventory = true;
                        Map = false;
                        Help = false;
                    }
                    else
                    {
                        Menus.PressH();

                        start.View(fighter);
                        Positions.Check();
                        Inventory = false;
                    }
                    break;

                case ConsoleKey.H:
                    Menus.Header();

                    if (!Help)
                    {
                        Menus.Help();
                        Help = true;
                        Map = false;
                        Inventory = false;
                    }
                    else
                    {
                        Menus.PressH();

                        start.View(fighter);
                        Positions.Check();
                        Help = false;
                    }
                    break;

                case ConsoleKey.U:
                    if (!(Inventory || Map || Help))
                    {
                        Menus.Header();
                        Menus.CheckHeal(potion, fighter);
                        start.View(fighter);
                        Positions.Check();
                    }
                    break;

                case ConsoleKey.E:
                    Interactions.Interact();
                    break;

                case ConsoleKey.Escape:
                    if (Inventory || Map || Help)
                    {
                        Menus.Header();
                        Menus.PressH();

                        start.View(fighter);
                        Positions.Check();
                        Inventory = false;
                        Map = false;
                        Help = false;
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
