using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace spartaTextDungeon.finalFile
{
    internal class Monster
    {
        public string Name { get; }
        public int MaxHP { get; }
        public int HP { get; private set; }
        public int Attack { get; }
        public int CheckIndex { get; set; }
        public Monster(string name, int maxHP, int hp, int attack, int checkIndex)
        {
            Name = name;
            MaxHP = maxHP;
            HP = maxHP;
            Attack = attack;
            CheckIndex = checkIndex;
        }

        public bool IsDead()
        {
            return HP <= 0;
        }

        public void TakeDamage(int damage)
        {
            HP -= damage;
            if (HP < 0)
            {
                HP = 0;
            }
        }

        public override string ToString()
        {
            string status = IsDead() ? "Dead" : $"HP {HP}";
            return $"{Name}  {status}";
        }
    }
}
