using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MaxMana.Tools;
using MaxMana.Forms;

namespace MaxMana.UI
{
    public class Map : Panel
    {
        private const int TILE_WIDTH = 32;
        private const int TILE_HEIGHT = 32;
        private const int MONSTER_COUNT = 20;

        public Tile[,] Tiles { get; private set; }
        public Point PlayerLocation { get; private set; }
        public Point StartLocation { get; private set; }
        public Point ShopLocation { get; private set; }
        public Point FinalBossLocation { get; private set; }
        public static bool Locked { get; set; }
        private Image mGrass;
        private Image mStone;
        private Image mWater;
        private Image mWater1;
        private Image mWater2;
        private Image mTree;
        private Image mTree1;
        private Image mTree2;
        private Image mRock;
        private Image mSand;
        private Image mCharacter;
        private Image mShop;
        private Image mFinalBoss;
        private bool mLeft;
        private bool mWaterTwo = false;
        private bool mTreeTwo = false;

        ////////////////////////////////////
        private bool encounterSet = false;
        private bool randomEncounter = true;
        private List<Image> iList = new List<Image>();
        private List<Point> pList = new List<Point>();
        private List<string> nList = new List<string>();
        private Point mBossLocation = new Point();
        private int mBossState = 0;
        public static bool BossBeaten = false;
        ////////////////////////////////////

        public Map() : this(new Point(0, 0)) { }
        public Map(Point startLocation)
        {
            this.StartLocation = startLocation;
            this.mLeft = true;
            this.randomEncounter = false; // <-- Set encounters
            Locked = false;

            Tiles = new Tile[70, 50];

            for (int y = 0; y < Tiles.GetLength(1); y++)
            {
                for (int x = 0; x < Tiles.GetLength(0); x++)
                {
                    if (x == 0 || x == Tiles.GetLength(0) - 1 || y == 0 || y == Tiles.GetLength(1) - 1) { Tiles[x, y] = 2; }
                    /*else if (y == 5 && x == 5) { Tiles[x, y] = 3; }
                    else if (y == 6 && x == 5) { Tiles[x, y] = 3; }
                    else if (y == 5 && x == 6) { Tiles[x, y] = 3; }
                    else if (y == 6 && x == 6) { Tiles[x, y] = 3; }*/
                    //Lake 1
                    else if (x >= 3 && x <= 16 && y >= 38 && y <= 46) { Tiles[x, y] = 3; }
                    else if (x >= 5 && x <= 18 && y >= 36 && y <= 44) { Tiles[x, y] = 3; }
                    else if (x >= 15 && x <= 18 && y >= 33 && y <= 35) { Tiles[x, y] = 3; }
                    //Lake 2
                    else if (x >= 60 && x <= 66 && y >= 3 && y <= 11) { Tiles[x, y] = 3; }
                    else if (x >= 57 && x <= 59 && y >= 5 && y <= 9) { Tiles[x, y] = 3; }
                    //Boss Circle
                    else if (x >= 60 && x <= 67 && y == 47) { Tiles[x, y] = 3; }
                    else if (x >= 60 && x <= 67 && y == 40) { Tiles[x, y] = 3; }
                    else if (x == 60 && y >= 40 && y <= 42) { Tiles[x, y] = 3; }
                    else if (x == 60 && y >= 44 && y <= 47) { Tiles[x, y] = 3; }
                    else if (x == 67 && y >= 40 && y <= 47) { Tiles[x, y] = 3; }
                    else { Tiles[x, y] = 1; }
                    //Sand
                    if (x >= 61 && x <= 66 && y >= 41 && y <= 46) { Tiles[x, y] = 4; }
                    //Rock
                    if (x == 4 && y == 45) { Tiles[x, y].Feature = FeatureType.Rock; }
                    if (x == 8 && y == 38) { Tiles[x, y].Feature = FeatureType.Rock; }
                    if (x == 17 && y == 43) { Tiles[x, y].Feature = FeatureType.Rock; }
                    //Tree
                    if (x >= 1 && x <= 15 && y >= 1 && y <= 3) { Tiles[x, y].Feature = FeatureType.Tree; }
                    if (x >= 1 && x <= 7 && y == 4) { Tiles[x, y].Feature = FeatureType.Tree; }
                    if (x >= 9 && x <= 15 && y == 4) { Tiles[x, y].Feature = FeatureType.Tree; }
                    if (x >= 2 && x <= 7 && y == 5) { Tiles[x, y].Feature = FeatureType.Tree; }
                    if (x >= 9 && x <= 14 && y == 5) { Tiles[x, y].Feature = FeatureType.Tree; }
                }
            }

            this.DoubleBuffered = true;
            this.Width = Tiles.GetLength(0) * TILE_WIDTH;
            this.Height = Tiles.GetLength(1) * TILE_HEIGHT;
            this.PlayerLocation = startLocation;
            this.BackColor = Color.FromArgb(255, 0, 0, 0);

            this.mGrass = Image.FromFile("Images/Tile/Grass.png");
            this.mStone = Image.FromFile("Images/Tile/Rocks.png");
            this.mRock = Image.FromFile("Images/Tile/Rock.png");
            this.mWater1 = Image.FromFile("Images/Tile/Water.png");
            this.mWater2 = Image.FromFile("Images/Tile/Water2.png");
            this.mWater = this.mWater1;
            this.mSand = Image.FromFile("Images/Tile/Sand.png");
            this.mTree1 = Image.FromFile("Images/Tile/Old Tree.png");
            this.mTree2 = Image.FromFile("Images/Tile/Vine Tree.png");
            this.mTree = this.mTree1;
            //this.mCharacter = Image.FromFile("Images/vivi.png");
            this.mShop = Image.FromFile("Images/Mart.png");
            this.mFinalBoss = Image.FromFile("Images/FinalBoss.png");

            Timer t = new Timer() { Interval = 500 };
            t.Tick += delegate
            {
                if (this.mWaterTwo) { this.mWaterTwo = false; this.mWater = this.mWater1; }
                else { this.mWaterTwo = true; this.mWater = this.mWater2; }
                //Tree
                if (this.mTreeTwo) { this.mTreeTwo = false; this.mTree = this.mTree1; }
                else { this.mTreeTwo = true; this.mTree = this.mTree2; }

                if (!Locked)
                {
                    switch (this.mBossState)
                    {
                        case 0:
                            {
                                this.mBossLocation = new Point(this.Tiles.GetLength(0) - 6 + StartLocation.X, this.Tiles.GetLength(1) - 6 + StartLocation.Y);
                                break;
                            }
                        case 1:
                            {
                                this.mBossLocation = new Point(this.Tiles.GetLength(0) - 7 + StartLocation.X, this.Tiles.GetLength(1) - 6 + StartLocation.Y);
                                break;
                            }
                        case 2:
                            {
                                this.mBossLocation = new Point(this.Tiles.GetLength(0) - 7 + StartLocation.X, this.Tiles.GetLength(1) - 7 + StartLocation.Y);
                                break;
                            }
                        case 3:
                            {
                                this.mBossLocation = new Point(this.Tiles.GetLength(0) - 6 + StartLocation.X, this.Tiles.GetLength(1) - 7 + StartLocation.Y);
                                break;
                            }
                    }
                    this.mBossState++;
                    if (this.mBossState == 4) { this.mBossState = 0; }
                    if (PlayerLocation.X + StartLocation.X == mBossLocation.X && PlayerLocation.Y + StartLocation.Y == mBossLocation.Y)
                    {
                        Encounter();
                    }
                }
                this.Invalidate();
            };
            t.Start();
        }

