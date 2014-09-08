using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Models;
using ZombieApocalypseSimulator.Models.Characters;

namespace ZombieApocalypseSimulator
{
    [Serializable()]
    public class CharacterStack : Stack<Character>
    {
        //public CharacterStack(int size)
        //{
        //    _characters = new Character[size];
        //    currentIndex = 0;
        //}

        //public void Push(Character c)
        //{
        //    _characters[currentIndex++] = c;
        //}

        //public Character Pop()
        //{
        //    if (currentIndex == 0)
        //    {
        //        Character C = _characters[currentIndex];
        //        _characters[currentIndex] = null;
        //        return C;
        //    }
        //    return _characters[--currentIndex];
        //}

        //public Character Peek()
        //{
        //    if (currentIndex == 0)
        //    {
        //        return _characters[currentIndex];
        //    }
        //    return _characters[(currentIndex - 1)];
        //}

        //public string GetList()
        //{
        //    string list = "";
        //    for (int i = 0; i < _characters.Length; i++)
        //    {
        //        list += (i + 1) + ".) "+ _characters[i];
        //    }
        //    return list;
        //}

        public void RemoveCharacter(Character c)
        {
            CharacterStack Temp = new CharacterStack();
            bool Done = false;
            while (this.Count > 0 && !Done)
            {
                Character Check = this.Pop();
                Done = Check.Equals(c);
                if (!Done)
                {
                    Temp.Push(Check);
                }
            }
            while (Temp.Count > 0)
            {
                this.Push(Temp.Pop());
            }
        }


        //private Character[] _characters;

        //private int currentIndex;
    }
}
