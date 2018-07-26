using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaxMana.Tools.Weapons
{
    public class ChopStick: Item
    {
        public ChopStick()
        {
            this.Name = "ChopStick";
            this.UID = 11;
            this.Type = ItemType.Weapon;
            this.Price = 1111;
        }
        public override void UseItem(object target)
        {
            Character character = (Character)target;
            character.MagicAttack += 11;
            character.Attack += 11;
        }
        public override void UnuseItem(object target)
        {
            Character character = (Character)target;
            character.MagicAttack -= 11;
            character.Attack -= 11;
        }
    }
}
