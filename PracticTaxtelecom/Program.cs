using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PracticTaxtelecom
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string[] array = { "a", "abcdefae", "abcdE", "abckaf", "ТМовыц", "fffjjj" };
            string validChars = "abcdefghijklmnopqrstuvwxyz";

            Console.WriteLine("Выберите алгоритм сортировки: 1 - Быстрая сортировка, 2 - Сортировка деревом");
            int choice = int.Parse(Console.ReadLine());

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

                    char[] sortedArray = view.ToCharArray();
                    if (choice == 1)
                    {
                        QuickSort(sortedArray, 0, sortedArray.Length - 1);
                    }
                    else
                    {
                        sortedArray = TreeSort(sortedArray);
                    }
                    Console.WriteLine("Отсортированная строка: " + new string(sortedArray));

                    int randomIndex = await GetRandomNumber(sortedArray.Length);

                    if (randomIndex >= 0 && randomIndex < sortedArray.Length)
                    {
                        string stringMod = new string(sortedArray.Where((c, index) => index != randomIndex).ToArray());
                        Console.WriteLine($"Строка после удаления символа на позиции {randomIndex}: {stringMod}");
                    }

                }
                Console.WriteLine();
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
        //Запрос случайного числа API
        static async Task<int> GetRandomNumber(int max)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string request = "http://www.randomnumberapi.com/api/v1.0/random?min=0&max=" + (max - 1) + "&count=1";
                    HttpResponseMessage response = await client.GetAsync(request);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    int[] numbers = JsonConvert.DeserializeObject<int[]>(responseBody);
                    return numbers?.Length > 0 ? numbers[0] : new Random().Next(0, max);
                }
                catch
                {
                    Console.WriteLine("Ошибка при получении случайного числа из API. Получаем число средствами .NET.");
                    return new Random().Next(0, max);
                }
            }
        }
        //Сортировки
        static void QuickSort(char[] arr, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(arr, left, right);
                QuickSort(arr, left, pivot - 1);
                QuickSort(arr, pivot + 1, right);
            }
        }

        static int Partition(char[] arr, int left, int right)
        {
            char pivot = arr[right];
            int i = left - 1;
            for (int j = left; j < right; j++)
            {
                if (arr[j] < pivot)
                {
                    i++;
                    (arr[i], arr[j]) = (arr[j], arr[i]);
                }
            }
            (arr[i + 1], arr[right]) = (arr[right], arr[i + 1]);
            return i + 1;
        }

        class TreeNode
        {
            public char Value;
            public TreeNode Left, Right;
            public TreeNode(char value) => Value = value;
        }

        static char[] TreeSort(char[] arr)
        {
            TreeNode root = null;
            foreach (var item in arr)
                root = Insert(root, item);
            List<char> sortedList = new List<char>();
            InOrderTraversal(root, sortedList);
            return sortedList.ToArray();
        }

        static TreeNode Insert(TreeNode node, char value)
        {
            if (node == null) return new TreeNode(value);
            if (value < node.Value) node.Left = Insert(node.Left, value);
            else node.Right = Insert(node.Right, value);
            return node;
        }

        static void InOrderTraversal(TreeNode node, List<char> list)
        {
            if (node != null)
            {
                InOrderTraversal(node.Left, list);
                list.Add(node.Value);
                InOrderTraversal(node.Right, list);
            }
        }
    }
}
