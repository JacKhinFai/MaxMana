using System.Windows.Forms;

namespace MaxMana.Tools
{
    public abstract class Item
    {
        public string Name { get; protected set; }
        public uint UID { get; protected set; }
        public ItemType Type { get; protected set; }
        public uint Price { get; protected set; }


        public virtual void UseItem(object target)
        {
            MessageBox.Show("Item not implemented");
        }

        public virtual void UnuseItem(object target)
        {
            MessageBox.Show("Item not implemented");
        }
    }
}
