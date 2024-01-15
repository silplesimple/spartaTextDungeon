using System.Numerics;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace spartaTextDungeon.finalFile
{
    internal class Program
    {
        static Player? _player;
        static List<Monster>? _monsters;
        static void Main(string[] args)
        {
            GameDataSetting();
            PrintStartLogo();
            StartMenu();
        }

        private static void GameDataSetting()
        {
            _player = new Player("Chad", "전사", 100, 100, 10, 5, 1500, 1);
            List<Monster> createMonster = new List<Monster>{
            new Monster("미니언", 15, 5, 6,0,2),
            new Monster("대포미니언", 25, 8, 10,1,5),
            new Monster("공허충", 10, 3, 8,2,3) };
            _monsters = RandomMonster(createMonster);
        }
        private static List<Monster> RandomMonster(List<Monster> monsters)
        {
            List<Monster> saveMonster = new List<Monster>();
            Random random = new Random();
            int rndcnt = random.Next(1, 5);
            for (int i = 0; i < rndcnt; i++)
            {
                int rndIndex = random.Next(0, monsters.Count);
                Monster selectedMonster = (Monster)monsters[rndIndex].Clone();
                saveMonster.Add(selectedMonster);
            }
            return saveMonster;
        }
        private static void StartMenu()
        {
            Console.Clear();
            Console.WriteLine($"◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆");
            Console.WriteLine($"스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine($"이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine($"◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆");
            Console.WriteLine();
            Console.WriteLine("1.상태 보기");
            Console.WriteLine("2.전투 시작");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.WriteLine(">>");

            switch (CheckVailedInput(1, 2))
            {
                case 1:
                    State();
                    break;
                case 2:
                    Battle();
                    break;
            }
        }

        private static void Battle()
        {
            Console.Clear();
            Console.WriteLine("Battle!!\n");
            playerInfo();
            monterInfo();
            Console.WriteLine("\n1. 공격\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.\n>>");            
            switch (CheckVailedInput(1, 1))
            {
                case 1:
                    Attack();
                        break;
            }                                              
                //_monsters.ForEach(monster => { if (monster.CheckIndex == CheckVailedInput(1, monster.CheckIndex)) });              
        }
        static void monterInfo()
        {
            foreach (Monster monster in _monsters)
            {                
                if(monster.IsDead())
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"{monster}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"{monster}");
                }
            }
            Console.Write("\n");
        }

        static void playerInfo()
        {                        
            Console.WriteLine($"\n[내정보]");
            Console.WriteLine($"{_player}");
            Console.WriteLine("");                        
        }

        static void Attack()
        {
            Console.Clear();
            Console.WriteLine("Battle!!\n");
            for (int i = 0; i < _monsters.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_monsters[i]}");
            }
            Console.WriteLine("");
            playerInfo();
            Console.WriteLine("0. 취소\n");
            Console.WriteLine("대상을 입력해주세요\n>>");
            int choice= CheckVailedInput(0,_monsters.Count);
            if (choice == 0)
            {
                Battle();
            }
            else
            {
                Attack(_monsters[choice-1]);                
            }                                                      
            //Console.WriteLine("\n게임 종료");
            
        }

        static void Attack(Monster monster)
        {
            if (monster.IsDead())
            {
                Console.Clear();
                Console.WriteLine("잘못된 입력입니다. 이미 죽은 몬스터를 공격할 수 없습니다.");
                Console.WriteLine("\n0 다음");
                switch (CheckVailedInput(0, 0))
                {
                    case 0:
                         Battle();
                        break;
                }
            }
            else
            {                
                Console.WriteLine($"몬스터 {monster.Name}을(를) 공격합니다.");
                int damage = CalculateDamage(_player.Attack);
                //monster.TakeDamage(damage);
                monster.HP -= damage;                
                Console.WriteLine($"몬스터에게 {damage}의 데미지를 입혔습니다.");
                if (monster.IsDead())
                {
                    Console.WriteLine($"몬스터 {monster.Name}을(를) 처치했습니다.");
                }
                Console.WriteLine("\n0 다음");
                switch (CheckVailedInput(0, 0))
                {
                    case 0:
                        EnemyPhase();
                        break;
                }
            }
        }

        static int CalculateDamage(int baseAttack)
        {
            Random random = new Random();
            double error = Math.Ceiling(baseAttack * 0.1);      // 공격력의 10% 오차, 소수점은 올림 처리
            int randomValue = random.Next(-(int)error, (int)error + 1);
            return baseAttack + randomValue;
        }            

        private static void EnemyPhase()
        {
            Console.Clear();            
            foreach (Monster monster in _monsters)
            {
                if (!monster.IsDead())
                {
                    Console.Clear();
                    Console.WriteLine("Battle!!\n");
                    Console.WriteLine($"Lv.{monster.Level} {monster.Name}의 공격!");
                    Console.WriteLine($"{_player.Name} 을(를) 맞췄습니다. [데미지 : {monster.Attack}]\n");
                    Console.WriteLine($"Lv.{_player.Level} {_player.Name}");
                    Console.Write($"HP {_player.HP}");
                    _player.HP -= monster.Attack;
                    Console.WriteLine($"-> {_player.HP}\n");
                    Console.WriteLine("0.다음\n");
                    Console.WriteLine("대상을 선택해주세요.\n>>");
                    CheckVailedInput(0, 0);
                }               
            }                       
            Battle();                   
                   //PlayerTurn(saveMonsters, player);
        }

        private static void State()
        {
            Console.Clear();                      
            ChangeTextColor("상태보기", ConsoleColor.Green);
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            Console.WriteLine
                   ($" LV. {_player.Level}\n " +
                   $"{_player.Name} ( {_player.Class} )\n " +
                   $"공격력 : {_player.Attack}\n " +
                   $"방어력 : {_player.Def}\n " +
                   $"체  력 : {_player.HP}\n " +
                   $"Gold   : {_player.Gold}");
            Console.WriteLine("");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("");
            Console.Write("원하시는 행동을 입력 해주세요.\n>>");
            switch (CheckVailedInput(0, 0))
            {
                case 0:
                    StartMenu();
                    break;
            }

        }

        private static void ChangeTextColor(string text,ConsoleColor consoleColor)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        private static int CheckVailedInput(int min, int max)
        {
            int saveIndex;
            while (true)
            {
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("잘못된 입력입니다");
                    continue;
                }
                if (!int.TryParse(input, out saveIndex))
                {
                    Console.WriteLine("잘못된 입력입니다");
                    continue;
                }
                if (saveIndex < min || saveIndex > max)
                {
                    Console.WriteLine("잘못된 입력입니다");
                    continue;
                }
                break;
            }
            return saveIndex;
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