        public void UpdatePlayerLocation(Point p)
        {
            this.PlayerLocation = p;
            this.Invalidate();
        }

        public void MoveLeft()
        {
            if (!Locked)
            {
                if (!this.mLeft) { mCharacter.RotateFlip(RotateFlipType.RotateNoneFlipX); this.mLeft = true; }
                if (this.PlayerLocation.X - 1 >= 0)
                {
                    if (this.Tiles[this.PlayerLocation.X - 1, this.PlayerLocation.Y].Value != TileValue.Void &&
                        this.Tiles[this.PlayerLocation.X - 1, this.PlayerLocation.Y].Value != TileValue.Stone &&
                        this.Tiles[this.PlayerLocation.X - 1, this.PlayerLocation.Y].Value != TileValue.Water &&
                        this.Tiles[this.PlayerLocation.X - 1, this.PlayerLocation.Y].Feature != FeatureType.Tree &&
                        this.Tiles[this.PlayerLocation.X - 1, this.PlayerLocation.Y].Feature != FeatureType.Rock)
                    {
                        this.PlayerLocation = new Point(this.PlayerLocation.X - 1, this.PlayerLocation.Y);
                    }
                }
                this.Invalidate();
                this.Encounter();
            }
        }
        public void MoveRight()
        {
            if (!Locked)
            {
                if (this.mLeft) { mCharacter.RotateFlip(RotateFlipType.RotateNoneFlipX); this.mLeft = false; }
                if (this.PlayerLocation.X + 1 <= this.Tiles.GetLength(0) - 1)
                {
                    if (this.Tiles[this.PlayerLocation.X + 1, this.PlayerLocation.Y].Value != TileValue.Void &&
                        this.Tiles[this.PlayerLocation.X + 1, this.PlayerLocation.Y].Value != TileValue.Stone &&
                        this.Tiles[this.PlayerLocation.X + 1, this.PlayerLocation.Y].Value != TileValue.Water &&
                        this.Tiles[this.PlayerLocation.X + 1, this.PlayerLocation.Y].Feature != FeatureType.Tree &&
                        this.Tiles[this.PlayerLocation.X + 1, this.PlayerLocation.Y].Feature != FeatureType.Rock)
                    {
                        this.PlayerLocation = new Point(this.PlayerLocation.X + 1, this.PlayerLocation.Y);
                    }
                }
                this.Invalidate();
                this.Encounter();
            }
        }
        public void MoveUp()
        {
            if (!Locked)
            {
                if (this.PlayerLocation.Y - 1 >= 0)
                {
                    if (this.Tiles[this.PlayerLocation.X, this.PlayerLocation.Y - 1].Value != TileValue.Void &&
                        this.Tiles[this.PlayerLocation.X, this.PlayerLocation.Y - 1].Value != TileValue.Stone &&
                        this.Tiles[this.PlayerLocation.X, this.PlayerLocation.Y - 1].Value != TileValue.Water &&
                        this.Tiles[this.PlayerLocation.X, this.PlayerLocation.Y - 1].Feature != FeatureType.Tree &&
                        this.Tiles[this.PlayerLocation.X, this.PlayerLocation.Y - 1].Feature != FeatureType.Rock)
                    {
                        this.PlayerLocation = new Point(this.PlayerLocation.X, this.PlayerLocation.Y - 1);
                    }
                }
                this.Invalidate();
                this.Encounter();
            }
        }
        public void MoveDown()
        {
            if (!Locked)
            {
                if (this.PlayerLocation.Y + 1 >= 0)
                {
                    if (this.Tiles[this.PlayerLocation.X, this.PlayerLocation.Y + 1].Value != TileValue.Void &&
                        this.Tiles[this.PlayerLocation.X, this.PlayerLocation.Y + 1].Value != TileValue.Stone &&
                        this.Tiles[this.PlayerLocation.X, this.PlayerLocation.Y + 1].Value != TileValue.Water &&
                        this.Tiles[this.PlayerLocation.X, this.PlayerLocation.Y + 1].Feature != FeatureType.Tree &&
                        this.Tiles[this.PlayerLocation.X, this.PlayerLocation.Y + 1].Feature != FeatureType.Rock)
                    {
                        this.PlayerLocation = new Point(this.PlayerLocation.X, this.PlayerLocation.Y + 1);
                    }
                }
                this.Invalidate();
                this.Encounter();
            }
        }

