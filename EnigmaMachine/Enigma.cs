using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EnigmaMachine.Plugboard;

namespace EnigmaMachine
{
    class Enigma
    {
        List<Rotor> rotors;
        List<Rotor> selectedRotors;
        Reflector reflector;
        Plugboard plugboard;

        public static readonly string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public Enigma()
        {
            plugboard = new Plugboard();
            selectedRotors = new List<Rotor>();
            rotors = new List<Rotor>()
            {
                new Rotor("EKMFLGDQVZNTOWYHXUSPAIBRCJ", 'Q', "I"),
                new Rotor("AJDKSIRUXBLHWTMCQGZNPYFVOE", 'E', "II"),
                new Rotor("BDFHJLCPRTXVZNYEIWGAKMUSQO", 'V', "III"),
                new Rotor("ESOVPZJAYQUIRHXLNFTGKDCMWB", 'J', "IV"),
                new Rotor("VZBRGITYUPSDNHLXAWMJQOFECK", 'Z', "V")
            };
            reflector = new Reflector("YRUHQSLDPXNGOKMIEBFZCWVJAT");
        }

        public void stepForward()
        {
            if (selectedRotors.Count != 3)
            {
                throw new Exception("Must have 3 rotors to rotate");
            }
            bool r1turnover = selectedRotors[0].inTurnover();
            bool r2turnover = selectedRotors[1].inTurnover();
            bool r3turnover = selectedRotors[2].inTurnover();

            selectedRotors[0].stepForward();
            if (r1turnover)
            {
                selectedRotors[1].stepForward();
            }
            if (r2turnover)
            {
                selectedRotors[2].stepForward();
            }
        }

        public char forwardTranslation(char c)
        {
            int position = (int)c - 65;

            foreach(Rotor rotor in rotors)
            {
                int ringPosition = (int)rotor.ringSetting - 65;
                int fMap = rotor.map[(26 + position + rotor.position - ringPosition) % 26];
                position = (position + fMap) % 26;
            }
            return validChars.ToCharArray()[position];
        }

        public char reverseTranslation(char c)
        {
            int position = (int)c - 65;

            foreach(Rotor rotor in rotors)
            {
                int ringPosition = (int)rotor.ringSetting - 65;
                int rMap = rotor.reverseMap[(26 + position + rotor.position - ringPosition) % 26];
                position = (position + rMap) % 26;
            }
            return validChars.ToCharArray()[position];
        }

        private char reflectorMap(char c)
        {
            int position = (int)c - 65;
            position = (position + reflector.map[position]) % 26;
            return validChars.ToCharArray()[position];
        }

        public void setSettings(char[] rings, char[] initialPos, List<string> rotorOrder)
        {
            if (rings.Length != 3 || initialPos.Length != 3 || rotorOrder.Count != 3)
            {
                throw new Exception("Array must consist of 3 items");
            }
            selectedRotors.Clear();

            foreach (string rotorName in rotorOrder)
            {
                Rotor rotor = rotors.Find(r => r.name == rotorName);
                selectedRotors.Add(rotor);
            }

            for (int i=0; i < selectedRotors.Count; i++)
            {
                selectedRotors[i].ringSetting = Char.ToUpper(rings[i]);
                selectedRotors[i].setDisplayChar(Char.ToUpper(initialPos[i]));
            }
        }

        private char encryptChar(char c)
        {
            stepForward();
            c = plugboard.getOppositeKey(c);
            c = forwardTranslation(c);
            c = reflectorMap(c);
            c = reverseTranslation(c);
            c = plugboard.getOppositeKey(c);
            return c;
        }

        public string execute(string msg)
        {
            string encrypted = "";
            msg = msg.ToUpper();

            foreach(char c in msg)
            {
                encrypted += encryptChar(c);
            }

            return encrypted;
        }

        public void addPlugs(List<Pair> pairs)
        {
            plugboard.pairs = pairs;
        }
    }
}
