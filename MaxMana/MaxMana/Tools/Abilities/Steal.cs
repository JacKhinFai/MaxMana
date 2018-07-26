using System;
using System.Windows.Forms;

using MaxMana.Forms;

namespace MaxMana.Tools.Abilities
{
    public class Steal : Spell
    {
        public Steal()
        {
            this.Name = "Steal";
            this.UID = 6;
        }
        public override void UseSpell(object target)
        {
            if (World.character.Mana >= 6)
            {
                Random ran = new Random();
                int value = ran.Next(0, 25);
                Monster monster = (Monster)target;
                int damage = (int)(World.character.Attack * 2) - (int)(monster.Defence / 2);
                if (damage < 0) { damage = 1; }
                int total = (int)monster.Health - damage;
                if (total < 0) { monster.Health = 0; }
                else { monster.Health = (uint)total; }
                World.character.Gold += (uint)value;
                MessageBox.Show(string.Format("Gold stolen: {0}", value.ToString()));
                World.character.Mana -= 6;
            }
        }
    }
}