        public void AddMonster()
        {
            Random random = new Random();
            string name = string.Empty;
            Image Mon = null;
            while (true)
            {
                int x = random.Next(0, this.Tiles.GetLength(0));
                int y = random.Next(0, this.Tiles.GetLength(1));
                if (this.Tiles[x, y].Value != TileValue.Grass || this.pList.Contains(new Point(x + StartLocation.X, y + StartLocation.Y)))
                {
                    continue;
                }
                else
                {
                    this.pList.Add(new Point(x + StartLocation.X, y + StartLocation.Y));
                    break;
                }
            }
            switch (random.Next(0, 9))
            {
                case 0:
                    {
                        name = "Billy";
                        Mon = Image.FromFile("Images/Monsters/Billy.png");
                        break;
                    }
                case 1:
                    {
                        name = "Sven";
                        Mon = Image.FromFile("Images/Monsters/Sven.png");
                        break;
                    }
                case 2:
                    {
                        name = "Derp";
                        Mon = Image.FromFile("Images/Monsters/Derp.png");
                        break;
                    }
                case 3:
                    {
                        name = "Ghostsicle";
                        Mon = Image.FromFile("Images/Monsters/Ghostsicle.png");
                        break;
                    }
                case 4:
                    {
                        name = "HummingBird";
                        Mon = Image.FromFile("Images/Monsters/Hummingbird.png");
                        break;
                    }
                case 5:
                    {
                        name = "Hydra";
                        Mon = Image.FromFile("Images/Monsters/Hydra.png");
                        break;
                    }
                case 6:
                    {
                        name = "Mammy";
                        Mon = Image.FromFile("Images/Monsters/Mammy.png");
                        break;
                    }
                case 7:
                    {
                        name = "Reaper";
                        Mon = Image.FromFile("Images/Monsters/Reaper.png");
                        break;
                    }
                case 8:
                    {
                        name = "YingYang";
                        Mon = Image.FromFile("Images/Monsters/Yinyang.png");
                        break;
                    }
            }
            this.iList.Add(Mon);
            this.nList.Add(name);
        }

