using System.Collections.Generic;
using System.Windows.Forms;

namespace MaxMana.Tools
{
    public class Character
    {
        // Constants
        private const CharacterClass DEFAULT_CLASS = CharacterClass.Warrior;
        private const CharacterRace DEFAULT_RACE = CharacterRace.Human;
        private const string DEFAULT_NAME = "DEFAULT";
        private const uint BASE_HEALTH = 10;
        private const uint BASE_MANA = 0;
        private const uint BASE_ATTACK = 4;
        private const uint BASE_DEFENCE = 2;
        private const uint BASE_MAGIC_ATTACK = 0;
        private const uint BASE_MAGIC_DEFENCE = 0;

        // Properties
        public CharacterClass Class { get; set; }
        public CharacterRace Race { get; set; }
        public string Name { get; set; }
        public uint Health { get; set; }
        public uint MaxHealth { get; set; }
        public uint Mana { get; set; }
        public uint MaxMana { get; set; }
        public uint Attack { get; set; }
        public uint Defence { get; set; }
        public uint MagicAttack { get; set; }
        public uint MagicDefence { get; set; }
        public uint Exp { get; set; }
        public uint Level { get; set; }
        public uint Gold { get; set; }
        public List<Item> Items { get; set; }
        public List<Spell> Spells { get; set; }
        public Item CurrentArmour { get; set; }
        public Item CurrentWeapon { get; set; }

        // Constructors
        public Character() : this(DEFAULT_CLASS, DEFAULT_RACE, DEFAULT_NAME) { }
        public Character(CharacterClass characterClass, CharacterRace characterRace, string name)
        {
            this.Class = characterClass;
            this.Race = characterRace;
            this.Name = name;

            this.Health = BASE_HEALTH;
            this.Mana = BASE_MANA;
            this.Attack = BASE_ATTACK;
            this.Defence = BASE_DEFENCE;
            this.MagicAttack = BASE_MAGIC_ATTACK;
            this.MagicDefence = BASE_MAGIC_DEFENCE;
            this.Exp = 0;
            this.Level = 1;
            this.Gold = 0;
            this.Items = new List<Item>();
            this.Spells = new List<Spell>();

               ////Items added////
            Items.Add(new MaxMana.Tools.Consumable.HealthPotion());
            Items.Add(new Weapons.Excalibur());

            // Class Bonuses
            switch (characterClass)
            {
                case CharacterClass.Mage:
                    {
                        this.Health += 2;
                        this.Mana += 8;
                        this.Attack += 2;
                        this.Defence += 2;
                        this.MagicAttack += 4;
                        this.MagicDefence += 4;
                        break;
                    }
                case CharacterClass.Rogue:
                    {
                        this.Health += 4;
                        this.Mana += 4;
                        this.Attack += 2;
                        this.Defence += 2;
                        this.MagicAttack += 2;
                        this.MagicDefence += 2;
                        break;
                    }
                case CharacterClass.Warrior:
                    {
                        this.Health += 8;
                        this.Mana += 2;
                        this.Attack += 4;
                        this.Defence += 4;
                        this.MagicAttack += 2;
                        this.MagicDefence += 3;
                        break;
                    }
            }

            // Race Bonuses
            switch (characterRace)
            {
                case CharacterRace.Dwarf:
                    {
                        this.Health += 4;
                        this.Defence += 4;
                        this.MagicDefence += 4;
                        this.Spells.Add(new Abilities.Headbutt());
                        //double attack

                        break;
                    }
                case CharacterRace.Elf:
                    {
                        this.Mana += 4;
                        this.MagicAttack += 4;
                        this.MagicDefence += 4;
                        this.Spells.Add(new Abilities.Snipe());
                        //add critical chance
                        break;
                    }
                case CharacterRace.Human:
                    {
                        this.Health += 2;
                        this.Mana += 2;
                        this.Attack += 2;
                        this.Defence += 2;
                        this.MagicAttack += 2;
                        this.MagicDefence += 2;
                        this.Spells.Add(new Abilities.CoinToss());
                        //
                        break;
                    }
            }

            this.MaxHealth = this.Health;
            this.MaxMana = this.Mana;
        }
        public void Levelup()
        {
            if ((Exp >= 100 && this.Level == 1) 
                || (Exp >= 250 && this.Level == 2) 
                || (Exp >= 500 && this.Level == 3) 
                || (Exp >= 1200 && this.Level == 4)
                || (Exp >= 2000 && this.Level == 5))
            {
                this.Health = this.MaxHealth;
                this.Mana = this.MaxMana;
                this.Level++;
                switch (this.Class)
                {
                    case CharacterClass.Mage:
                        {
                            this.Health += 4;
                            this.Mana += 4;
                            this.Attack += 1;
                            this.Defence += 1;
                            this.MagicAttack += 2;
                            this.MagicDefence += 2;
                            switch (this.Level)
                            {
                                case 2:
                                    {
                                        Spells.Add(new MaxMana.Tools.Abilities.Fira());
                                        MessageBox.Show("");
                                        break;
                                    }
                                case 3: { Spells.Add(new Abilities.Fatality()); break; }
                            }
                            break;
                        }
                    case CharacterClass.Rogue:
                        {
                            this.Health += 6;
                            this.Mana += 2;
                            this.Attack += 1;
                            this.Defence += 1;
                            this.MagicAttack += 1;
                            this.MagicDefence += 1;
                            switch (this.Level)
                            {
                                case 2: { Spells.Add(new MaxMana.Tools.Abilities.QuickAttack()); break; }
                                case 3: { Spells.Add(new MaxMana.Tools.Abilities.Steal()); break; }
                            }
                            break;
                        }
                    case CharacterClass.Warrior:
                        {
                            this.Health += 8;
                            this.Mana += 2;
                            this.Attack += 2;
                            this.Defence += 2;
                            this.MagicAttack += 1;
                            this.MagicDefence += 1;
                            switch (this.Level)
                            {
                                case 2: { Spells.Add(new MaxMana.Tools.Abilities.Beserk()); break; }
                                case 3: { Spells.Add(new MaxMana.Tools.Abilities.WhirlWind()); break; }
                            }
                            break;
                        }
                }
                this.MaxHealth = this.Health;
                this.MaxMana = this.Mana;
                MessageBox.Show("Well done, but shame you still useless");
            }
        }
    }
}
