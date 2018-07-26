using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaxMana.Tools.Consumable
{
    public class ManaPotion : Item
    {
        public ManaPotion()
        {
            this.Name = "Mana Potion";
            this.UID = 3;
            this.Type = ItemType.Consumable;
            this.Price = 25;
        }

        public override void UseItem(object target)
        {
            Character character = (Character)target;
            character.Mana += 10;
            if (character.Mana > character.MaxMana) { character.Mana = character.MaxMana; }
        }

    }
}
