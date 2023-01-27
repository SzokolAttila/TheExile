using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheExileBasic
{
    internal class Consumable
    {
        public static void CheckConsumable(Fighter fighter, string direction)
        {
            if (!(DetectInput.Map || DetectInput.Inventory || DetectInput.Help))
            {
                if (fighter.Consumables.Count == 0)
                {
                    Menus.Header();
                    Room.Rooms[0].View(fighter);
                    Console.WriteLine("\nNo consumables available!");
                }
                else
                {
                    if (direction == "up")
                        NextConsumable(fighter);
                    else if (direction == "down")
                        PrevConsumable(fighter);
                    else UseConsumable(fighter);
                }
            }
        }

        private static void UseConsumable(Fighter fighter)
        {
            Menus.Header();
            Room.Rooms[0].View(fighter);
            switch (fighter.CurrentConsumable.Type.Split('-')[1].Trim())
            {
                case "Heal":
                    fighter.HP += fighter.CurrentConsumable.Heal;
                    Console.WriteLine($"\nHealed player for {fighter.CurrentConsumable.Heal} HP.\n");
                    break;
                case "Vitality":
                    fighter.MaxHP += fighter.CurrentConsumable.HP;
                    fighter.HP += fighter.CurrentConsumable.HP;
                    Console.WriteLine($"\nMaxHP increased by {fighter.CurrentConsumable.HP}");
                    break;
                case "Strength":
                    fighter.Attack += fighter.CurrentConsumable.Attack;
                    break;
                case "Sight":
                    fighter.Range += fighter.CurrentConsumable.Range;
                    break;
                default:
                    break;
            }
            fighter.Consumables.Remove(fighter.CurrentConsumable);
        }

        private static void NextConsumable(Fighter fighter)
        {
            int index = fighter.Consumables.IndexOf(fighter.CurrentConsumable);
            if (index == fighter.Consumables.Count - 1)
                fighter.CurrentConsumable = fighter.Consumables[0];
            else fighter.CurrentConsumable = fighter.Consumables[index + 1];

            Menus.Header();
            Room.Rooms[0].View(fighter);
            Menus.ItemStats(fighter.CurrentConsumable);
        }

        private static void PrevConsumable(Fighter fighter)
        {
            int index = fighter.Consumables.IndexOf(fighter.CurrentConsumable);
            if (index == 0)
                fighter.CurrentConsumable = fighter.Consumables[fighter.Consumables.Count - 1];
            else fighter.CurrentConsumable = fighter.Consumables[index - 1];

            Menus.Header();
            Room.Rooms[0].View(fighter);
            Menus.ItemStats(fighter.CurrentConsumable);
        }
    }
}
