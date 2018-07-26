using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaxMana.Forms;

namespace MaxMana.Tools.Abilities
{
    public class QuickAttack : Spell
    {
        public QuickAttack()
        {
            this.Name = "Quick Attack";
            this.UID = 1;
        }
        public override void UseSpell(object target)
        {
            if (World.character.Mana >= 6)
            {
                Monster monster = (Monster)target;
                int damage = (int)(World.character.Attack * 5) - (int)(monster.Defence / 4);
                if (damage < 0) { damage = 1; }
                int total = (int)monster.Health - damage;
                if (total < 0) { monster.Health = 0; }
                else { monster.Health = (uint)total; }
                World.character.Mana -= 6;
            }
        }
    }
}
