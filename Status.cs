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
            public int level;
            public string name;
            public int atkStat;
            public int defSts;
            public int HP;
            public int gold;
            //기본값은 private로 보호하고 변동될 수 있는 스텟들 구현하기
            public void PrintState()
            {
                Console.WriteLine("상태보기"); //글씨 색 넣기 추가
                Console.WriteLine("캐릭터의 정보가 표시됩니다.");
                Console.WriteLine();
                Console.WriteLine($"LV. {level}");
            }
        }
       
        static void Main(string[] args)
        {
            PlayerState newstate=new PlayerState();
            newstate.level = 01;


            newstate.PrintState();

        }

    }
}