using System;

class Program
{
    static void Main()
    {
        Player player = new Player("Chad", "전사", 100);
        Monster[] monsters = {
            new Monster("Lv.2 미니언", 15, 5),
            new Monster("Lv.5 대포미니언", 25, 8),
            new Monster("LV.3 공허충", 10, 3)
        };

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
                AttackPhase(player, monsters);
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

    static void AttackPhase(Player player, Monster[] monsters)
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
            int damage = CalculateDamage(player.AttackPower);
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

    static int GetUserInput(int maxChoice)
    {
        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > maxChoice)
        {
            Console.WriteLine("올바르지 않은 입력입니다. 다시 입력해주세요.");
        }
        return choice;
    }
}

class Player
{
    public string Name { get; }
    public string Class { get; }
    public int MaxHP { get; }
    public int CurrentHP { get; private set; }
    public int AttackPower { get; }

    public Player(string name, string playerClass, int maxHP)
    {
        Name = name;
        Class = playerClass;
        MaxHP = maxHP;
        CurrentHP = maxHP;
        AttackPower = 10;
    }

    public void TakeDamage(int damage)
    {
        CurrentHP -= damage;
        if (CurrentHP < 0)
        {
            CurrentHP = 0;
        }
    }

    public bool IsDead()
    {
        return CurrentHP <= 0;
    }

    public override string ToString()
    {
        return $"Lv.1 {Name} ({Class})\nHP {CurrentHP}/{MaxHP}";
    }
}

class Monster
{
    public string Name { get; }
    public int MaxHP { get; }
    public int CurrentHP { get; private set; }
    public int AttackPower { get; }

    public Monster(string name, int maxHP, int attackPower)
    {
        Name = name;
        MaxHP = maxHP;
        CurrentHP = maxHP;
        AttackPower = attackPower;
    }

    public bool IsDead()
    {
        return CurrentHP <= 0;
    }

    public void TakeDamage(int damage)
    {
        CurrentHP -= damage;
        if (CurrentHP < 0)
        {
            CurrentHP = 0;
        }
    }

    public override string ToString()
    {
        string status = IsDead() ? "Dead" : $"HP {CurrentHP}";
        return $"{Name}  {status}";
    }
}