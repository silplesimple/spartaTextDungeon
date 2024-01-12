﻿using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

// 모든 코드을 여기에 합쳐 수정!
namespace spartaTextDungeon
{
    internal class TextDungeonGame
    {
        static void Main()
        {
            PrintStartLogo();
            StartMenu();
            StateSelectMenu();
            PlayerState newPlayerstate = new PlayerState();
            newPlayerstate.PrintState();
        }

        static void BattleStart()
        {
            Player player = new Player("Chad", "전사", 100, 100, 10);
            List<Monster> Monsters = new List<Monster>
            { 
            new Monster("Lv.2 미니언", 15, 5, 6),
            new Monster("Lv.5 대포미니언", 25, 8, 10),
            new Monster("LV.3 공허충", 10, 3, 8)
        };
            Random random = new Random();
            List<Monster> shuffledMonsters = Monsters.OrderBy(x => random.Next()).ToList(); // LINQ 메서드를 사용하여 목록의 몬스터 순서를 섞음
                                                                                            // 정렬을 위한 키로 사용되어 무작위 순서를 보장
            int numMonsters = random.Next(1, 4);                                            // Monsters.OrderBy(random.Next())
            Monster[] monsters = shuffledMonsters.Take(numMonsters).ToArray();

            do
            {
                Console.Clear();

                DisplayInfo(player, monsters);
                Console.WriteLine("\n1. 공격");
                Console.WriteLine("0. 취소");

                int choice = GetUserInput(1);

                if (choice == 0)
                {
                    Console.WriteLine("취소되었습니다.");
                }
                else
                {
                    Console.Clear();
                    Attack(player, monsters);
                }

            } while (true);
        }

        static void DisplayInfo(Player player, Monster[] monsters)
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine($"\n[내정보]");
            Console.WriteLine($"{player}");

            Console.WriteLine("");
            foreach (var monster in monsters)
            {
                Console.WriteLine($"{monster}");
            }
        }

        static void Attack(Player player, Monster[] monsters)
        {
            DisplayInfo(player, monsters);

            for (int i = 0; i < monsters.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {monsters[i]}");
            }

            Console.WriteLine("0. 취소");

            int choice = GetUserInput(monsters.Length);

            if (choice == 0)
            {
                Console.WriteLine("취소되었습니다.");
            }
            else
            {
                Attack(player, monsters[choice - 1]);
                DisplayInfo(player, monsters);

                if (!monsters[choice - 1].IsDead())
                {
                    DisplayInfo(player, monsters);
                }
            }

            Console.WriteLine("\n게임 종료");
            Console.ReadKey();
        }

        static void Attack(Player player, Monster monster)
        {
            if (monster.IsDead())
            {
                Console.WriteLine("잘못된 입력입니다. 이미 죽은 몬스터를 공격할 수 없습니다.");
            }
            else
            {
                Console.WriteLine($"몬스터 {monster.Name}을(를) 공격합니다.");
                int damage = CalculateDamage(player.Attack);
                monster.TakeDamage(damage);
                Console.WriteLine($"몬스터에게 {damage}의 데미지를 입혔습니다.");

                if (monster.IsDead())
                {
                    Console.WriteLine($"몬스터 {monster.Name}을(를) 처치했습니다.");
                }
            }
        }

        static int CalculateDamage(int baseAttack)
        {
            Random random = new Random();
            double error = Math.Ceiling(baseAttack * 0.1);
            int randomValue = random.Next(-(int)error, (int)error + 1);
            return baseAttack + randomValue;
        }

