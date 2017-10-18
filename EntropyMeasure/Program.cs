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
            computeAll(computeSymbol(@"C:\Users\Искандер\Desktop\2.txt"), computeCouple(@"C:\Users\Искандер\Desktop\2.txt"));
            Console.ReadKey();
        }
        static Dictionary<char, double> computeSymbol(string a)
        {
            string text = File.ReadAllText(a);
            Dictionary<char, int> symbols = text.GroupBy(c => c).ToDictionary(q => q.Key, q => q.Count());
            Dictionary<char, double> symbols2 = new Dictionary<char, double>();
            double H = 0;
            foreach (var symbol in symbols)
            {

                double p = symbol.Value / (double)text.Length;
                H += p * Math.Log(p, 2);
                symbols2.Add(symbol.Key, p);
            }
            Console.WriteLine(H);
            return symbols2;
        }

        static Dictionary<string, double> computeCouple(string a)
        {
            string text = File.ReadAllText(a);
            string couple = "";
            Dictionary<string, int> payrs = new Dictionary<string, int>();
            for (int i = 0; i < text.Length - 1; i++)
            {
                couple = (text[i].ToString() + text[i + 1].ToString());

                //Dictionary<string, int> symbols = couple.GroupBy(c => c).ToDictionary(q => q.Key, q => q.Count());
                if (payrs.ContainsKey(couple))
                {
                    payrs[couple] += 1;
                    //b++;
                    //payrs.Add(couple, b);
                }
                else
                    payrs.Add(couple, 1);
                couple = "";
            }
            Dictionary<string, double> payrsP = new Dictionary<string, double>();
            foreach (var item in payrs)
            {
                double p = (double)item.Value / (double)(text.Length - 1);
                payrsP.Add(item.Key, p);
            }
            return payrsP;
            //Dictionary<IList<string>, int> payrs = strings.GroupBy(c => c).ToDictionary((q => q.Key, q => q.Count()));
        }
        static void computeAll(Dictionary<char, double> symbols, Dictionary<string, double> pairs)
        {
            double p = 0;

            Dictionary<string, double> condP = new Dictionary<string, double>();
            double H = 0;
            //for (int i = 0; i < symbols.Count; i++)
            //{
            //    for (int j = 0; j < symbols.Count; j++)
            //    {
            //        var listFirstCharPi = pairs.Where(s => s.Key[0] == symbols.Keys.ElementAt(i)).Select(x => x.Key).ToList();

            //    }
            //}
            foreach (var pair in pairs)
            {
                foreach (var symbol in symbols)
                {
                    if (pair.Key[0] == symbol.Key)
                    {
                        p = pair.Value / symbol.Value;
                        condP.Add(pair.Key, p);
                        break;                 
                    }
                }
            }
            foreach (var cond in condP)
            {
                foreach (var pair in pairs)
                {
                    if (cond.Key == pair.Key)
                    {
                        H += pair.Value * Math.Log(cond.Value, 2);
                        break;
                    }
                }

            }
            Console.WriteLine(H);
        }
    }
}