        public void Encounter()
        {
            if (this.randomEncounter)
            {
                Random random = new Random();
                int value = random.Next(0, 100);
                if (value == 0)
                {
                    Locked = true;
                    Battle battleForm = new Battle(null, null, null);
                    battleForm.Show();
                }
            }
            else
            {
                for (int i = 0; i < pList.Count; i++)
                {
                    if (PlayerLocation.X + StartLocation.X == pList[i].X && PlayerLocation.Y + StartLocation.Y == pList[i].Y)
                    {
                        Locked = true;
                        Battle battleForm = new Battle(nList[i], iList[i], null);
                        battleForm.Show();
                        this.iList.RemoveAt(i);
                        this.pList.RemoveAt(i);
                        this.nList.RemoveAt(i);
                        AddMonster();
                        this.Invalidate();
                        break;
                    }
                }
              
                if (PlayerLocation.X + StartLocation.X == ShopLocation.X && PlayerLocation.Y + StartLocation.Y == ShopLocation.Y)
                {
                    Locked = true;
                    Shop shopForm = new Shop();
                    shopForm.Show();
                }

                if (PlayerLocation.X + StartLocation.X == mBossLocation.X && PlayerLocation.Y + StartLocation.Y == mBossLocation.Y && !BossBeaten)
                {
                    Locked = true;
                    Battle battleForm = new Battle("Big Bad Boss", this.mFinalBoss, new MaxMana.Tools.Weapons.Excalibur());
                    battleForm.Show();
                    AddMonster();
                    this.Invalidate();
                }
            }
        }
        
