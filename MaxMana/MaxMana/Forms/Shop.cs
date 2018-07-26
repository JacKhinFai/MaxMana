using System.Drawing;
using System.Windows.Forms;

using MaxMana.UI;
using MaxMana.Tools;
using System;
using MaxMana.Tools.Consumable;
using MaxMana.Tools.Weapons;
using MaxMana.Tools.Armour;

namespace MaxMana.Forms
{
    public class Shop : Form
    {
        public Shop()
        {
            this.Icon = new Icon(@"Images\MaxMana.ico");
            ListBox buyListbox = new ListBox()
            {
                Height = 100,
                Left = 10,
                ScrollAlwaysVisible = true,
                Top = 10,
                Width = 160
            };
            // Fill buyListbox with sellable items

            string healthPrice = new HealthPotion().Price.ToString();
            string manaPrice = new ManaPotion().Price.ToString();
            string hiPrice = new HiPotion().Price.ToString();
            string bronzeWPrice = new BronzeSword().Price.ToString();
            string bronzeAPrice = new BronzeArmour().Price.ToString();
            string glassAPrice = new GlassArmour().Price.ToString();
            string chopstickPrice = new ChopStick().Price.ToString();

            buyListbox.Items.Add("Health Potion\t- £" + healthPrice);
            buyListbox.Items.Add("Mana Potion\t- £" + manaPrice);
            buyListbox.Items.Add("Hi Potion\t\t- £" + hiPrice);
            buyListbox.Items.Add("Bronze Sword\t- £" + bronzeWPrice);
            buyListbox.Items.Add("Bronze Armour\t- £" + bronzeAPrice);
            buyListbox.Items.Add("Glass Armour\t- £" + glassAPrice);
            buyListbox.Items.Add("ChopStick\t- £" + chopstickPrice);
            this.Controls.Add(buyListbox);

            CustomButton buyButton = new CustomButton(CustomButtonType.Buy)
            {
                Height = 40,
                Left = 10,
                Text = "BUY",
                Top = 110,
                Width = 100
            };
            buyButton.Click += (sender, args) =>
            {
                if (buyListbox.SelectedItem.ToString() == "Health Potion\t- £" + healthPrice)
                {
                    HealthPotion item = new HealthPotion();
                    if (World.character.Gold >= item.Price)
                    {
                        World.character.Items.Add(item);
                        World.character.Gold -= item.Price;
                        MessageBox.Show(string.Format("Item bought: {0}", item.Name));
                    }
                    else
                    {
                        //Messagebox
                        MessageBox.Show("STOP RIGHT THERE, CRIMINAL SCUM!\nI choose you Debt Collector.");
                        Battle battleForm = new Forms.Battle("Debt Collector", Image.FromFile("Images/Monsters/DebtCollector.png"), item);
                        battleForm.Show();
                        this.Close();
                    }
                }
                else if (buyListbox.SelectedItem.ToString() == "Mana Potion\t- £" + manaPrice)
                {
                    ManaPotion item = new ManaPotion();
                    if (World.character.Gold >= item.Price)
                    {
                        World.character.Items.Add(item);
                        World.character.Gold -= item.Price;
                        MessageBox.Show(string.Format("Item bought: {0}", item.Name));
                    }
                    else
                    {
                        //Messagebox
                        MessageBox.Show("STOP RIGHT THERE, CRIMINAL SCUM!\nI choose you Debt Collector.");
                        Battle battleForm = new Forms.Battle("Debt Collector", Image.FromFile("Images/Monsters/DebtCollector.png"), item);
                        battleForm.Show();
                        this.Close();
                    }
                }
                else if (buyListbox.SelectedItem.ToString() == "Hi Potion\t\t- £" + hiPrice)
                {
                    HiPotion item = new HiPotion();
                    if (World.character.Gold >= item.Price)
                    {
                        World.character.Items.Add(item);
                        World.character.Gold -= item.Price;
                        MessageBox.Show(string.Format("Item bought: {0}", item.Name));
                    }
                    else
                    {
                        //Messagebox
                        MessageBox.Show("STOP RIGHT THERE, CRIMINAL SCUM!\nI choose you Debt Collector.");
                        Battle battleForm = new Forms.Battle("Debt Collector", Image.FromFile("Images/Monsters/DebtCollector.png"), item);
                        battleForm.Show();
                        this.Close();
                    }
                }
                else if (buyListbox.SelectedItem.ToString() == "Bronze Sword\t- £" + bronzeWPrice)
                {
                    BronzeSword item = new BronzeSword();
                    if (World.character.Gold >= item.Price)
                    {
                        World.character.Items.Add(item);
                        World.character.Gold -= item.Price;
                        MessageBox.Show(string.Format("Item bought: {0}", item.Name));
                    }
                    else
                    {
                        //Messagebox
                        MessageBox.Show("STOP RIGHT THERE, CRIMINAL SCUM!\nI choose you Debt Collector.");
                        Battle battleForm = new Forms.Battle("Debt Collector", Image.FromFile("Images/Monsters/DebtCollector.png"), item);
                        battleForm.Show();
                        this.Close();
                    }
                }
                else if (buyListbox.SelectedItem.ToString() == "Bronze Armour\t- £" + bronzeAPrice)
                {
                    BronzeArmour item = new BronzeArmour();
                    if (World.character.Gold >= item.Price)
                    {
                        World.character.Items.Add(item);
                        World.character.Gold -= item.Price;
                        MessageBox.Show(string.Format("Item bought: {0}", item.Name));
                    }
                    else
                    {
                        //Messagebox
                        MessageBox.Show("STOP RIGHT THERE, CRIMINAL SCUM!\nI choose you Debt Collector.");
                        Battle battleForm = new Forms.Battle("Debt Collector", Image.FromFile("Images/Monsters/DebtCollector.png"), item);
                        battleForm.Show();
                        this.Close();
                    }
                }
                else if (buyListbox.SelectedItem.ToString() == "Glass Armour\t- £" + glassAPrice)
                {
                    GlassArmour item = new GlassArmour();
                    if (World.character.Gold >= item.Price)
                    {
                        World.character.Items.Add(item);
                        World.character.Gold -= item.Price;
                        MessageBox.Show(string.Format("Item bought: {0}", item.Name));
                    }
                    else
                    {
                        //Messagebox
                        MessageBox.Show("STOP RIGHT THERE, CRIMINAL SCUM!\nI choose you Debt Collector.");
                        Battle battleForm = new Forms.Battle("Debt Collector", Image.FromFile("Images/Monsters/DebtCollector.png"), item);
                        battleForm.Show();
                        this.Close();
                    }
                }
                else if (buyListbox.SelectedItem.ToString() == "ChopStick\t- £" + chopstickPrice)
                {
                    ChopStick item = new ChopStick();
                    if (World.character.Gold >= item.Price)
                    {
                        World.character.Items.Add(item);
                        World.character.Gold -= item.Price;
                        MessageBox.Show(string.Format("Item bought: {0}", item.Name));
                    }
                    else
                    {
                        //Messagebox
                        MessageBox.Show("STOP RIGHT THERE, CRIMINAL SCUM!\nI choose you Debt Collector.");
                        Battle battleForm = new Forms.Battle("Debt Collector", Image.FromFile("Images/Monsters/DebtCollector.png"), item);
                        battleForm.Show();
                        this.Close();
                    }
                }
            };
            this.Controls.Add(buyButton);

            ListBox sellListbox = new ListBox()
            {
                Height = 100,
                Left = 180,
                ScrollAlwaysVisible = true,
                Top = 10,
                Width = 100
            };
            // Fill sellListbox with player items
            this.Controls.Add(sellListbox);

            CustomButton sellButton = new CustomButton(CustomButtonType.Sell)
            {
                Height = 40,
                Left = 180,
                Text = "SELL",
                Top = 110,
                Width = 100
            };
            sellButton.Click += (sender, args) =>
            {
                switch (sellListbox.SelectedItem.ToString())
                {
                    case "Health Potion":
                        {
                            // Remove item from character inventory
                            // Add gold to character
                            break;
                        }
                    case "Mana Potion":
                        {
                            // Remove item from player inventory
                            // Add gold to character
                            break;
                        }
                }
            };
            this.Controls.Add(sellButton);
        }

        protected override void OnClosed(EventArgs e)
        {
            Map.Locked = false;
            base.OnClosed(e);
        }
    }
}
