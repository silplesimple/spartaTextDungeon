using System.ComponentModel.Design.Serialization;
using System.Numerics;
using System.Reflection.Emit;
using System.Threading;
using System.Xml.Linq;

namespace spartaTextDungeon.finalFile
{
    internal class Program
    {
        static Player? _player;
        static List<Monster>? _monsters;

        

        static void Main(string[] args)
        {           
            PrintStartLogo();
            GameDataSetting();
            StartMenu();
        }
        private static void InputNickname()
        {
            string inputName;
            Console.Clear();
            Console.WriteLine($"◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆");
            Console.WriteLine($"스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine($"원하시는 이름을 설정해주세요.\n>>");
            Console.WriteLine($"◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆");
            while (true)
            {
                inputName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(inputName))
                {
                    Console.WriteLine("잘못된 입력입니다");
                    continue;
                }
                break;                
            }
            _player.Name = inputName;            
        }
        public static void SelectClass()
        {
            Console.Clear ();
            Console.WriteLine($"◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆");
            Console.WriteLine($"원하시는 직업을 선택해주세요");
            Console.WriteLine($"1.초보자(스탯이 일정함)");
            Console.WriteLine($"2.전사(체력이 높지만 공격력이 낮음)");
            Console.WriteLine($"3.도적(체력이 낮지만 공격력이 높음");
            Console.WriteLine($"4.마법사(체력이낮지만 마나가 높음)");
            Console.WriteLine($"◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆");
            //초보자
            switch (CheckVailedInput(1, 4))
            {
                case 1:
                    _player = new Player("이름", "초보자", 100,50, 100,50, 10, 5, 1500, 1);
                    break;
                case 2:
                    _player = new Player("이름", "전사", 250,40, 250,40, 8, 5, 1500, 1);
                    break;
                case 3:
                    _player = new Player("이름", "도적", 75,80, 75, 80,15, 5, 1500, 1);
                    break;
                case 4:
                    _player = new Player("이름", "마법사", 80, 120,80, 120, 10, 5, 1500, 1);
                    break;
            }          
        }
        private static void GameDataSetting()
        {           
            SelectClass();
            InputNickname();
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
            Console.WriteLine("");
            Console.WriteLine("1.상태 보기");
            Console.WriteLine("2.전투 시작");
            Console.WriteLine("");
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
            Console.WriteLine("Battle!!");
            Console.WriteLine("");
            MonsterInfo();
            playerInfo();
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 스킬");
            Console.WriteLine("0. 나가기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.\n>>");
            switch (CheckVailedInput(0, 2))
            {
                case 0:
                    StartMenu();
                    break;
                case 1:
                    Attack();
                    break;
                case 2:
                    Skill();
                    break;
            }       
        }

        private static void Skill()
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine("");
            MonsterInfo();
            playerInfo();
            if(_player.Class=="전사")
            {
                Console.WriteLine("1.알파 스트라이크 - MP 10");
                Console.WriteLine("  공격력 * 2로 하나의 적을 공격합니다");
                Console.WriteLine("2.더블 스트라이크 - MP 15");
                Console.WriteLine("  공격력 * 1.5로 2명의 적을 랜덤으로 공격합니다");
                Console.WriteLine("0.취소\n");
                Console.WriteLine("원하시는 행동을 입력해주세요\n>>");
                switch (CheckVailedInput(0, 2))
                {
                    case 0:
                        Battle();
                        break;
                    case 1:
                        AlphaStrike();
                        break;
                    case 2:
                        break;
                    
                }

            }
        }

