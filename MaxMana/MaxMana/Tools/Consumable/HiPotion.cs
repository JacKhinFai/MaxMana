using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaxMana.Tools.Consumable
{
    public class HiPotion : Item
    {
        public HiPotion()
        {
            this.Name = "Hi-Potion";
            this.UID = 2;
            this.Type = ItemType.Consumable;
            this.Price = 50;
        }

        public override void UseItem(object target)
        {
            Character character = (Character)target;
            character.Health += 20;
            if (character.Health > character.MaxHealth) { character.Health = character.MaxHealth; }
        }
    }
}
