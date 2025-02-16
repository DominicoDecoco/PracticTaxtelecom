using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    public class StringProcessor
    {
        public static string ReverseString(string s)
        {
            char[] chars = s.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        public static Dictionary<char, int> CountSymbol(string symbol)
        {
            var dict = new Dictionary<char, int>();
            foreach (char c in symbol)
                dict[c] = dict.ContainsKey(c) ? dict[c] + 1 : 1;
            return dict;
        }

        public static string VowelVowel(string s)
        {
            string vowels = "aeiouy";
            int maxLen = 0;
            string longestSubstring = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (vowels.Contains(s[i]))
                {
                    for (int j = i + 1; j < s.Length; j++)
                    {
                        if (vowels.Contains(s[j]))
                        {
                            string view = s.Substring(i, j - i + 1);
                            if (view.Length > maxLen)
                            {
                                maxLen = view.Length;
                                longestSubstring = view;
                            }
                        }
                    }
                }
            }
            return longestSubstring;
        }

        public static void QuickSort(char[] arr, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(arr, left, right);
                QuickSort(arr, left, pivot - 1);
                QuickSort(arr, pivot + 1, right);
            }
        }

        private static int Partition(char[] arr, int left, int right)
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

        public static char[] TreeSort(char[] arr)
        {
            TreeNode root = null;
            foreach (var item in arr)
                root = Insert(root, item);
            List<char> sortedList = new List<char>();
            InOrderTraversal(root, sortedList);
            return sortedList.ToArray();
        }

        private static TreeNode Insert(TreeNode node, char value)
        {
            if (node == null) return new TreeNode(value);
            if (value < node.Value) node.Left = Insert(node.Left, value);
            else node.Right = Insert(node.Right, value);
            return node;
        }

        private static void InOrderTraversal(TreeNode node, List<char> list)
        {
            if (node != null)
            {
                InOrderTraversal(node.Left, list);
                list.Add(node.Value);
                InOrderTraversal(node.Right, list);
            }
        }

        public static async Task<int> GetRandomNumber(int max)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string request = $"http://www.randomnumberapi.com/api/v1.0/random?min=0&max={max - 1}&count=1";
                    HttpResponseMessage response = await client.GetAsync(request);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    int[] numbers = JsonConvert.DeserializeObject<int[]>(responseBody);
                    return numbers?.Length > 0 ? numbers[0] : new Random().Next(0, max);
                }
                catch
                {
                    return new Random().Next(0, max);
                }
            }
        }
    }

    public class TreeNode
    {
        public char Value;
        public TreeNode Left, Right;
        public TreeNode(char value) => Value = value;
    }
}
