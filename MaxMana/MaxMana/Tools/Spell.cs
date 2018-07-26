using System.Windows.Forms;

namespace MaxMana.Tools
{
    public abstract class Spell
    {
        public string Name { get; protected set; }
        public uint UID { get; protected set; }

        public virtual void UseSpell(object target)
        {
            MessageBox.Show("Spells not implemented");
        }
    }
}