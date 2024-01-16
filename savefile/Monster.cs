using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace spartaTextDungeon.savefile
{
    internal class Monster
    {
        public string Name { get; }
        public int MaxHP { get; }
        public int HP { get;  set; }
        public int Attack { get; }        
        public int Level { get; set; }
        public Monster(string name, int maxHP, int hp, int attack, int checkIndex, int level)
        {
            Name = name;
            MaxHP = maxHP;
            HP = maxHP;
            Attack = attack;            
            Level = level;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
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
            return $"Lv{Level} {Name}  {status}";
        }
    }
}
