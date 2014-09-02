
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Factories;
using ZombieApocalypseSimulator.Models.Characters;
using ZombieApocalypseSimulator.Models.Characters.Classes;
using ZombieApocalypseSimulator.Models.Items;

namespace ZombieApocalypseSimulator
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(120, 50);
            Controller c = new Controller(20, 20);

            Character NewPlayer = new Player();
            NewPlayer.Money = 110;
            Coordinate Coor = new Coordinate(12, 13);
            c.AddCharacterToField(NewPlayer, Coor);
            //for (int i = 0; i < 10; i++)
            //{
            //    NewPlayer.Items.Add(WeaponFactory.RandomWeapon());
            //}

            //Character Trader = new Trader(100);
            //Coordinate TraderCoor = new Coordinate(2, 2);
            //c.AddCharacterToField(Trader, TraderCoor);
            //for (int i = 0; i < 10; i++)
            //{
            //    Trader.Items.Add(WeaponFactory.RandomWeapon());
            //}

<<<<<<< HEAD
            /*
             * Run Away Test
             */
            //Character Zed6 = ZedFactory.GetInstance("Sloucher");
            //Coordinate ZedCoor6 = new Coordinate(13, 13);
            //Zed6.Health = 1;
            //c.AddCharacterToField(Zed6, ZedCoor6);

            /*
             * Cluster test
             */
            Character Zed2 = ZedFactory.GetInstance("Tank");
            Coordinate ZedCoor2 = new Coordinate(12, 2);
=======
            Character Zed1 = ZedFactory.GetInstance("Shank");
            Coordinate ZedCoor1 = new Coordinate(5, 3);
            c.AddCharacterToField(Zed1, ZedCoor1);

            Character Zed2 = ZedFactory.GetInstance("Tank");
            Coordinate ZedCoor2 = new Coordinate(2, 2);
>>>>>>> origin/master
            c.AddCharacterToField(Zed2, ZedCoor2);

            Character Zed4 = ZedFactory.GetInstance("Sloucher");
            Coordinate ZedCoor4 = new Coordinate(4, 12);
            c.AddCharacterToField(Zed4, ZedCoor4);

            Character Zed5 = ZedFactory.GetInstance("Sloucher");

            Coordinate ZedCoor5 = new Coordinate(12, 7);
            c.AddCharacterToField(Zed5, ZedCoor5);

            Character Zed6 = ZedFactory.GetInstance("Sloucher");
            Coordinate ZedCoor6 = new Coordinate(3, 17);
            c.AddCharacterToField(Zed6, ZedCoor6);

            Character Zed7 = ZedFactory.GetInstance("Sloucher");
            Coordinate ZedCoor7 = new Coordinate(3, 18);
            c.AddCharacterToField(Zed7, ZedCoor7);

            Character Zed8 = ZedFactory.GetInstance("Sloucher");
            Coordinate ZedCoor8 = new Coordinate(13, 9);
            c.AddCharacterToField(Zed8, ZedCoor8);

            /*
             * Attack high SDC Players
             */

            //NewPlayer = new Player();
            //NewPlayer.Money = 110;
            ////NewPlayer.Health = 5;
            //Coor = new Coordinate(14, 13);
            //c.AddCharacterToField(NewPlayer, Coor);

            //Character Zed2 = ZedFactory.GetInstance("Shank");
            //Coordinate ZedCoor2 = new Coordinate(13, 13);
            //c.AddCharacterToField(Zed2, ZedCoor2);


            /*
             * Attack vulnerable players
             */
            //NewPlayer = new Player();
            //NewPlayer.Money = 110;
            //NewPlayer.Health = 5;
            //Coor = new Coordinate(14, 13);
            //c.AddCharacterToField(NewPlayer, Coor);

            //Character Zed2 = ZedFactory.GetInstance("Sloucher");
            //Coordinate ZedCoor2 = new Coordinate(13, 13);
            //c.AddCharacterToField(Zed2, ZedCoor2);


            c.Run();
        }
    }
}
