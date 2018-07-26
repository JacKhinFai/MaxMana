using System.Drawing;
using System.Windows.Forms;
using MaxMana.UI;
using MaxMana.Tools;
using MaxMana.Forms;
using WMPLib;

namespace MaxMana
{
    public partial class FormMainMenu : Form
    {
        public static WindowsMediaPlayer BackgroundM = new WindowsMediaPlayer();
        public static string[] musicURL = new string[]
        {
            "Music/FlashFloodGuitar.mid",
            "Music/HordeEnsemble.mid",
            "Music/RideEnsemble.mid",
            "Music/TartarusEnsemble.mid"
        };
        public FormMainMenu()
        {
            InitializeComponent();
            this.Width = 800;
            this.Height = 600;
            this.Icon = new Icon(@"Images\MaxMana.ico");
            this.Text = "MaxMana";
            this.BackgroundImage = Properties.Resources.BG;
            //Image image = Properties.Resources.Name;

            CustomButton Start = new CustomButton(CustomButtonType.Start)
            {
                Left = 300,
                Top = 350,
            };
            Start.Click += (sender, args) =>
            {
                CharacterCreation ccform = new CharacterCreation();
                ccform.Show();
                this.WindowState = FormWindowState.Minimized;
            };
            this.Controls.Add(Start);
            CustomButton Open = new CustomButton(CustomButtonType.Open)
            {
                Left = 350,
                Top = 450,
            };
            Open.Click += (sender, args) =>
            {
                World wform = new World();
                wform.Show();
                World.Open();
                this.WindowState = FormWindowState.Minimized;
            };
            this.Controls.Add(Open);

            RunMusic();
        }

        private static void RunMusic()
        {
            BackgroundM.PlayStateChange += (newState) =>
            {
                if ((WMPPlayState)newState == WMPPlayState.wmppsStopped)
                    BackgroundM.controls.play();
            };
            BackgroundM.MediaError += (err) =>
            {
                MessageBox.Show("Cannot play media File.");
            };
            BackgroundM.URL = musicURL[3];
            BackgroundM.controls.play();
        }
    }
}
