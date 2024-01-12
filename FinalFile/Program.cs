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
            new Monster("Lv.2 미니언", 15, 5, 6,0),
            new Monster("Lv.5 대포미니언", 25, 8, 10,1),
            new Monster("LV.3 공허충", 10, 3, 8,2) };
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
                foreach (Monster monster in monsters)
                {
                    if (rndIndex == monster.CheckIndex)
                    {
                        saveMonster.Add(monster);
                        break;
                    }
                }

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

        }

        private static void State()
        {
            Console.Clear();
            Console.WriteLine("상태보기"); //글씨 색 넣기 추가
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
