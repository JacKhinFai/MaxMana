using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaxMana.Forms;

namespace MaxMana.Tools.Abilities
{
    public class Fatality : Spell
    {
        public Fatality()
        {
            this.Name = "Fatality";
            this.UID = 4;
        }
        public override void UseSpell(object target)
        {
            if (World.character.Mana >= 15)
            {
                Monster monster = (Monster)target;
                Random ran = new Random();
                int value = ran.Next(0, 5);
                if(value == 0)
                {
                    monster.Health = 0;
                }
                World.character.Mana -= 15;
            }
        }
    }
}
