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
            string[] array = { "a", "abcdef", "abcde" };

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Length % 2 == 0)
                {
                    string first = array[i].Substring(0, array[i].Length / 2);
                    string second = array[i].Substring(array[i].Length / 2);

                    Console.WriteLine($"{ReverseString(first) + ReverseString(second)}");
                }
                else
                {
                    Console.WriteLine($"{ReverseString(array[i]) + array[i]}");
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
    }
}
