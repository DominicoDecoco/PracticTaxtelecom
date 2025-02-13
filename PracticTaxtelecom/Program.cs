using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticTaxtelecom
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] array = { "a", "abcdefae", "abcdE", "FbcJEkf0", "ТМовыц", "fffjjj" };
            string validChars = "abcdefghijklmnopqrstuvwxyz";

            for (int i = 0; i < array.Length; i++)
            {
                string invalidChars = new string(array[i].ToString().Where(c => !validChars.Contains(c)).Distinct().ToArray());

                if (invalidChars.Length > 0)
                {
                    Console.WriteLine($"Неверные символы: {invalidChars}");
                }
                else if(array[i].Length % 2 == 0)
                {
                    string first = array[i].Substring(0, array[i].Length / 2);
                    string second = array[i].Substring(array[i].Length / 2);

                    Console.WriteLine($"{ReverseString(first) + ReverseString(second)}");
                    CountSymbol(array[i]);
                }
                else
                {
                    Console.WriteLine($"{ReverseString(array[i]) + array[i]}");
                    CountSymbol(array[i]);
                }
            }
            Console.ReadKey();
        }
        static string ReverseString(string s)
        {
            char[] chars = s.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }
        static void CountSymbol(string symbol)
        {
            Dictionary<char, int> dict = new Dictionary<char, int>();

            foreach (char c in symbol)
            {
                if (dict.ContainsKey(c))
                    dict[c]++;
                else dict[c] = 1;
            }
            Console.WriteLine("Символы: ");
            foreach (var c in dict)
            {
                Console.WriteLine($"{c.Key}: {c.Value}");
            }
        }
    }
}
