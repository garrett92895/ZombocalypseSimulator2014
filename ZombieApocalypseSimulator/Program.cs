using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Factories;
using ZombieApocalypseSimulator.Models.Characters;

namespace ZombieApocalypseSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Controller c = new Controller(10,10);
            Character NewPlayer = new Player();
            Coordinate Coor = new Coordinate(2,3);
            c.AddCharacterToField(NewPlayer, Coor);
            
            for (int i = 0; i < 5; i++)
            {
                Character Zed = ZedFactory.GetInstance("Sloucher");
                c.AddCharacterToField(Zed);
            }
            c.Run();
        }
    }
}
