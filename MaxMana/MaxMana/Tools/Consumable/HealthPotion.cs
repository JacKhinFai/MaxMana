using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaxMana.Tools.Consumable
{ 
    public class HealthPotion : Item
    {
        public HealthPotion()
        {
            this.Name = "Health Potion";
            this.UID = 1;
            this.Type = ItemType.Consumable;
            this.Price = 25;
        }

        public override void UseItem(object target)
        {
            Character character = (Character)target;
            character.Health += 10;
            if (character.Health > character.MaxHealth) { character.Health = character.MaxHealth; }
        }
    }
}
