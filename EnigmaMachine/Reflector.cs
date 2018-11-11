using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaMachine
{
    class Reflector 
    {
        private string permutation;
        private char ringSetting;
        private int position;

        public int[] map;
        public int[] reverseMap;
        public Reflector(string perm)
        {
            position = 0;
            ringSetting = 'A';

            map = new int[26];
            reverseMap = new int[26];

            setPermutation(perm);
        }

        public void setPermutation(string perm)
        {
            permutation = perm;

            //displayChar = permutation.ToCharArray()[position];

            for (int i = 0; i < 26; i++)
            {
                int value = ((int)permutation.ToCharArray()[i]) - 65;
                map[i] = (value - i + 26) % 26;
                reverseMap[i] = (26 + i - value) % 26;
            }
        }
    }
}
