using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using MaxMana.UI;
using MaxMana.Tools;
using MaxMana.Forms;

namespace MaxMana.Forms
{
    public class Battle : Form
    {
        private Image mCharacter;
        private Image mMonster;

        private Monster monster;
        private ProgressBar mhp;
        private ProgressBar mmp;
        private ProgressBar php;
        private ProgressBar pmp;
        private ListBox Magicbox;
        private Label MonName;
        
        public Battle(string monsterName, Image monsterImage, Item prize)
        {
            this.Size = new Size(500, 500);
            //this.TopMost = true;
            Random random = new Random();
            this.WindowState = FormWindowState.Minimized;
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Icon = new Icon(@"Images\MaxMana.ico");
            this.FormClosed += delegate { World.RunMusic(); };

            this.MonName = new Label()
            {
                AutoSize = true,
                Font = new Font("Arial", 12.0f, FontStyle.Bold),
                Left = 10,
                Text = monsterName,
                Top = 10
            };
            this.Controls.Add(MonName);

            //////////////////////////////////////////////
            this.Magicbox = new ListBox()
            {
                Left = 200,
                Top = 370,
                Visible = false,
            };
            this.Magicbox.Click += (sender, args) =>
            {
                if (this.Magicbox.SelectedItem != null)
                {
                    string spellStr = this.Magicbox.SelectedItem.ToString();
                    foreach (Spell i in World.character.Spells)
                    {
                        if (i.Name == spellStr)
                        {
                            i.UseSpell(this.monster);
                            this.mhp.Value = (int)monster.Health < 0 ? 0 : (int)monster.Health;
                            if (this.mhp.Value < (int)(monster.MaxHealth / 3)) { ProgressExt.SetState(mhp, 2); }
                            if (this.mhp.Value == 0)
                            {
                                // Give gold and EXP.
                                uint expGain = (uint)((monster.MaxHealth + monster.Mana + monster.Attack + monster.Defence + monster.Magic_Attack + monster.Magic_Defence) / 2 + random.Next(-1, 2));
                                World.character.Exp += expGain;
                                uint goldGain = (uint)((monster.MaxHealth + monster.Mana + monster.Attack + monster.Defence + monster.Magic_Attack + monster.Magic_Defence) / 5 + random.Next(-1, 2));
                                World.character.Gold += goldGain;
                                if (prize != null) { World.character.Items.Add(prize); }
                                MessageBox.Show(string.Format("Battle Won!\nExp:\t{0} -> {1}\nGold:\t{2} -> {3}\nItem:\t{4}", expGain, World.character.Exp.ToString(), goldGain, World.character.Gold.ToString(), prize != null ? prize.Name : "NONE"));
                                World.character.Levelup();
                                if (MonName.Text == "Big Bad Boss")
                                {
                                    Map.BossBeaten = true;
                                    //Message boss beaten
                                    MessageBox.Show("Congratulations, you have won the Game!");
                                }
                                this.Close();
                                Map.Locked = false;
                                return;
                            }

                            if (World.character.Health - (monster.Attack - (World.character.Defence) / 4) < 0) { World.character.Health = 0; }
                            else { World.character.Health -= (monster.Attack - (World.character.Defence) / 4); }
                            this.php.Value = (int)World.character.Health < 0 ? 0 : (int)World.character.Health;
                            if (this.php.Value < (int)(World.character.MaxHealth / 3)) { ProgressExt.SetState(php, 2); }

                            if (this.php.Value == 0)
                            {
                                // Character dies.
                                MessageBox.Show("You failed in Life");
                                this.Close();
                                Map.Locked = false;
                                World.Open();
                                return;
                            }
                        }
                    }
                }
            };
            this.Controls.Add(Magicbox);
            //////////////////////////////////////////////

            CustomButton Attackb = new CustomButton(CustomButtonType.Attack)
            {
                Left = 10,
                Top = 370,

            };
            Attackb.Click += (sender, ard) =>
            {
                uint damage = 0;
                int value = ((int)World.character.Attack - ((int)monster.Defence) / 4);
                if (value <= 0) { damage = 1; }
                else { damage = (uint)value; }
                if (monster.Health - damage < 0) { monster.Health = 0; }
                else { monster.Health -= damage; }
                this.mhp.Value = (int)monster.Health < 0 ? 0 : (int)monster.Health;
                if (this.mhp.Value < (int)(monster.MaxHealth / 3)) { ProgressExt.SetState(mhp, 2); }

                if (this.mhp.Value == 0)
                {
                    // Give gold and EXP.
                    uint expGain = (uint)((monster.MaxHealth + monster.Mana + monster.Attack + monster.Defence + monster.Magic_Attack + monster.Magic_Defence) / 2 + random.Next(-1, 2));
                    World.character.Exp += expGain;
                    uint goldGain = (uint)((monster.MaxHealth + monster.Mana + monster.Attack + monster.Defence + monster.Magic_Attack + monster.Magic_Defence) / 5 + random.Next(-1, 2));
                    World.character.Gold += goldGain;
                    if (prize != null) { World.character.Items.Add(prize); }
                    MessageBox.Show(string.Format("Battle Won!\nExp:\t{0} -> {1}\nGold:\t{2} -> {3}\nItem:\t{4}", expGain, World.character.Exp.ToString(), goldGain, World.character.Gold.ToString(), prize != null ? prize.Name : "NONE"));
                    World.character.Levelup();
                    if (MonName.Text == "Big Bad Boss")
                    {
                        Map.BossBeaten = true;
                        //Message boss beaten
                        MessageBox.Show("Congratulations, you have won the Game!");
                    }
                    this.Close();
                    Map.Locked = false;
                    return;
                }

                if (World.character.Health - (monster.Attack - (World.character.Defence) / 4) < 0) { World.character.Health = 0; }
                else { World.character.Health -= (monster.Attack - (World.character.Defence) / 4); }
                this.php.Value = (int)World.character.Health < 0 ? 0 : (int)World.character.Health;
                if (this.php.Value < (int)(World.character.MaxHealth / 3)) { ProgressExt.SetState(php, 2); }

                if (this.php.Value == 0)
                {
                    // Character dies.
                    MessageBox.Show("You failed in Life");
                    this.Close();
                    Map.Locked = false;
                    World.Open();
                    return;
                }
            };
            CustomButton Magicb = new CustomButton(CustomButtonType.Magic)
            {
                Left = 100,
                Top = 370,
            };
            Magicb.Click += (sender, ard) =>
            {
                /*if (monster.Health - World.character.MagicAttack < 0) { monster.Health = 0; }
                else { monster.Health -= World.character.MagicAttack; }

                if (monster.Health == 0) { this.Close(); }*/
                this.Magicbox.Visible = !this.Magicbox.Visible;
                foreach (Spell i in World.character.Spells)
                {
                    this.Magicbox.Items.Add(i.Name);
                }
            };
            this.Controls.Add(Magicb);
            CustomButton Flee = new CustomButton(CustomButtonType.Flee)
            {
                Left = 10,
                Top = 400,
            };
            Flee.Click += (sender, ard) =>
            {
                int value = random.Next(0, 3);
                if (value == 0)
                {
                    MessageBox.Show("Only a Coward would Runs Away");
                    this.Close();
                    Map.Locked = false;
                    return;
                }

                if (World.character.Health - (monster.Attack - (World.character.Defence) / 4) < 0) { World.character.Health = 0; }
                else { World.character.Health -= (monster.Attack - (World.character.Defence) / 4); }
                this.php.Value = (int)World.character.Health < 0 ? 0 : (int)World.character.Health;
                if (this.php.Value < (int)(World.character.MaxHealth / 3)) { ProgressExt.SetState(php, 2); }

                if (this.php.Value == 0)
                {
                    // Character dies.
                    MessageBox.Show("You failed in Life");
                    this.Close();
                    Map.Locked = false;
                    World.Open();
                    return;
                }
            };
            CustomButton Defend = new CustomButton(CustomButtonType.Defend)
            {
                Left = 100,
                Top = 400,
            };
            Defend.Click += (sender, ard) =>
            {
                /*if (World.character.Health - (uint)((monster.Attack - (World.character.Defence) / 4) / 2) < 0) { World.character.Health = 0; }
                else { World.character.Health -= (uint)((monster.Attack - (World.character.Defence) / 4) / 2); }
                this.php.Value = (int)World.character.Health < 0 ? 0 : (int)World.character.Health;
                if (this.php.Value < (int)(World.character.MaxHealth / 3)) { ProgressExt.SetState(php, 2); }

                if (this.php.Value == 0)
                {
                    // Character dies.
                    MessageBox.Show("You failed in Life");
                    this.Close();
                    Map.Locked = false;
                    World.Open();
                    return;
                }*/
                MaxMana.Forms.Menu menu = new MaxMana.Forms.Menu();
                menu.Show();
            };

            this.Controls.Add(Attackb);
            this.Controls.Add(Flee);
            this.Controls.Add(Defend);
            this.mCharacter = Image.FromFile("Images/vivi.png");
            string name = string.Empty;
            if (monsterName == null && monsterImage == null)
            {
                int mon = random.Next(0, 9);
                //Monster Image
                switch (mon)
                {
                    case 0:
                        {
                            name = "Billy";
                            this.mMonster = Image.FromFile("Images/Monsters/Billy.png");
                            
                            break;
                        }
                    case 1:
                        {
                            name = "Sven";
                            this.mMonster = Image.FromFile("Images/Monsters/Sven.png");
                            break;
                        }
                    case 2:
                        {
                            name = "Derp";
                            this.mMonster = Image.FromFile("Images/Monsters/Derp.png");
                            break;
                        }
                    case 3:
                        {
                            name = "Ghostsicle";
                            this.mMonster = Image.FromFile("Images/Monsters/Ghostsicle.png");
                            break;
                        }
                    case 4:
                        {
                            name = "HummingBird";
                            this.mMonster = Image.FromFile("Images/Monsters/Hummingbird.png");
                            break;
                        }
                    case 5:
                        {
                            name = "Hydra";
                            this.mMonster = Image.FromFile("Images/Monsters/Hydra.png");
                            break;
                        }
                    case 6:
                        {
                            name = "Mammy";
                            this.mMonster = Image.FromFile("Images/Monsters/Mammy.png");
                            break;
                        }
                    case 7:
                        {
                            name = "Reaper";
                            this.mMonster = Image.FromFile("Images/Monsters/Reaper.png");
                            break;
                        }
                    case 8:
                        {
                            name = "Yinyang";
                            this.mMonster = Image.FromFile("Images/Monsters/Yinyang.png");
                            break;
                        }
                    case 9:
                        {
                            name = "Big Bad Boss";
                            this.mMonster = Image.FromFile("Images/Merchant.png");
                            break;
                        }
                }
            }
            else { name = monsterName; this.mMonster = monsterImage; }

            this.monster = new Monster(name,
                (uint)random.Next(10 + (int)(4 * World.character.Level), 14 + (int)(4 * World.character.Level)),
                (uint)random.Next(0 + (int)(4 * World.character.Level), 1 + (int)(4 * World.character.Level)),
                (uint)random.Next(3 + (int)(2 * World.character.Level), 5 + (int)(2 * World.character.Level)),
                (uint)random.Next(3 + (int)(2 * World.character.Level), 5 + (int)(2 * World.character.Level)),
                (uint)random.Next(0 + (int)(2 * World.character.Level), 1 + (int)(2 * World.character.Level)),
                (uint)random.Next(0 + (int)(2 * World.character.Level), 1 + (int)(2 * World.character.Level))
            );
            if (name == "Debt Collector")
            {
                this.monster = new Monster(name,
                    this.monster.Health * (uint)Math.Floor((double)prize.Price / 10),
                    this.monster.Mana * (uint)Math.Floor((double)prize.Price / 10),
                    this.monster.Attack * (uint)Math.Floor((double)prize.Price / 10),
                    this.monster.Defence * (uint)Math.Floor((double)prize.Price / 10),
                    this.monster.Magic_Attack * (uint)Math.Floor((double)prize.Price / 10),
                    this.monster.Magic_Defence * (uint)Math.Floor((double)prize.Price / 10));
            }
            if (name == "Big Bad Boss")
            {
                this.monster = new Monster(name,
                this.monster.Health * 10,
                this.monster.Mana * 10,
                this.monster.Attack * 10,
                this.monster.Defence * 10,
                this.monster.Magic_Attack * 10,
                this.monster.Magic_Defence * 10);
            }
            StartBattle();
            RunMusic();
        }

