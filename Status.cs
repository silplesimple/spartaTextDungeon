using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace spartaTextDungeon
{
    internal class Status
    {
        public class PlayerState
        {
            int Level = 01;
            public string Name = "none";
            int Atk = 10;
            int Def = 5;
            int HP = 100;
            int Gold = 1500;
            //기본값은 private로 보호하고 변동될 수 있는 스텟들 구현하기

            public void PrintState()
            {
                Console.WriteLine("상태보기"); //글씨 색 넣기 추가
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
                        break; //return 시작메뉴화면
                    default:
                        Console.WriteLine("");
                        Console.WriteLine("잘못된 선택입니다. 다시 선택해 주세요");
                        break;
                }
            }
        }

        static void Main()
        {
            PlayerState newPlayerstate = new PlayerState();

            newPlayerstate.PrintState();
            StateSelectMenu();

        }


    }
}