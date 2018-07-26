using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaxMana.Forms;

namespace MaxMana.Tools.Abilities
{
    public class Beserk : Spell
    {
        public Beserk()
        {
            this.Name = "Beserk";
            this.UID = 3;
        }
        public override void UseSpell(object target)
        {
            if (World.character.Mana >= 4)
            {
                /*
                int damage = (int)(World.character.Attack * 2) - (int)(monster.Defence / 2);
                if (damage < 0) { damage = 1; }
                int total = (int)monster.Health - damage;
                if (total < 0) { monster.Health = 0; }
                else { monster.Health = (uint)total; }*/
                Monster monster = (Monster)target;
                Random ran = new Random();
                int value = ran.Next(0, 5);
                if (value == 0 || value == 1 || value == 2)
                {
                    int damage = (int)(World.character.Attack * 2) - (int)(monster.Defence / 2);
                    if (damage < 0) { damage = 1; }
                    int total = (int)monster.Health - damage;
                    if (total < 0) { monster.Health = 0; }
                    else { monster.Health = (uint)total; }
                    int lifesteal = damage / 4;
                    if (World.character.Health < World.character.MaxHealth)
                    { World.character.Health += (uint)lifesteal; }
                   else { World.character.Health = World.character.MaxHealth; }
                }
                else if (value == 3 || value == 4)
                {
                    int damage = (int)(World.character.Attack * 2) - (int)(monster.Defence / 2);
                    if (damage < 0) { damage = 1; }
                    int total = (int)monster.Health - damage;
                    if (total < 0) { monster.Health = 0; }
                    else { monster.Health = (uint)total; }
                }
                World.character.Mana -= 4;
            }
        }
    }
}
