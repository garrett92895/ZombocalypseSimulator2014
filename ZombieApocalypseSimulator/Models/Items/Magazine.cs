using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieApocalypseSimulator.Models.Items
{
    public class Magazine : Item
    {
        public Magazine()
        {
            ClipSize = 20;
            _clip = new Ammo[ClipSize];
            CurrentIndex = 0;
        }

        public Magazine(int MagSize)
        {
            ClipSize = MagSize;
            _clip = new Ammo[ClipSize];
            CurrentIndex = 0;
        }

        public void Empty()
        {
            _clip = new Ammo[ClipSize];
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

        public int Amount()
        {
            int UnusedRounds =0;
            for (int i = 0; i < _clip.Length; i++)
            {
                if (!_clip[i].IsUsed)
                {
                    UnusedRounds++;
                }
            }
            return UnusedRounds;
        }

        public bool HasNext()
        {
            return (this.Peek().IsUsed);
        }

        private int ClipSize { get; set; }
        private Ammo[] _clip { get; set; }
        private int CurrentIndex;
    }
}
