using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class StringProcessingController : ControllerBase
{
    [HttpGet("process")]
    public IActionResult ProcessString([FromQuery] string input)
    {
        if (string.IsNullOrEmpty(input))
            return BadRequest(new { message = "Строка не должна быть пустой." });

        string reversed = ReverseString(input);
        Dictionary<char, int> charCounts = CountSymbol(input);
        string vowelSubstring = VowelVowel(input);
        string sortedString = new string(QuickSort(input.ToCharArray()));

        return Ok(new
        {
            original = input,
            reversed,
            charCounts,
            vowelSubstring,
            sortedString
        });
    }

    private static string ReverseString(string s)
    {
        char[] chars = s.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }

    private static Dictionary<char, int> CountSymbol(string symbol)
    {
        var dict = new Dictionary<char, int>();
        foreach (char c in symbol)
            dict[c] = dict.ContainsKey(c) ? dict[c] + 1 : 1;
        return dict;
    }

    private static string VowelVowel(string s)
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

    private static char[] QuickSort(char[] arr)
    {
        if (arr.Length <= 1) return arr;

        char pivot = arr[arr.Length / 2];
        var left = arr.Where(x => x < pivot).ToArray();
        var middle = arr.Where(x => x == pivot).ToArray();
        var right = arr.Where(x => x > pivot).ToArray();

        return QuickSort(left).Concat(middle).Concat(QuickSort(right)).ToArray();
    }
}
