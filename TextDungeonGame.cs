using System.Numerics;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace spartaTextDungeon
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
            List<Monster> createMonster = new List<Monster> {
            new Monster("미니언", 15, 5, 6, 0, 2),
            new Monster("대포미니언", 25, 8, 10, 1, 5),
            new Monster("공허충", 10, 3, 8, 2, 3) };
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
            Console.WriteLine("");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

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
            ChangeTextColor("Battle!!", ConsoleColor.DarkYellow);
            Console.WriteLine("");
            Console.WriteLine("[몬스터 정보]");
            monsterInfo();
            playerInfo();
            Console.WriteLine("1. 공격");
            Console.WriteLine("0. 나가기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            switch (CheckVailedInput(0, 1))
            {
                case 0:
                    StartMenu();
                    break;
                case 1:
                    Attack();
                    break;
            }
        }
        static void monsterInfo()
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
            ChangeTextColor("Battle!!\n", ConsoleColor.DarkYellow);
            for (int i = 0; i < _monsters.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_monsters[i]}");
            }
            Console.WriteLine("");
            playerInfo();
            Console.WriteLine("0. 취소\n");
            Console.WriteLine("대상을 선택해주세요.");
            Console.Write(">>");
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
                Console.WriteLine("");
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

        static void DisplayInfo(Player player, Monster[] monsters)
        {
            Console.Clear();
            ChangeTextColor("Battle!!", ConsoleColor.DarkYellow);
            Console.WriteLine($"\n[내정보]");
            Console.WriteLine($"{player}");
            Console.WriteLine("");
            foreach (var monster in monsters)
            {
                Console.WriteLine($"{monster}");
            }
            Console.WriteLine("");
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


        internal class Player
        {
            public string Name { get; }
            public string Class { get; }
            public int MaxHP { get; }
            public int HP { get; set; }
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

        internal class Monster
        {
            public string Name { get; }
            public int MaxHP { get; }
            public int HP { get; set; }
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

        static void PrintStartLogo()
        {
            Console.WriteLine(" .----------------.  .----------------.  .----------------.  .----------------.  .----------------. ");
            Console.WriteLine("| .--------------. || .--------------. || .--------------. || .--------------. || .--------------. |");
            Console.WriteLine("| |    _______   | || |   ______     | || |      __      | || |  _______     | || |  _________   | |");
            Console.WriteLine("| |   /  ___  |  | || |  |_   __ \\   | || |     /  \\     | || | |_   __ \\    | || | |  _   _  |  | |");
            Console.WriteLine("| |  |  (__ \\_|  | || |    | |__) |  | || |    / /\\ \\    | || |   | |__) |   | || | |_/ | | \\_   | |");
            Console.WriteLine("| |   '.___`-.   | || |    |  ___/   | || |   / ____ \\   | || |   |  __ /    | || |     | |      | |");
            Console.WriteLine("| |  |`\\____) |  | || |   _| |_      | || | _/ /    \\ \\  | || |  _| |  \\ \\_  | || |    _| |_     | |");
            Console.WriteLine("| |  |_______.'  | || |  |_____|     | || ||____|  |____|| || | |____| |___| | || |   |_____|    | |");
            Console.WriteLine("| |              | || |              | || |              | || |              | || |              | |");
            Console.WriteLine("| '--------------' || '--------------' || '--------------' || '--------------' || '--------------' |");
            Console.WriteLine(" '----------------'  '----------------'  '----------------'  '----------------'  '----------------' ");

            Console.WriteLine("  .----------------.  .----------------.  .-----------------. .----------------.  .----------------.  .----------------.  .-----------------.");
            Console.WriteLine(" | .--------------. || .--------------. || .--------------. || .--------------. || .--------------. || .--------------. || .--------------. |");
            Console.WriteLine(" | |  ________    | || | _____  _____ | || | ____  _____  | || |    ______    | || |  _________   | || |     ____     | || | ____  _____  | |");
            Console.WriteLine(" | | |_   ___ `.  | || ||_   _||_   _|| || ||_   \\|_   _| | || |  .' ___  |   | || | |_   ___  |  | || |   .'    `.   | || ||_   \\|_   _| | |");
            Console.WriteLine(" | |   | |   `. \\ | || |  | |    | |  | || |  |   \\ | |   | || | / .'   \\_|   | || |   | |_  \\_|  | || |  /  .--.  \\  | || |  |   \\ | |   | |");
            Console.WriteLine(" | |   | |    | | | || |  | '    ' |  | || |  | |\\ \\| |   | || | | |    ____  | || |   |  _|  _   | || |  | |    | |  | || |  | |\\ \\| |   | |");
            Console.WriteLine(" | |  _| |___.' / | || |   \\ `--' /   | || | _| |_\\   |_  | || | \\ `.___]  _| | || |  _| |___/ |  | || |  \\  `--'  /  | || | _| |_\\   |_  | |");
            Console.WriteLine(" | | |________.'  | || |    `.__.'    | || ||_____|\\____| | || |  `._____.'   | || | |_________|  | || |   `.____.'   | || ||_____|\\____| | |");
            Console.WriteLine(" | |              | || |              | || |              | || |              | || |              | || |              | || |              | |");
            Console.WriteLine(" | '--------------' || '--------------' || '--------------' || '--------------' || '--------------' || '--------------' || '--------------' |");
            Console.WriteLine("  '----------------'  '----------------'  '----------------'  '----------------'  '----------------'  '----------------'  '----------------' ");


            Console.WriteLine(" .----------------.  .----------------.  .----------------.  .----------------. ");
            Console.WriteLine("| .--------------. || .--------------. || .--------------. || .--------------. |");
            Console.WriteLine("| |    ______    | || |      __      | || | ____    ____ | || |  _________   | |");
            Console.WriteLine(@"| |  .' ___  |   | || |     /  \     | || ||_   \  /   _|| || | |_   ___  |  | |");
            Console.WriteLine(@"| | / .'   \_|   | || |    / /\ \    | || |  |   \/   |  | || |   | |_  \_|  | |");
            Console.WriteLine(@"| | | |    ____  | || |   / ____ \   | || |  | |\  /| |  | || |   |  _|  _   | |");
            Console.WriteLine("| | \\ `.___]  _| | || | _/ /    \\ \\_ | || | _| |_\\/_| |_ | || |  _| |___/ |  | |");
            Console.WriteLine("| |  `._____.'   | || ||____|  |____|| || ||_____||_____|| || | |_________|  | |");
            Console.WriteLine("| |              | || |              | || |              | || |              | |");
            Console.WriteLine("| '--------------' || '--------------' || '--------------' || '--------------' |");
            Console.WriteLine(" '----------------'  '----------------'  '----------------'  '----------------' ");


            Console.ReadKey();
        }
    }
}