using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaxMana.Tools.Armour
{
    public class BronzeArmour : Item
    {
        public BronzeArmour()
        {
            this.Name = "Bronze Armour";
            this.UID = 8;
            this.Type = ItemType.Armour;
            this.Price = 80;
        }
        public override void UseItem(object target)
        {
            Character character = (Character)target;
            character.Defence += 6;
        }

        public override void UnuseItem(object target)
        {
            Character character = (Character)target;
            character.Defence -= 6;
        }
    }
}
