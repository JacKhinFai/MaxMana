using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MaxMana.UI;
using MaxMana.Tools;
using WMPLib;

namespace MaxMana.Forms
{
    public class World : Form
    {
        public static Character character { get; set; }
        private static Map map;

        public World()
        {
            this.Icon = new Icon(@"Images\MaxMana.ico");
            this.WindowState = FormWindowState.Maximized;
            map = new Map(new Point(20, 10));
            //character = new Character(CharacterClass.Warrior, CharacterRace.Dwarf, "Billy Bob");
            this.Width = map.Width + 16;
            this.Height = map.Height + 39;
            this.Controls.Add(map);
            this.FormClosed += delegate { Application.Exit(); };
            RunMusic();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                case Keys.Left: { map.MoveLeft(); break; }
                case Keys.D:
                case Keys.Right: { map.MoveRight(); break; }
                case Keys.W:
                case Keys.Up: { map.MoveUp(); break; }
                case Keys.S:
                case Keys.Down: { map.MoveDown(); break; }
                case Keys.I: 
                case Keys.Escape: { Menu menu = new Menu(); menu.Show(); break; }
            }
        }

        public static void Save()
        {
            using (StreamWriter writer = new StreamWriter("data.save"))
            {
                writer.Write(string.Format("name:{0};", character.Name));
                writer.Write(string.Format("race:{0};", character.Race.ToString()));
                writer.Write(string.Format("class:{0};", character.Class.ToString()));
                writer.Write(string.Format("hp:{0};", character.Health.ToString()));
                writer.Write(string.Format("mhp:{0};", character.MaxHealth.ToString()));
                writer.Write(string.Format("mp:{0};", character.Mana.ToString()));
                writer.Write(string.Format("mmp:{0};", character.MaxMana.ToString()));
                writer.Write(string.Format("att:{0};", character.Attack.ToString()));
                writer.Write(string.Format("def:{0};", character.Defence.ToString()));
                writer.Write(string.Format("matt:{0};", character.MagicAttack.ToString()));
                writer.Write(string.Format("mdef:{0};", character.MagicDefence.ToString()));
                writer.Write(string.Format("exp:{0};", character.Exp.ToString()));
                writer.Write(string.Format("lvl:{0};", character.Level.ToString()));
                writer.Write(string.Format("gold:{0};", character.Gold.ToString()));
                writer.Write(string.Format("pos:{0},{1};", map.PlayerLocation.X.ToString(), map.PlayerLocation.Y.ToString()));
                foreach (Item i in character.Items)
                {
                    writer.Write(string.Format("item:{0};", i == character.CurrentArmour || i == character.CurrentWeapon ? i.UID.ToString() + "e" : i.UID.ToString()));
                }
                foreach (Spell i in character.Spells)
                {
                    writer.Write(string.Format("Abilities:{0};", i.UID.ToString()));
                }
            }

            MessageBox.Show("Save successful!");
        }

