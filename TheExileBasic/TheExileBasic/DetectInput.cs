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
        public static void FindKeyPressed (ConsoleKey input, bool map, bool help, bool inventory, Room start, Fighter fighter, Item potion)
        {
            switch (input)
            {
                case ConsoleKey.M:
                    Menus.Header();

                    if (!map)
                    {
                        start.Show(start.Map);
                        Menus.CloseMenu(map, inventory, help);
                    }
                    else
                    {
                        Menus.PressH();

                        start.View(fighter);
                        Positions.Check();
                        map = false;
                    }
                    break;

                case ConsoleKey.I:
                    Menus.Header();

                    if (!inventory)
                    {
                        Menus.Inventory(fighter);
                        Menus.CloseMenu(inventory, map, help);
                    }
                    else
                    {
                        Menus.PressH();

                        start.View(fighter);
                        Positions.Check();
                        inventory = false;
                    }
                    break;

                case ConsoleKey.H:
                    Menus.Header();

                    if (!help)
                    {
                        Menus.Help();
                        Menus.CloseMenu(help, inventory, map);
                    }
                    else
                    {
                        Menus.PressH();

                        start.View(fighter);
                        Positions.Check();
                        help = false;
                    }
                    break;

                case ConsoleKey.U:
                    Menus.Header();
                    Menus.CheckHeal(potion, fighter);
                    start.View(fighter);
                    Positions.Check();
                    break;

                case ConsoleKey.E:
                    Interactions.Interact();
                    break;

                case ConsoleKey.Escape:
                    if (inventory || map || help)
                    {
                        Menus.Header();
                        Menus.PressH();

                        start.View(fighter);
                        Positions.Check();
                        inventory = false;
                        map = false;
                        help = false;
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
