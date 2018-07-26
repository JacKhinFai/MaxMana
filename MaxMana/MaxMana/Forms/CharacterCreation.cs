using System;
using System.Drawing;
using System.Windows.Forms;
using MaxMana.Tools;
using MaxMana.UI;

namespace MaxMana.Forms
{
    public class CharacterCreation : Form
    {
        public CharacterCreation()
        {
            this.Icon = new Icon(@"Images\MaxMana.ico");

            //Name//
            TextBox namebox = new TextBox()
            {
                Text = "Enter Name"
            };
            this.Controls.Add(namebox);

            //Textbox//
            TextBox raceDescribe = new TextBox()
            {
                Top = 80,
                Width = 100,
                Left = 10,
                Height = 100,
                Multiline = true
            };
            this.Controls.Add(raceDescribe);
            //RaceBox//
            ComboBox racebox = new ComboBox()
            {
                Top = 20,
            };
            racebox.Items.Add("Human");
            racebox.Items.Add("Dwarf");
            racebox.Items.Add("Elf");
            racebox.SelectedItem = racebox.Items[0];
            racebox.SelectionChangeCommitted += (sender, args) =>
            {

                //this.CreateGraphics().FillRectangle(new SolidBrush(this.BackColor), 0, 0, this.Width, this.Height);
                switch (racebox.SelectedItem.ToString())
                {
                    case "Human":
                        {
                            raceDescribe.Text = "It's a human. This is a balanced class.";
                            break;
                        }
                    case "Dwarf":
                        {
                            raceDescribe.Text = "A strong Dwarf race which has high strength enabling you to do more physical damage to enemies.";
                            break;
                        }
                    case "Elf":
                        {
                            raceDescribe.Text = "A shockingly Magicial class that has high mana and magic at their disposal.";
                            break;
                        }
                }
            };
            this.Controls.Add(racebox);

            //ClassBox//
            ComboBox classbox = new ComboBox()
            {
                Top = 50,
            };
            classbox.Items.Add("Warrior");
            classbox.Items.Add("Rogue");
            classbox.Items.Add("Mage");
            classbox.SelectedItem = classbox.Items[0];
            classbox.SelectionChangeCommitted += (sender, args) =>
            {

                this.CreateGraphics().FillRectangle(new SolidBrush(this.BackColor), 0, 0, this.Width, this.Height);
                switch (classbox.SelectedItem.ToString())
                {
                    case "Mage":
                        {
                            this.CreateGraphics().DrawImage(Image.FromFile(@"Images\vivi.png"), 200, 0);
                            break;
                        }
                    case "Warrior":
                        {
                            Image image = Image.FromFile(@"Images\Warrior.png");
                            //this.CreateGraphics().DrawImage(Image.FromFile(@"Images\Warrior.png"), new Rectangle(180, 0, 100, 100), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
                            this.CreateGraphics().DrawImage(Image.FromFile(@"Images\Warrior.png"), new Rectangle(180, 0, 50, 50), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
                            break;
                        }
                    case "Rogue":
                        {
                            this.CreateGraphics().DrawImage(Image.FromFile(@"Images\Ninja.png"), 200, 0);
                            break;
                        }
                }
            };
            this.Controls.Add(classbox);

            CustomButton Create = new CustomButton(CustomButtonType.Create)
            {
                Left = 200,
                Top = 200,
            };
            Create.Click += (sender, args) =>
            {
                World wform = new World();
                World.character = new Character((CharacterClass)Enum.Parse(typeof(CharacterClass), classbox.Text), (CharacterRace)Enum.Parse(typeof(CharacterRace), racebox.Text), namebox.Text);
                wform.Show();
                this.Close();
            };
            this.Controls.Add(Create);
        }
    }
}
