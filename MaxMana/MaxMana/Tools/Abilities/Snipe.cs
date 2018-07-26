using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaxMana.Forms;

namespace MaxMana.Tools.Abilities
{
    public class Snipe : Spell
    {
        public Snipe()
        {
            this.Name = "Snipe";
            this.UID = 7;
        }
        public override void UseSpell(object target)
        {
            if (World.character.Mana >= 3)
            {
                Monster monster = (Monster)target;
                int damage = 5;
                int total = (int)monster.Health - damage;
                if (total < 0) { monster.Health = 0; }
                else { monster.Health = (uint)total; }
                World.character.Mana -= 2;
            }
        }
    }
}
