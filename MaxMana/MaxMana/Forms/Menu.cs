using System.Drawing;
using System.Windows.Forms;
using MaxMana.UI;
using MaxMana.Tools;

namespace MaxMana.Forms
{
    public class Menu : Form
    {
        private ListBox mListbox;
        private Label mGold;
        private Label mHealth;
        private Label mMana;
        public Menu()
        {
            this.Icon = new Icon(@"Images\MaxMana.ico");
            this.mListbox = new ListBox()
            {
                Left = 100,
                Top = 90,
                Visible = false,
                Height = 160,
                Width = 100,
                ScrollAlwaysVisible = true
            };
            this.mListbox.SelectedIndexChanged += (sender, args) =>
            {
                if (this.mListbox.SelectedItem != null)
                {
                    string itemStr = this.mListbox.SelectedItem.ToString();
                    Item itemUsed = null;
                    foreach (Item i in World.character.Items)
                    {
                        if (i.Name == itemStr)
                        {
                            i.UseItem(World.character);
                            itemUsed = i;
                        }
                    }
                    if (itemUsed != null)
                    {
                        this.mListbox.Items.Remove(itemStr);
                        if (itemUsed.Type == ItemType.Consumable)
                        {
                            World.character.Items.Remove(itemUsed);
                        }
                        else if (itemUsed.Type == ItemType.Armour)
                        {
                            if (World.character.CurrentArmour != null)
                            {
                                World.character.CurrentArmour.UnuseItem(World.character);
                            }
                            World.character.CurrentArmour = itemUsed;

                            this.mListbox.Items.Clear();
                            foreach (Item i in World.character.Items)
                            {
                                if ((i.Type == ItemType.Weapon || i.Type == ItemType.Armour) && i != World.character.CurrentArmour && i != World.character.CurrentWeapon)
                                { this.mListbox.Items.Add(i.Name); }
                            }
                        }
                        else if (itemUsed.Type == ItemType.Weapon)
                        {
                            if (World.character.CurrentWeapon != null)
                            {
                                World.character.CurrentWeapon.UnuseItem(World.character);
                            }
                            World.character.CurrentWeapon = itemUsed;

                            this.mListbox.Items.Clear();
                            foreach (Item i in World.character.Items)
                            {
                                if ((i.Type == ItemType.Weapon || i.Type == ItemType.Armour) && i != World.character.CurrentArmour && i != World.character.CurrentWeapon)
                                { this.mListbox.Items.Add(i.Name); }
                            }
                        }
                    }
                    mHealth.Text = "Health: " + World.character.Health.ToString();
                    mMana.Text = "Mana: " + World.character.Mana.ToString();
                    mGold.Text = "Gold: " + World.character.Gold.ToString();
                }
            };
            this.Controls.Add(this.mListbox);

            CustomButton Save = new CustomButton(CustomButtonType.Save)
            {
                Left = 10,
                Top = 10,
            };
            Save.Click += (sender, args) =>
            {
                World.Save();
            };
            this.Controls.Add(Save);

            CustomButton Open = new CustomButton(CustomButtonType.Load)
            {
                Left = 100,
                Top = 10,
            };
            Open.Click += (sender, args) =>
            {
                World.Open();
            };
            this.Controls.Add(Open);

            CustomButton Items = new CustomButton(CustomButtonType.Items)
            {
                Left = 10,
                Top = 50,
            };
            Items.Click += (sender, args) =>
            {
                this.mListbox.Visible = !this.mListbox.Visible;
                this.mListbox.Items.Clear();
                foreach (Item i in World.character.Items)
                {
                    if (i.Type == ItemType.Consumable)
                    { this.mListbox.Items.Add(i.Name); }
                }
            };
            this.Controls.Add(Items);

            CustomButton Equipment = new CustomButton(CustomButtonType.Equip)
            {
                Left = 100,
                Top = 50,
            };
            Equipment.Click += (sender, args) =>
            {
                this.mListbox.Visible = !this.mListbox.Visible;
                this.mListbox.Items.Clear();
                foreach (Item i in World.character.Items)
                {
                    if ((i.Type == ItemType.Weapon || i.Type == ItemType.Armour) && i != World.character.CurrentArmour && i != World.character.CurrentWeapon)
                    { this.mListbox.Items.Add(i.Name); }
                }
            };
            this.Controls.Add(Equipment);

            mGold = new Label
            {
                AutoSize = true,
                Font = new Font("Arial", 10.0f, FontStyle.Bold),
                Height = 20,
                Left = 10,
                Text = string.Format("Gold: {0}", World.character.Gold.ToString()),
                Top = Equipment.Top + Equipment.Height + 10
            };
            this.Controls.Add(mGold);

            mHealth = new Label
            {
                AutoSize = true,
                Font = new Font("Arial", 10.0f, FontStyle.Bold),
                Height = 20,
                Left = 10,
                Text = string.Format("Health: {0}", World.character.Health.ToString()),
                Top = mGold.Top + mGold.Height + 10
            };
            this.Controls.Add(mHealth);

            mMana = new Label
            {
                AutoSize = true,
                Font = new Font("Arial", 10.0f, FontStyle.Bold),
                Height = 20,
                Left = 10,
                Text = string.Format("Mana: {0}", World.character.Mana.ToString()),
                Top = mHealth.Top + mHealth.Height + 10
            };
            this.Controls.Add(mMana);
        }
        
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.I || e.KeyCode == Keys.Escape) { this.Close(); }
        }
    }
}
