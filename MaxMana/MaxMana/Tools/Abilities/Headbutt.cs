using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaxMana.Forms;

namespace MaxMana.Tools.Abilities
{
    public class Headbutt : Spell
    {
        public Headbutt()
        {
            this.Name = "Headbutt";
            this.UID = 8;
        }
        public override void UseSpell(object target)
        {
            if (World.character.Health > (World.character.MaxHealth / 2))
            {
                Monster monster = (Monster)target;
                int damage = 50;
                int total = (int)monster.Health - damage;
                if (total < 0) { monster.Health = 0; }
                else { monster.Health = (uint)total; }
                World.character.Health -= (World.character.MaxHealth / 2);
            }
        }
    }
}
