using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaxMana.Forms;

namespace MaxMana.Tools.Abilities
{
    public class CoinToss : Spell
    {
        public CoinToss()
        {
            this.Name = "CoinToss";
            this.UID = 9;
        }
        public override void UseSpell(object target)
        {
            if (World.character.Mana >= 2)
            {
                Random ran = new Random();
                if (ran.Next(0, 2) == 0)
                {
                    Monster monster = (Monster)target;
                    monster.Health /= 2;
                }
                else
                {
                    World.character.Health /= 2;
                }
                World.character.Mana -= 2;
            }
        }
    }
}
