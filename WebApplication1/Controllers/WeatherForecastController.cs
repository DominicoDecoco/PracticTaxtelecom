using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class StringProcessingController : ControllerBase
{
    private readonly IConfiguration _configuration;

    private static int _currentRequests = 0;
    private static SemaphoreSlim _semaphore;

    public StringProcessingController(IConfiguration configuration)
    {
        _configuration = configuration;
        int parallelLimit = _configuration.GetValue<int>("Settings:ParallelLimit", 10); // Значение по умолчанию 10
        _semaphore = new SemaphoreSlim(parallelLimit, parallelLimit);
    }

    [HttpGet("process")]
    public IActionResult ProcessString([FromQuery] string input)
    {
        if (string.IsNullOrEmpty(input))
            return BadRequest(new { message = "Строка не должна быть пустой." });

        if (!_semaphore.Wait(0))
            return StatusCode(503, new { message = "Сервис перегружен. Попробуйте позже." });

        Interlocked.Increment(ref _currentRequests);
        try
        {
            var blackList = _configuration.GetSection("Settings:BlackList").Get<List<string>>() ?? new List<string>();

            if (blackList.Any(word => input.Contains(word)))
                return BadRequest(new { message = "Введённая строка содержит запрещённое слово." });

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
        finally
        {
            Interlocked.Decrement(ref _currentRequests);
            _semaphore.Release();
        }
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
