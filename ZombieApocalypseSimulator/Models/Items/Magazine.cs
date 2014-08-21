using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieApocalypseSimulator.Models.Items
{
    public class Magazine
    {
        public Magazine()
        {
            _clip = new Ammo[20];
            CurrentIndex = 0;
        }

        public Magazine(int MagSize)
        {
            _clip = new Ammo[MagSize];
            CurrentIndex = 0;
        }

        public void Push(Ammo a)
        {
            _clip[CurrentIndex++] = a;
        }

        public Ammo Pop()
        {
            return _clip[--CurrentIndex];
        }

        public Ammo Peek()
        {
            return _clip[(CurrentIndex - 1)];
        }

        private Ammo[] _clip { get; set; }
        private int CurrentIndex;
    }
}
