﻿using System;

namespace spartaTextDungeon
{
    internal class StartMenu
    {
        //static void Main(string[] args)
        //{
        //    PrintStartLogo();
        //    StartMenu1();
        //}

        public static void StartMenu1()
        {
            // 구성
            // 0. 화면정리
            // 1.선택 멘트를 줌
            // 2. 선택결과값을 검증함
            // 3. 선택 결과에 따라 메뉴로 보내줌
            
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




        }
        public static void PrintStartLogo()
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