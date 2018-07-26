using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaxMana.Tools.Armour
{
    public class GlassArmour : Item
    {
        public GlassArmour()
        {
            this.Name = "Glass Armour";
            this.UID = 9;
            this.Type = ItemType.Armour;
            this.Price = 100;
        }
        public override void UseItem(object target)
        {
            Character character = (Character)target;
            character.MagicDefence += 3;
            character.Defence += 8;
        }
        public override void UnuseItem(object target)
        {
            Character character = (Character)target;
            character.MagicDefence -= 3;
            character.Defence -= 8;
        }
    }
}