        public void StartBattle()
        {
            mhp = new ProgressBar() { Height = 15, Width = 200,
                Maximum = (int)this.monster.Health, Minimum = 0,
                Value = (int)this.monster.Health, 
                Left = 10, Top = 50 };
            this.Controls.Add(mhp);
            mmp = new ProgressBar() { Height = 10, Width = 30,
                Maximum = (int)this.monster.Mana, Minimum = 0,
                Value = (int)this.monster.Mana,
                Left = 10, Top = 60 };
            this.Controls.Add(mmp);
            ProgressExt.SetState(mmp, 3);
            php = new ProgressBar() { Height = 10, Width = 30,
                Maximum = (int)World.character.MaxHealth, 
                Minimum = 0, Value = (int)World.character.Health, 
                Left = 10, Top = 300 };
            this.Controls.Add(php);
            pmp = new ProgressBar() { Height = 10, Width = 30,
                Maximum = (int)World.character.MaxMana, Minimum = 0, 
                Value = (int)World.character.Mana,
                Left = 10, Top = 310 };
            this.Controls.Add(pmp);
            ProgressExt.SetState(pmp, 3);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Position X, Y Image Width, Height
            e.Graphics.DrawImage(this.mMonster, 350, 30, /*this.mMonster.Width*/ 100, 100);
        }
        private static void RunMusic()
        {
            FormMainMenu.BackgroundM.controls.stop();
            FormMainMenu.BackgroundM.URL = FormMainMenu.musicURL[0];
            FormMainMenu.BackgroundM.controls.play();
        }
    }

    public static class ProgressExt
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr w, IntPtr l);
        public static void SetState(this ProgressBar pBar, int state)
        {
            try { SendMessage(pBar.Handle, 1040, (IntPtr)state, IntPtr.Zero); }
            catch { }
        }
    }
}
