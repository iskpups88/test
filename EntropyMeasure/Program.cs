using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntropyMeasure
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\Искандер\Desktop\test.txt";
            string text;
            text = File.ReadAllText(path);
            var characters =
                text.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
            double H = 0;
            foreach (var character in characters)
            {
                double pi = character.Value / (double)text.Length;
                Console.WriteLine(character.Key + "= " + pi);
                H += pi * Math.Log(pi, 2);
            }
            H = -H;
            Console.WriteLine("Total entropy = " + H);
            Console.ReadKey();
        }
    }
}
