using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spartaTextDungeon.finalFile
{
    internal class Player
    {
        public string Name { get; }
        public string Class { get; }
        public int MaxHP { get; }
        public int HP { get; private set; }
        public int Attack { get; }
        public int Def { get; }
        public int Gold { get; }
        public int Level { get; }

        public Player(string name, string playerClass, int maxHP, int hp, int attack, int def, int gold, int level)
        {
            Name = name;
            Class = playerClass;
            MaxHP = maxHP;
            HP = hp;
            Attack = attack;
            Def = def;
            Gold = gold;
            Level = level;
        }

        public void TakeDamage(int damage)
        {
            HP -= damage;
            if (HP < 0)
            {
                HP = 0;
            }
        }

        public bool IsDead()
        {
            return HP <= 0;
        }

        public override string ToString()
        {
            return $"Lv.1 {Name} ({Class})\nHP {HP}/{MaxHP}";
        }

    }
}