        static int GetUserInput(int maxChoice)
        {
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > maxChoice)
            {
                Console.WriteLine("올바르지 않은 입력입니다. 다시 입력해주세요.");
            }
            return choice;
        }

        public class Player
        {
            public string Name { get; }
            public string Class { get; }
            public int MaxHP { get; }
            public int HP { get; private set; }
            public int Attack { get; }

            public Player(string name, string playerClass, int maxHP, int hp, int attack)
            {
                Name = name;
                Class = playerClass;
                MaxHP = maxHP;
                HP = hp;
                Attack = 10;
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

        public class Monster
        {
            public string Name { get; }
            public int MaxHP { get; }
            public int HP { get; private set; }
            public int Attack { get; }

            public Monster(string name, int maxHP, int hp, int attack)
            {
                Name = name;
                MaxHP = maxHP;
                HP = maxHP;
                Attack = attack;
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

        public class PlayerState
        {
            int Level = 01;
            public string Name = "none";
            int Atk = 10;
            int Def = 5;
            int HP = 100;
            int Gold = 1500;

            public void PrintState()
            {
                Console.WriteLine("상태보기");
                Console.WriteLine("캐릭터의 정보가 표시됩니다.");
                Console.WriteLine();
                Console.WriteLine
                    ($" LV. {Level}\n " +
                    $"{Name + " ( 전사 )"}\n " +
                    $"공격력 : {Atk}\n " +
                    $"방어력 : {Def}\n " +
                    $"체  력 : {HP}\n " +
                    $"Gold   : {Gold}");

                Console.WriteLine("");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("");
                Console.Write("원하시는 행동을 입력 해주세요.\n>>");
            }
        }

        static void StateSelectMenu()
        {
            while (true)
            {
                ConsoleKeyInfo select = Console.ReadKey();
                switch (select.Key)
                {
                    case ConsoleKey.D0:
                        Console.WriteLine();
                        Console.WriteLine("시작메뉴로 돌아갑니다.");
                        break;
                    default:
                        Console.WriteLine("");
                        Console.WriteLine("잘못된 선택입니다. 다시 선택해 주세요");
                        break;
                }
            }
        }

        static void StartMenu()
        {
            Console.Clear();
            Console.WriteLine($"◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆");
            Console.WriteLine($"스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine($"이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine($"◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆");
            Console.WriteLine();
            Console.WriteLine("1.상태 보기");
            Console.WriteLine("2.전투 시작");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.WriteLine(">>");

            //int keyInput = int.Parse(Console.ReadLine());
            switch (CheckValidInput(1, 2))
            {
                case 1:
                    StateSelectMenu();
                    break;
                case 2:
                    BattleStart();
                    break;

            }
        }

        private static int CheckValidInput(int min, int max)
        {
            // 설명
            // 아래 두가지 상황은 비정상 -> 재입력 수행
            // (1) 숫자가 아닌 입력을 받은 경위
            // (2) 숫자가 최솟값-최댓값의 범위를 넘는 경우

            int keyInput;
            bool result;
            do
            {
                Console.WriteLine("원하시는 행동을 입력하세요");
                result = int.TryParse(Console.ReadLine(), out keyInput);
            } while (result == false || CheckIfValid(keyInput, min, max) == false);

            //제대로 입력을 받았다는 뜻
            return keyInput;

        }

        //StartMenu : 선택 검증2
        private static bool CheckIfValid(int keyInput, int min, int max)
        {
            if (min <= keyInput && keyInput <= max) return true;
            return false;
        }

        static void PrintStartLogo()
        {
            Console.WriteLine($"▄████████    ▄███████▄    ▄████████    ▄████████     ███        ▄████████\n" +
                              $"███    ███   ███    ███   ███    ███   ███    ███ ▀█████████▄   ███    ███\n" +
                              $"███    █▀    ███    ███   ███    ███   ███    ███    ▀███▀▀██   ███    ███\n" +
                              $"███          ███    ███   ███    ███  ▄███▄▄▄▄██▀     ███   ▀   ███    ███\n" +
                              $"▀███████████ ▀█████████▀  ▀███████████ ▀▀███▀▀▀▀▀     ███     ▀███████████\n" +
                              $"       ███   ███          ███    ███ ▀███████████     ███       ███    ███\n" +
                              $" ▄█    ███   ███          ███    ███   ███    ███     ███       ███    ███\n" +
                              $"▄████████▀  ▄████▀        ███    █▀    ███    ███    ▄████▀     ███    █▀\n" +
                              $"\n" +
                              $"████████▄  ███    █▄  ███▄▄▄▄      ▄██████▄     ▄████████  ▄██████▄  ███▄▄▄▄\n" +
                              $"███   ▀███ ███    ███ ███▀▀▀██▄   ███    ███   ███    ███ ███    ███ ███▀▀▀██▄\n" +
                              $"███    ███ ███    ███ ███   ███   ███    █▀    ███    █▀  ███    ███ ███   ███\n" +
                              $"███    ███ ███    ███ ███   ███  ▄███         ▄███▄▄▄     ███    ███ ███   ███\n" +
                              $"███    ███ ███    ███ ███   ███ ▀▀███ ████▄  ▀▀███▀▀▀     ███    ███ ███   ███\n" +
                              $"███    ███ ███    ███ ███   ███   ███    ███   ███    █▄  ███    ███ ███   ███\n" +
                              $"███   ▄███ ███    ███ ███   ███   ███    ███   ███    ███ ███    ███ ███   ███\n" +
                              $"████████▀  ████████▀   ▀█   █▀    ████████▀    ██████████  ▀██████▀   ▀█   █▀\n" +
                              $"================================================================================\n" +
                              $"                           PRESS ANYKEY TO START                                \n" +
                              $"================================================================================\n");

            Console.ReadKey();
        }
    }
}