        public void SetEncounter(PaintEventArgs e)
        {
            if (!this.encounterSet)
            {
                this.encounterSet = true;
                Random random = new Random();
                for (int i = 0; i < MONSTER_COUNT; i++)
                {
                    int value = random.Next(0, 9);
                    Image Mon = null;
                    string name = string.Empty;
                    switch (value)
                    {
                        case 0:
                            {
                                name = "Billy";
                                Mon = Image.FromFile("Images/Monsters/Billy.png");
                                break;
                            }
                        case 1:
                            {
                                name = "Sven";
                                Mon = Image.FromFile("Images/Monsters/Sven.png");
                                break;
                            }
                        case 2:
                            {
                                name = "Derp";
                                Mon = Image.FromFile("Images/Monsters/Derp.png");
                                break;
                            }
                        case 3:
                            {
                                name = "Ghostsicle";
                                Mon = Image.FromFile("Images/Monsters/Ghostsicle.png");
                                break;
                            }
                        case 4:
                            {
                                name = "HummingBird";
                                Mon = Image.FromFile("Images/Monsters/Hummingbird.png");
                                break;
                            }
                        case 5:
                            {
                                name = "Hydra";
                                Mon = Image.FromFile("Images/Monsters/Hydra.png");
                                break;
                            }
                        case 6:
                            {
                                name = "Mammy";
                                Mon = Image.FromFile("Images/Monsters/Mammy.png");
                                break;
                            }
                        case 7:
                            {
                                name = "Reaper";
                                Mon = Image.FromFile("Images/Monsters/Reaper.png");
                                break;
                            }
                        case 8:
                            {
                                name = "YingYang";
                                Mon = Image.FromFile("Images/Monsters/Yinyang.png");
                                break;
                            }
                    }
                    int x = random.Next(0, this.Tiles.GetLength(0));
                    int y = random.Next(0, this.Tiles.GetLength(1));
                    if (this.Tiles[x, y].Value != TileValue.Grass || this.Tiles[x, y].Feature != FeatureType.Void || this.pList.Contains(new Point(x + StartLocation.X, y + StartLocation.Y)))
                    {
                        i--;
                        continue;
                    }
                    iList.Add(Mon);
                    pList.Add(new Point(x + StartLocation.X, y + StartLocation.Y));
                    nList.Add(name);
                }

                ShopLocation = new Point(8 + StartLocation.X, 4 + StartLocation.Y);
                this.mBossLocation = new Point(this.Tiles.GetLength(0) - 2 + StartLocation.X, this.Tiles.GetLength(1) - 2 + StartLocation.Y);
            }

            for (int i = 0; i < iList.Count; i++)
            {
                e.Graphics.DrawImage(iList[i], pList[i].X * TILE_WIDTH - (PlayerLocation.X * TILE_WIDTH), pList[i].Y * TILE_HEIGHT - (PlayerLocation.Y * TILE_HEIGHT), TILE_WIDTH, TILE_HEIGHT);
            }
            if (World.character.Level > 0 && !BossBeaten)
            {
                e.Graphics.DrawImage(mFinalBoss, mBossLocation.X * TILE_WIDTH - (PlayerLocation.X * TILE_WIDTH), mBossLocation.Y * TILE_HEIGHT - (PlayerLocation.Y * TILE_HEIGHT), TILE_WIDTH, TILE_HEIGHT);
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            for (int y = 0; y < Tiles.GetLength(1); y++)
            {
                for (int x = 0; x < Tiles.GetLength(0); x++)
                {
                    switch (Tiles[x, y].Value)
                    {
                        case TileValue.Void:
                            {
                                SolidBrush brush = new SolidBrush(Color.FromArgb(255, 0, 0, 0));
                                break;
                            }
                        case TileValue.Grass:
                            {
                                e.Graphics.DrawImage(mGrass, x * TILE_WIDTH - ((PlayerLocation.X - StartLocation.X) * TILE_WIDTH), y * TILE_HEIGHT - ((PlayerLocation.Y - StartLocation.Y) * TILE_HEIGHT), TILE_WIDTH, TILE_HEIGHT);
                                break;
                            }
                        case TileValue.Stone:
                            {
                                e.Graphics.DrawImage(mStone, x * TILE_WIDTH - ((PlayerLocation.X - StartLocation.X) * TILE_WIDTH), y * TILE_HEIGHT - ((PlayerLocation.Y - StartLocation.Y) * TILE_HEIGHT), TILE_WIDTH, TILE_HEIGHT);
                                break;
                            }
                        case TileValue.Water:
                            {
                                e.Graphics.DrawImage(mWater, x * TILE_WIDTH - ((PlayerLocation.X - StartLocation.X) * TILE_WIDTH), y * TILE_HEIGHT - ((PlayerLocation.Y - StartLocation.Y) * TILE_HEIGHT), TILE_WIDTH, TILE_HEIGHT);
                                break;
                            }
                        case TileValue.Sand:
                            {
                                e.Graphics.DrawImage(mSand, x * TILE_WIDTH - ((PlayerLocation.X - StartLocation.X) * TILE_WIDTH), y * TILE_HEIGHT - ((PlayerLocation.Y - StartLocation.Y) * TILE_HEIGHT), TILE_WIDTH, TILE_HEIGHT);
                                break;
                            }
                    }
                    if (Tiles[x, y].Feature == FeatureType.Tree)
                    { e.Graphics.DrawImage(mTree, x * TILE_WIDTH - ((PlayerLocation.X - StartLocation.X) * TILE_WIDTH), y * TILE_HEIGHT - ((PlayerLocation.Y - StartLocation.Y) * TILE_HEIGHT), TILE_WIDTH, TILE_HEIGHT); }
                    if (Tiles[x, y].Feature == FeatureType.Rock)
                    { e.Graphics.DrawImage(mRock, x * TILE_WIDTH - ((PlayerLocation.X - StartLocation.X) * TILE_WIDTH), y * TILE_HEIGHT - ((PlayerLocation.Y - StartLocation.Y) * TILE_HEIGHT), TILE_WIDTH, TILE_HEIGHT); }
                }
            }
            if (!this.encounterSet)
            {
                switch (World.character.Class)
                {
                    case CharacterClass.Warrior:
                        {
                            this.mCharacter = Image.FromFile("Images/Warrior.png");
                            break;
                        }
                    case CharacterClass.Mage:
                        {
                            this.mCharacter = Image.FromFile("Images/vivi.png");
                            break;
                        }
                    case CharacterClass.Rogue:
                        {
                            this.mCharacter = Image.FromFile("Images/Ninja.png");
                            break;
                        }
                }
            }
            e.Graphics.DrawImage(mCharacter, this.StartLocation.X * TILE_WIDTH, this.StartLocation.Y * TILE_HEIGHT, TILE_WIDTH, TILE_HEIGHT);
            e.Graphics.DrawImage(mShop, ShopLocation.X * TILE_WIDTH - (PlayerLocation.X * TILE_WIDTH), ShopLocation.Y * TILE_HEIGHT - (PlayerLocation.Y * TILE_HEIGHT), TILE_WIDTH, TILE_HEIGHT);
            SetEncounter(e);
        }
    }
}
