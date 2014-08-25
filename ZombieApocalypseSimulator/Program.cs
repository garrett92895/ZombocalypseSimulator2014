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
            Controller c = new Controller(10,10);
            Character NewPlayer = new Player();
            Coordinate Coor = new Coordinate(2,3);
            c.AddCharacterToField(NewPlayer, Coor);

            Character Zed1 = ZedFactory.GetInstance("Sloucher");
            Coordinate ZedCoor1 = new Coordinate(1, 4);
            c.AddCharacterToField(Zed1, ZedCoor1);

            Character Zed2 = ZedFactory.GetInstance("Sloucher");
            Coordinate ZedCoor2 = new Coordinate(8, 9);
            c.AddCharacterToField(Zed2, ZedCoor2);

            Character Zed3 = ZedFactory.GetInstance("Sloucher");
            Coordinate ZedCoor3 = new Coordinate(7, 7);
            c.AddCharacterToField(Zed3, ZedCoor3);

            Character Zed4 = ZedFactory.GetInstance("Sloucher");
            Coordinate ZedCoor4 = new Coordinate(7, 8);
            c.AddCharacterToField(Zed4, ZedCoor4);

            Character Zed5 = ZedFactory.GetInstance("Sloucher");
            Coordinate ZedCoor5 = new Coordinate(7, 9);
            c.AddCharacterToField(Zed5, ZedCoor5);

            Character Zed6 = ZedFactory.GetInstance("Sloucher");
            Coordinate ZedCoor6 = new Coordinate(5, 5);
            c.AddCharacterToField(Zed6, ZedCoor6);

            Item Gun = WeaponFactory.GetInstance("Winchester|Ranged|Shotgun|80|3d6|4");
            Coordinate GunCoor = new Coordinate(1, 2);
            c.AddItemToField(Gun, GunCoor);

            c.Run();
        }
    }
}
