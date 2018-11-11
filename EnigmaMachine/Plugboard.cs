using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaMachine
{
    public class Plugboard
    {
        public struct Pair
        {
            public char char1;
            public char char2;
        }
        public List<Pair> pairs;
        public Plugboard(List<Pair> plugPairs)
        {
            if (plugPairs.Count > 10)
            {
                throw new Exception("Too many connections in plugboard");
            }
            pairs = plugPairs;
        }
        public Plugboard()
        {
            pairs = new List<Pair>();
        }

        public bool containsKey(char c)
        {
            return pairs.Any(p => p.char1 == c || p.char2 == c);
        }

        public char getOppositeKey(char c)
        {
            if (pairs.Any(p => p.char1 == c || p.char2 == c))
            {
                Pair pair = pairs.Find(p => p.char1 == c || p.char2 == c);
                if (pair.char1 == c)
                {
                    return pair.char2;
                }
                else
                {
                    return pair.char1;
                }
            }
            else return c;
        }
    }
}
