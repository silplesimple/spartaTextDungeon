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
            public int Level;
            public string Name;
            public int Atk;
            public int Def;
            public int HP;
            public int Gold;
            //기본값은 private로 보호하고 변동될 수 있는 스텟들 구현하기

            public void PrintState()
            {
                Console.WriteLine("상태보기"); //글씨 색 넣기 추가
                Console.WriteLine("캐릭터의 정보가 표시됩니다.");
                Console.WriteLine();
                Console.WriteLine($"LV. {Level}");
            }
        }
       
        static void Main()
        {
            PlayerState newstate=new PlayerState();
            newstate.Level = 01;

            newstate.PrintState();
        }
    }
}