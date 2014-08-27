
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Factories;
using ZombieApocalypseSimulator.Models.Characters;
using ZombieApocalypseSimulator.Models.Items;

namespace ZombieApocalypseSimulator
{
    public class Program
    {
        static void Main(string[] args)
        {
            Controller c = new Controller(10, 10);

            Player NewPlayer = new Player();
            Coordinate Coor = new Coordinate(2, 3);
            c.AddCharacterToField(NewPlayer, Coor);

            //Test 1
            Character Zed1 = ZedFactory.GetInstance("Shank");
            Coordinate ZedCoor1 = new Coordinate(5, 3);
            c.AddCharacterToField(Zed1, ZedCoor1);

            Character Zed2 = ZedFactory.GetInstance("Tank");
            Coordinate ZedCoor2 = new Coordinate(8, 3);
            c.AddCharacterToField(Zed2, ZedCoor2);

            //Character Zed3 = ZedFactory.GetInstance("Sloucher");
            //Coordinate ZedCoor3 = new Coordinate(3, 6);
            //c.AddCharacterToField(Zed3, ZedCoor3);

            //Character Zed4 = ZedFactory.GetInstance("Sloucher");
            //Coordinate ZedCoor4 = new Coordinate(2, 2);
            //c.AddCharacterToField(Zed4, ZedCoor4);

            //Character Zed5 = ZedFactory.GetInstance("Sloucher");
            //Coordinate ZedCoor5 = new Coordinate(4, 5);
            //c.AddCharacterToField(Zed5, ZedCoor5);

            //Character Zed6 = ZedFactory.GetInstance("Sloucher");
            //Coordinate ZedCoor6 = new Coordinate(4, 6);
            //c.AddCharacterToField(Zed6, ZedCoor6);

            //Character Zed7 = ZedFactory.GetInstance("Sloucher");
            //Coordinate ZedCoor7 = new Coordinate(5, 6);
            //c.AddCharacterToField(Zed7, ZedCoor7);

            //Character Zed8 = ZedFactory.GetInstance("Sloucher");
            //Coordinate ZedCoor8 = new Coordinate(3, 9);
            //c.AddCharacterToField(Zed8, ZedCoor8);


            c.Run();
        }
    }
}
