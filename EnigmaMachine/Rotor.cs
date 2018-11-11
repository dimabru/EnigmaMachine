using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaMachine
{
    class Rotor
    {
        private string permutation { set; get; }
        public int position { get; private set; }
        public char displayChar;
        private char turnoverNotch;
        public string name { get; }
        public char ringSetting;

        public int[] map;
        public int[] reverseMap;

        public Rotor(string perm, char turnover, string name)
        {
            turnoverNotch = turnover;
            this.name = name;
            position = 0;
            ringSetting = 'A';

            map = new int[26];
            reverseMap = new int[26];

            setPermutation(perm);
        }

        public void setPermutation(string perm)
        {
            permutation = perm;
            displayChar = permutation.ToCharArray()[position];

            for (int i=0; i<26; i++)
            {
                int value = ((int)permutation.ToCharArray()[i]) - 65;
                map[i] = (value - i + 26) % 26;
                reverseMap[i] = (26 + i - value) % 26;
            }
        }

        public void setPosition(int pos)
        {
            position = pos;
            displayChar = Enigma.validChars.ToCharArray()[position];
        }

        public void setDisplayChar(char c)
        {
            displayChar = c;
            position = Enigma.validChars.IndexOf(c);
        }

        public void stepForward()
        {
            position = (position + 1) % 26;
            displayChar = Enigma.validChars.ToCharArray()[position];
        }

        public bool inTurnover()
        {
            return displayChar == turnoverNotch;
        }
    }
}
