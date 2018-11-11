using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            //Enigma enigma = new Enigma();
            //char[] rings = { 'A', 'A', 'A' };
            //char[] ground = { 'F', 'D', 'V' };
            //List<string> rotorOrder = new List<string>()
            //{
            //    "III",
            //    "II",
            //    "I"
            //};
            //enigma.setSettings(rings, ground, rotorOrder);
            //string msg = enigma.execute("ENIGMA");

            //Console.WriteLine(msg);
            string msg = new Enigma_Emulator.EnigmaMachine().runEnigma("test");
            Console.WriteLine(msg);
        }
    }
}
