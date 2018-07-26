using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaxMana.Tools
{
    public class Monster
    {
        private const string DEFAULT_NAME = "DEFAULT";

        public string Name { get; set; }
        public uint Health { get; set; }
        public uint MaxHealth { get; set; }
        public uint Mana { get; set; }
        public uint Attack { get; set; }
        public uint Defence { get; set; }
        public uint Magic_Attack { get; set; }
        public uint Magic_Defence { get; set; }

        public Monster() : this(DEFAULT_NAME, 0, 0, 0, 0, 0, 0) { }
        public Monster(string name, uint health, uint mana, uint attack, uint defence, uint magicAttack, uint magicDefence)
        {
            this.Name = name;
            this.Health = health;
            this.MaxHealth = health;
            this.Mana = mana;
            this.Attack = attack;
            this.Defence = defence;
            this.Magic_Attack = magicAttack;
            this.Magic_Defence = magicDefence;
        }
    }
}
