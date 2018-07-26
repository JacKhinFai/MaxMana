using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaxMana.Tools.Weapons
{
    public class Excalibur : Item
    {
        public Excalibur()
        {
            this.Name = "Excalibur";
            this.UID = 666;
            this.Type = ItemType.Weapon;
            this.Price = 9001;
        }
        public override void UseItem(object target)
        {
            Character character = (Character)target;
            character.Attack += 666;
        }
        public override void UnuseItem(object target)
        {
            Character character = (Character)target;
            character.Attack -= 666;
        }
    }
}
