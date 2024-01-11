using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace spartaTextDungeon
{
    internal class MonsterAvatar
    {
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
                new Monster(2,"공허충", 3, 10),              
            };

            // 전투 시작
            Console.WriteLine("Battle!!\n");
            DisplayStatus(player, monsters);

            // 플레이어 공격
            PlayerAttack(player, monsters);

        }
        private static void DisplayStatus(Player player, List<Monster> monsters)
        {   
            Random random =new Random();
            int createMonster = random.Next(1,5);//몬스터 마리수
            bool firstMonsterCheck = false;
            foreach (Monster monster in monsters)
            {
                if (!firstMonsterCheck)
                    FirstMonster(monster, ref createMonster, ref firstMonsterCheck);
                else
                {
                    RandomMonster(monster, ref createMonster);
                }

                if(createMonster<=0)
                {
                    break;
                }
                
            }
            
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine($"[내정보]\n{player}");
        }
        private static void FirstMonster(Monster monster,ref int createMonster,ref bool firstMonsterCheck)
        {
            Random random = new Random();
            if (createMonster > 0)
            {
                int randomIndex = random.Next(0,3);
                if (randomIndex <= monster.CheckNumber)
                {
                    Console.WriteLine($"{monster}");
                    createMonster--;
                    firstMonsterCheck = true;
                }
            }
        }
        private static void RandomMonster(Monster monster,ref int createMonster)
        {            
            Random random = new Random();            
            for(int i=0;i<3;i++)
            {
                if (createMonster > 0)
                {
                    int randomIndex = random.Next(0,3);
                    if (randomIndex == monster.CheckNumber)
                    {
                        Console.WriteLine($"{monster}");
                        createMonster--;
                    }
                }

            }          

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

            public int CheckNumber { get; set; }
            public Monster(int checkNumber, string name, int level, int maxHP) : base(name, level, maxHP)
            {
                CheckNumber = checkNumber;
                AttackPower = 5 * level;    // 몬스터의 공격력은 레벨에 비례하도록 설정
            }
            

            public override string ToString()
            {
                return $"Lv.{Level} {Name} HP {HP}";    // 몬스터의 상태를 문자열로 반환
            }
        }
    }
}
