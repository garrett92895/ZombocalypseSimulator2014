using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Models;
using ZombieApocalypseSimulator.Models.Characters;

namespace ZombieApocalypseSimulator
{
    public class CharacterStack
    {
        public CharacterStack(int size)
        {
            _characters = new Character[size];
            currentIndex = 0;
        }

        public void Push(Character c)
        {
            _characters[currentIndex++] = c;
        }

        public Character Pop()
        {
            return _characters[--currentIndex];
        }

        public Character Peek()
        {
            return _characters[currentIndex];
        }

        public string GetList()
        {
            string list = "";
            for (int i = 0; i < _characters.Length; i++)
            {
                list += (i + 1) + ".) "+ _characters[i];
            }
            return list;
        }

        public void RemoveCharacter(Character c)
        {
            for (int i = 0; i < _characters.Length; i++)
            {
                if (_characters[i].Equals(c))
                {
                    _characters[i] = null;
                }
            }

            for (int i = 0; i < _characters.Length; i++)
            {
                if (_characters[i] == null)
                {
                    _characters[i] = _characters[i + 1];
                    _characters[i + 1] = null;
                }
            }
            currentIndex--;
        }


        private Character[] _characters;

        private int currentIndex;
    }
}
