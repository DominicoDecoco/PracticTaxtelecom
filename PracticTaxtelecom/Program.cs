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
            string[] array = { "a", "abcdefae", "abcdE", "abckaf", "ТМовыц", "fffjjj" };
            string validChars = "abcdefghijklmnopqrstuvwxyz";

            for (int i = 0; i < array.Length; i++)
            {
                string invalidChars = new string(array[i].ToString().Where(c => !validChars.Contains(c)).Distinct().ToArray());
                if (invalidChars.Length > 0)
                {
                    Console.WriteLine($"Неверные символы: {invalidChars}");
                }
                else
                {
                    string view;
                    if (array[i].Length % 2 == 0)
                    {
                        string first = array[i].Substring(0, array[i].Length / 2);
                        string second = array[i].Substring(array[i].Length / 2);
                        view = ReverseString(first) + ReverseString(second);
                    }
                    else
                    {
                        view = ReverseString(array[i]) + array[i];
                        
                    }
                    Console.WriteLine(view);
                    CountSymbol(view);

                    string VowelMax = VowelVowel(ReverseString(view));
                    if(!string.IsNullOrEmpty(VowelMax))
                    {
                        Console.WriteLine($"Наибольшая подстрока на гласные: {VowelMax}");
                    }
                    else
                    {
                        Console.WriteLine("Гласная подстрока не найдена.");
                    }
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
        static string VowelVowel(string s)
        {
            string vowels = "aeiouy";
            int maxLen = 0;
            string longestsubstring = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (vowels.Contains(s[i]))
                {
                    for (int j = i+1; j< s.Length; j++)
                    {
                        if (vowels.Contains(s[j]))
                        {
                            string view = s.Substring(i, j- i + 1);
                            if(view.Length > maxLen)
                            {
                                maxLen = view.Length;
                                longestsubstring = view;
                            }
                        }
                    }
                }
            }
            return longestsubstring;
        }
    }
}
