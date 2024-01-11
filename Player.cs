using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace spartaTextDungeon
{
    internal class DungeonGame
    {
        static void Main()
        {
            Player player = new Player("Chad", 1, 100, 10, 5);
            // 몬스터 객체들을 저장하는 동적 배열
            // List<T> 클래스는 제네릭 타입으로, 여기서 `T`는 저장되는 요소의 타입을 나타냄
            List<Monster> monsters = new List<Monster>
            {
                new Monster("미니언", 2, 5, 6, 0),
                new Monster("대포미니언", 5, 15, 15, 0),
                new Monster("공허충", 3, 10, 10, 0)
            };

            // 전투 시작
            Console.WriteLine("Battle!!\n");
            DisplayStatus(player, monsters);

            // 플레이어 공격
            PlayerAttack(player, monsters);

            // 몬스터 공격
            //EnemyPhase(player, monsters);

            // 전투 결과
            DisplayResult(player, monsters);
        }



        private static void DisplayStatus(Player player, List<Monster> monsters)
        {
            foreach (var monster in monsters)
            {
                Console.WriteLine($"{monster}");
            }
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine($"[내정보]\n{player}");
        }

        private static void PlayerAttack(Player player, List<Monster> currentMonsters)
        {
            Console.WriteLine("** Battle!! **");
            int targetIndex = GetMonsterIndex(currentMonsters);
            if (targetIndex < 0 || targetIndex >= currentMonsters.Count)
            {
                Console.WriteLine("잘못된 입력입니다.");
                return;
            }

            Monster targetMonster = currentMonsters[targetIndex];

            if (targetMonster.IsDead)
            {
                Console.WriteLine("잘못된 입력입니다.");
                return;
            }

            // 공격 로직
            int damage = CalculateDamage(player.AttackPower);
            targetMonster.TakeDamage(damage);

            Console.WriteLine($"{player.Name}의 공격!\n{targetMonster}을(를) 맞췄습니다. [데미지 : {damage}]");

            // 몬스터가 죽었는지 확인
            if (targetMonster.IsDead)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"{targetMonster.Name}이(가) 죽었습니다.");
                Console.ResetColor();
            }
        }

        //private static EnemyPhase(Player player, List<Monster> currentMonsters)
        //{
        //    Console.WriteLine("\nBattle!! - Enemy Phase");

        //    foreach (var monster in currentMonsters)
        //    {
        //        if (!monster.IsDead)
        //        {
        //            int damage = CalculateDamage(monster.AttackPower);
        //            player.TakeDamage(damage);

        //            Console.WriteLine($"{monster.Name}의 공격!\n{player}을(를) 맞췄습니다. [데미지 : {damage}]");
        //            DisplayStatus(player, currentMonsters);

        //            if (player.IsDead)
        //            {
        //                Console.ForegroundColor = ConsoleColor.DarkGray;
        //                Console.WriteLine("You Lose\n");
        //                Console.ResetColor();
        //                return;
        //            }
        //        }
        //    }
        //}

        static void DisplayResult(Player player, List<Monster> monsters)
        {
            Console.WriteLine("\n3. 전투 결과\n");
            DisplayStatus(player, monsters);

            if (monsters.TrueForAll(monster => monster.IsDead))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nVictory\n던전에서 몬스터들을 잡았습니다.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"\nYou Lose\n{player}");
            }

            Console.ResetColor();
        }

        static int GetMonsterIndex(List<Monster> monsters)
        {
            Console.WriteLine("\n대상을 선택해주세요.\n");

            for (int i = 0; i < monsters.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {monsters[i]}");
            }

            Console.WriteLine("");
            Console.WriteLine("0. 취소");
            Console.Write(">>");
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > monsters.Count)
            {
                Console.WriteLine("잘못된 입력입니다.");
            }

            return choice - 1;
        }

        static int CalculateDamage(int attackPower)
        {
            double randomFactor = new Random().NextDouble() * 0.2 + 0.9; // 0.9 ~ 1.1 사이의 랜덤값
            int damage = (int)Math.Ceiling(attackPower * randomFactor);
            return damage;
        }
    }

    public class Character
    {
        public string Name { get; protected set; }
        public int Level { get; protected set; }
        public int MaxHP { get; protected set; }
        public int HP { get; protected set; }       // protected set: 해당 속성을 클래스 내부와 파생 클래스에서만 수정할 수 있도록 해준다.
        public int Atk { get; protected set; }
        public int Def { get; protected set; }

        public bool IsDead => HP <= 0;      // IsDead: 현재 체력이 0 이하이면 참을 반환

        public Character(string name, int level, int maxHP, int atk, int def)
        {
            Name = name;
            Level = level;
            MaxHP = maxHP;
            HP = MaxHP;
            Atk = atk;
            Def = def;
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

        public Player(string name, int level, int maxHP, int atk, int def) : base(name, level, maxHP, atk, def) 
        {
            AttackPower = 10;   // 플레이어의 기본 공격력 설정
        }
    }

    class Monster : Character       // 클래스를 상속받아 플레이어 특화 속성을 추가
    {
        public int AttackPower { get; private set; }

        public Monster(string name, int level, int maxHP, int atk, int def) : base(name, level, maxHP, atk, def)
        {
            AttackPower = 5 * level;    // 몬스터의 공격력은 레벨에 비례하도록 설정
        }

        public override string ToString()
        {
            return $"Lv.{Level} {Name} HP {HP}";    // 몬스터의 상태를 문자열로 반환
        }
    }
}