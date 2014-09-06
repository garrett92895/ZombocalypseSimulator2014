using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieApocalypseSimulator
{
    public class PathCoordinate : Coordinate
    {
        public int DistanceLeft { get; set; }

        public PathCoordinate(Coordinate BaseCoordinate, int NewDistanceLeft = 0)
        {
            X = BaseCoordinate.X;
            Y = BaseCoordinate.Y;
            DistanceLeft = NewDistanceLeft;
        }
    }
}