        public static void Open()
        {
            character = new Character();
            character.Items.Clear();
            using (StreamReader reader = new StreamReader("data.save"))
            {
                string data = reader.ReadToEnd();
                string[] dataSplit = data.Split(';');
                for (int i = 0; i < dataSplit.Length; i++)
                {
                    string current = dataSplit[i];
                    string[] currentSplit = current.Split(':');
                    switch (currentSplit[0])
                    {
                            //Object refernce not set to a instance (nullreference)
                        case "name": { character.Name = currentSplit[1]; break; }
                        case "race": { character.Race = (CharacterRace)Enum.Parse(typeof(CharacterRace), currentSplit[1]); break; }
                        case "class": { character.Class = (CharacterClass)Enum.Parse(typeof(CharacterClass), currentSplit[1]); break; }
                        case "hp": { character.Health = uint.Parse(currentSplit[1]); break; }
                        case "mhp": { character.MaxHealth = uint.Parse(currentSplit[1]); break; }
                        case "mp": { character.Mana = uint.Parse(currentSplit[1]); break; }
                        case "mmp": { character.MaxMana = uint.Parse(currentSplit[1]); break; }
                        case "att": { character.Attack = uint.Parse(currentSplit[1]); break; }
                        case "def": { character.Defence = uint.Parse(currentSplit[1]); break; }
                        case "matt": { character.MagicAttack = uint.Parse(currentSplit[1]); break; }
                        case "mdef": { character.MagicDefence = uint.Parse(currentSplit[1]); break; }
                        case "exp": { character.Exp = uint.Parse(currentSplit[1]); break; }
                        case "lvl": { character.Level = uint.Parse(currentSplit[1]); break; }
                        case "gold": { character.Gold = uint.Parse(currentSplit[1]); break; }
                        case "pos":
                            {
                                string[] nextSplit = currentSplit[1].Split(',');
                                Point p = new Point(int.Parse(nextSplit[0]), int.Parse(nextSplit[1]));
                                map.UpdatePlayerLocation(p);
                                break;
                            }
                        case "item":
                            {
                                //String input is not in the correct format
                                bool setEquip = false;
                                if (currentSplit[1][currentSplit[1].Length - 1] == 'e')
                                {
                                    setEquip = true;
                                    currentSplit[1] = currentSplit[1].Substring(0, currentSplit[1].Length - 1);
                                }
                                int itemUID = int.Parse(currentSplit[1]);
                                switch (itemUID)
                                {
                                    case 1:
                                    {
                                        character.Items.Add(new MaxMana.Tools.Consumable.HealthPotion());
                                        break;
                                    }
                                    case 2:
                                    {
                                        character.Items.Add(new MaxMana.Tools.Consumable.HiPotion());
                                        break;
                                    }
                                    case 3:
                                    {
                                        character.Items.Add(new MaxMana.Tools.Consumable.ManaPotion());
                                        break;
                                    }
                                    case 7:
                                    {
                                        character.Items.Add(new MaxMana.Tools.Weapons.BronzeSword());
                                        if (setEquip) { character.CurrentWeapon = character.Items[character.Items.Count - 1]; }
                                        break;
                                    }
                                    case 8:
                                    {
                                        character.Items.Add(new MaxMana.Tools.Armour.BronzeArmour());
                                        if (setEquip) { character.CurrentArmour = character.Items[character.Items.Count - 1]; }
                                        break;
                                    }
                                    case 666:
                                    {
                                        character.Items.Add(new MaxMana.Tools.Weapons.Excalibur());
                                        if (setEquip) { character.CurrentWeapon = character.Items[character.Items.Count - 1]; }
                                        break;
                                    }
                                    case 11:
                                    {
                                        character.Items.Add(new MaxMana.Tools.Weapons.ChopStick());
                                        if (setEquip) { character.CurrentWeapon = character.Items[character.Items.Count - 1]; }
                                        break;
                                    }
                                    case 9:
                                    {
                                        character.Items.Add(new MaxMana.Tools.Armour.GlassArmour());
                                        if (setEquip) { character.CurrentArmour = character.Items[character.Items.Count - 1]; }
                                        break;
                                    }
                                }
                                break;
                            }
                            //Spell save code
                        case "Abilities":
                            {
                                 int spellUID = int.Parse(currentSplit[1]);
                                 switch (spellUID)
                                 {
                                     case 1:
                                         {
                                             character.Spells.Add(new MaxMana.Tools.Abilities.QuickAttack());
                                             break;
                                         }
                                     case 2:
                                         {
                                             character.Spells.Add(new MaxMana.Tools.Abilities.Fira());
                                             break;
                                         }
                                     case 3:
                                         {
                                             character.Spells.Add(new MaxMana.Tools.Abilities.Beserk());
                                             break;
                                         }
                                     case 4:
                                         {
                                             character.Spells.Add(new MaxMana.Tools.Abilities.Fatality());
                                             break;
                                         }
                                     case 5:
                                         {
                                             character.Spells.Add(new MaxMana.Tools.Abilities.WhirlWind());
                                             break;
                                         }
                                     case 6:
                                         {
                                             character.Spells.Add(new MaxMana.Tools.Abilities.Steal());
                                             break;
                                         }
                                 }
                                 break;
                            }
                    }
                }
            }
            
            MessageBox.Show("Welcome Back!");
        }
        public static void RunMusic()
        {
            FormMainMenu.BackgroundM.controls.stop();
            FormMainMenu.BackgroundM.URL = FormMainMenu.musicURL[2];
            FormMainMenu.BackgroundM.controls.play();
        }
    }
}
