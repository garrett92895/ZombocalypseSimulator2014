using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieApocalypseSimulator.Models.Items
{
    public class Magazine : Item
    {

        public Magazine(int MagSize = 20)
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
            if (CurrentIndex == 0)
            {
                return _clip[CurrentIndex];
            }
            return _clip[--CurrentIndex];
        }

        public Ammo Peek()
        {
            if (CurrentIndex == 0)
            {
                return _clip[CurrentIndex];
            }
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
            //I don't know what this is supposed to do so it just returns true for now
            return true;
            //return (this.Peek().IsUsed);
        }

        private int ClipSize { get; set; }
        private Ammo[] _clip { get; set; }
        private int CurrentIndex;
    }
}
