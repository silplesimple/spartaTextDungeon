﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace spartaTextDungeon
{
    internal class MonsterAvatar
    {
        private static List<Monster> randdomMonster = new List<Monster>();
        static void Main(string[] args)
        {
            int inputNumber;
           //inputNumber = int.Parse(Console.ReadLine());
            Battle();
        }
        
        private static void Battle()
        {
            Console.Clear();
            Player player = new Player("Chad", 1, 100);
            // 몬스터 객체들을 저장하는 동적 배열
            // List<T> 클래스는 제네릭 타입으로, 여기서 `T`는 저장되는 요소의 타입을 나타냄
            List<Monster> monsters = new List<Monster>
            {
                new Monster(0,"미니언", 2, 15),
                new Monster(1,"대포미니언", 5, 25),
                new Monster(2,"공허충", 3, 10) 
            };
            
           
            
            // 전투 시작
            Console.WriteLine("Battle!!\n");            
            DisplayStatus(player);
            
            // 플레이어 공격
            PlayerAttack(player, monsters);
            
            List<Monster> saveMonsters=RandomMonster(monsters);//몬스터 정보를 1~4마리를 랜덤으로 저장
            foreach(Monster monster in saveMonsters)
            {
                Console.WriteLine($"{monster}");
            }
            
        }

        private void EnemyPhase()
        {
            Console.Clear();
            

        }
        
        
        
        private static void DisplayStatus(Player player)
        {                           
                             
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine($"[내정보]\n{player}");
        }
        private static List<Monster> RandomMonster(List<Monster> monsters)
        {
            List<Monster> saveMonster= new List<Monster>();
            Random random = new Random();
            int rndcnt = random.Next(1, 5);
            for(int i=0;i<rndcnt;i++)
            {
                int rndIndex = random.Next(0, monsters.Count);
                foreach (Monster monster in monsters)
                {
                    if ( rndIndex == monster.CheckIndex)
                    {                        
                        saveMonster.Add(monster);
                        break;
                    }
                }
                                
            }
            return saveMonster;
        }

        private static void PlayerAttack(Player player, List<Monster> monsters)
        {

        }


        class Character//캐릭터 범용 클래스
        {
            public string Name { get; protected set; }//캐릭터 이름
            public int Level { get; protected set; }
            public int MaxHP { get; protected set; }
            public int HP { get; protected set; }       // protected set: 해당 속성을 클래스 내부와 파생 클래스에서만 수정할 수 있도록 해준다.

            public bool IsDead => HP <= 0;      // IsDead: 현재 체력이 0 이하이면 참을 반환

            public Character(string name, int level, int maxHP)
            {
                Name = name;
                Level = level;
                MaxHP = maxHP;
                HP = MaxHP;
            }

            public virtual void TakeDamage(int damage)      // TakeDamage(int damage): 캐릭터가 피해를 받을 때 호출되는 메서드
            {                                               // 현재 체력에서 피해만큼 감소시키고 체력이 음수가 되지 않도록 조정
                HP -= damage;
                if (HP < 0)
                    HP = 0;
            }

            public override string ToString()
            {
                return $"Lv.{Level} {Name} (전사)\nHP {HP}/{MaxHP}";    // 캐릭터의 상태를 문자열로 반환
            }
        }

        class Player : Character
        {
            public int AttackPower { get; private set; }

            public Player(string name, int level, int maxHP) : base(name, level, maxHP)
            {
                AttackPower = 10;   // 플레이어의 기본 공격력 설정
            }
        }

        class Monster : Character       // 클래스를 상속받아 플레이어 특화 속성을 추가
        {
            public int AttackPower { get; private set; }

            public int CheckIndex { get; set; }
            public Monster(int checkIndex, string name, int level, int maxHP) : base(name, level, maxHP)
            {
                CheckIndex = checkIndex;
                AttackPower = 5 * level;    // 몬스터의 공격력은 레벨에 비례하도록 설정
            }
            

            public override string ToString()
            {
                return $"Lv.{Level} {Name} HP {HP}";    // 몬스터의 상태를 문자열로 반환
            }
        }
    }
}
