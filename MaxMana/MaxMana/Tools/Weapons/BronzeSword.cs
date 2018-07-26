using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaxMana.Tools.Weapons
{
    public class BronzeSword : Item
    {
        public BronzeSword()
        {
            this.Name = "Bronze Sword";
            this.UID = 7;
            this.Type = ItemType.Weapon;
            this.Price = 50;
        }
        public override void UseItem(object target)
        {
            Character character = (Character)target;
            character.Attack += 6;
        }
        public override void UnuseItem(object target)
        {
            Character character = (Character)target;
            character.Attack -= 6;
        }
    }
}
