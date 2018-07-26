using System.Drawing;
using System.Windows.Forms;
using MaxMana.Tools;

namespace MaxMana.UI
{
    public class CustomButton : Panel
    {
        public Image Image { get; private set; }
        public CustomButtonType ButtonType { get; private set; }

        public CustomButton(CustomButtonType type)
        {
            this.ButtonType = type;

            switch (this.ButtonType)
            {
                case CustomButtonType.Attack: { this.Image = Image.FromFile("Images/Buttons/Attack.png"); break; }
                case CustomButtonType.Defend: { this.Image = Image.FromFile("Images/Buttons/Defend.png"); break; }
                case CustomButtonType.Magic: { this.Image = Image.FromFile("Images/Buttons/Magic.png"); break; }
                case CustomButtonType.Flee: { this.Image = Image.FromFile("Images/Buttons/Flee.png"); break; }
                case CustomButtonType.Save: { this.Image = Image.FromFile("Images/Buttons/Save.png"); break; }
                case CustomButtonType.Load: { this.Image = Image.FromFile("Images/Buttons/Load.png"); break; }
                case CustomButtonType.Buy: { this.Image = Image.FromFile("Images/Buttons/Buy.png"); break; }
                case CustomButtonType.Sell: { this.Image = Image.FromFile("Images/Buttons/Sell.png"); break; }
                case CustomButtonType.Start: { this.Image = Image.FromFile("Images/Buttons/Start Game.png"); break; }
                case CustomButtonType.Open: { this.Image = Image.FromFile("Images/Buttons/Loading.png"); break; }
                case CustomButtonType.Create: { this.Image = Image.FromFile("Images/Buttons/Create.png"); break; }
                case CustomButtonType.Items: { this.Image = Image.FromFile("Images/Buttons/Items.png"); break; }
                case CustomButtonType.Equip: { this.Image = Image.FromFile("Images/Buttons/Equip.png"); break; }
            }

            this.Width = this.Image.Width;
            this.Height = this.Image.Height;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImage(this.Image, 0, 0, this.Width, this.Height);
        }
    }
}
