using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaxMana.Forms;

namespace MaxMana.Tools.Abilities
{
        public class Fira : Spell
        {
            public Fira()
            {
                this.Name = "Fira";
                this.UID = 2;
            }
            public override void UseSpell(object target)
            {
                if (World.character.Mana >= 8)
                {
                    Monster monster = (Monster)target;
                    int damage = (int)(World.character.MagicAttack * 2.5) - (int)(monster.Magic_Defence / 2);
                    if (damage < 0) { damage = 1; }
                    int total = (int)monster.Health - damage;
                    if (total < 0) { monster.Health = 0; }
                    else { monster.Health = (uint)total; }
                World.character.Mana -= 8;
            }
            }
        }
}