        private static void AlphaStrike()
        {
            Console.Clear();
            for (int i = 0; i < _monsters.Count; i++)
            {
                if (_monsters[i].IsDead())
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"{i + 1}. {_monsters[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"{i + 1}. {_monsters[i]}");
                }
            }
            Console.WriteLine("맞출 대상을 선택해주세요");
            Console.WriteLine("0.취소");
            int choice = CheckVailedInput(0, _monsters.Count);
            if (choice == 0)
            {
                Skill();
            }
            else
            {
                AlphaStrikeAttack(choice);
            }            
        }
        private static void AlphaStrikeAttack(int choiceIndex)
        {
            Console.Clear();
            int alphaMp = 10;
            int alphaStrike = _player.Attack * 2;
            _player.MP-=alphaMp;
            _monsters[choiceIndex - 1].HP -= alphaStrike;
            Console.WriteLine($"{_player.Name}의 스킬 발동!");
            Console.WriteLine($"알파 스트라이크!");
            Console.WriteLine($"\n{_monsters[choiceIndex - 1].Name}의 HP가 {alphaStrike}만큼 닳았습니다! ");
            if (_monsters[choiceIndex - 1].IsDead())
            {
                Console.WriteLine($"몬스터 {_monsters[choiceIndex - 1].Name}을(를) 처치했습니다.");
            }
            Console.WriteLine("\n0.다음");
            switch(CheckVailedInput(0,0))
            {
                case 0:
                    EnemyPhase();
                    break;
            }
        }


        static void MonsterInfo()
        {
            foreach (Monster monster in _monsters)
            {
                if (monster.IsDead())
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
            Console.WriteLine($"[내정보]");
            Console.WriteLine($"{_player}");
            Console.WriteLine("");
        }

        static void Attack()
        {
            Console.Clear();
            Console.WriteLine("Battle!!\n");
            for (int i = 0; i < _monsters.Count; i++)
            {
                if (_monsters[i].IsDead())
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"{i + 1}. {_monsters[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"{i + 1}. {_monsters[i]}");
                }
            }
            Console.WriteLine("");
            playerInfo();
            Console.WriteLine("0. 취소\n");
            Console.WriteLine("대상을 선택해주세요.\n>>");
            int choice = CheckVailedInput(0, _monsters.Count);
            if (choice == 0)
            {
                Battle();
            }
            else
            {
                Attack(_monsters[choice - 1]);
            }
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

        private static void EnemyPhase()
        {
            Console.Clear();
            bool allMonstersDead = true;
            foreach (Monster monster in _monsters)
            {
                if (!monster.IsDead())
                {
                    Console.Clear();
                    allMonstersDead = false;
                    ChangeTextColor("Battle!!\n", ConsoleColor.DarkYellow);
                    Console.WriteLine($"Lv.{monster.Level} {monster.Name}의 공격!");
                    Console.WriteLine($"{_player.Name} 을(를) 맞췄습니다. [데미지 : {monster.Attack}]\n");
                    Console.WriteLine($"Lv.{_player.Level} {_player.Name}");
                    Console.Write($"HP {_player.HP}");
                    _player.HP -= monster.Attack;
                    Console.WriteLine($"-> {_player.HP}\n");
                    Console.WriteLine("0.다음\n");
                    Console.WriteLine("대상을 선택해주세요.\n");
                    Console.Write(">>");
                    CheckVailedInput(0, 0);
                }
            }

            if (allMonstersDead)
            {
                Console.Clear();
                ChangeTextColor("Battle!! - Result", ConsoleColor.DarkYellow);
                Console.WriteLine("");
                ChangeTextColor("Victory!", ConsoleColor.Green);
                Console.WriteLine("");
                Console.WriteLine($"던전에서 몬스터 {_monsters.Count}마리를 잡았습니다.");
                Console.WriteLine("");
                Console.WriteLine($"{_player}\n");
                Console.WriteLine("0. 다음\n>>");
                CheckVailedInput(0, 0);
                Console.Clear();
                Console.WriteLine("게임 종료");
                Console.ReadKey();
                Environment.Exit(0);
            }

            if (_player.IsDead())
            {
                Console.Clear();
                ChangeTextColor("Battle!! - Result", ConsoleColor.DarkYellow);
                Console.WriteLine("");
                ChangeTextColor("You Lose", ConsoleColor.Red);
                Console.WriteLine("");
                Console.WriteLine($"{_player}\n");
                Console.WriteLine("0. 다음\n>>");
                Console.WriteLine("");
                CheckVailedInput(0, 0);
                Console.ReadKey();
                Environment.Exit(0);
            }

            Battle();
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
            double error = Math.Ceiling(baseAttack * 0.1);      // 공격력의 10% 오차, 소수점은 올림 처리
            int randomValue = random.Next(-(int)error, (int)error + 1);
            return baseAttack + randomValue;
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
                   $"마  나 : {_player.MP}\n " +
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

        private static void ChangeTextColor(string text, ConsoleColor consoleColor)
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
