namespace TestProject1
{
    public class Tests
    {
        [Test]
        public void ReverseString_ShouldReverseCorrectly()
        {
            Assert.That(StringProcessor.ReverseString("abc"), Is.EqualTo("cba"));
            Assert.That(StringProcessor.ReverseString("123456789"), Is.EqualTo("987654321"));
            Assert.That(StringProcessor.ReverseString(""), Is.EqualTo(""));
        }

        [Test]
        public void CountSymbol_ShouldCountCorrectly()
        {
            var result = StringProcessor.CountSymbol("aabbc");
            Assert.That(result['a'], Is.EqualTo(2));
            Assert.That(result['b'], Is.EqualTo(2));
            Assert.That(result['c'], Is.EqualTo(1));
        }

        [Test]
        public void VowelVowel_ShouldFindLongestVowelSubstring()
        {
            Assert.That(StringProcessor.VowelVowel("house"), Is.EqualTo("ouse"));
            Assert.That(StringProcessor.VowelVowel("bcdfg"), Is.EqualTo(""));
        }

        [Test]
        public void QuickSort_ShouldSortCorrectly()
        {
            char[] input = "dcba".ToCharArray();
            StringProcessor.QuickSort(input, 0, input.Length - 1);
            Assert.That(new string(input), Is.EqualTo("abcd"));
        }

        [Test]
        public void TreeSort_ShouldSortCorrectly()
        {
            char[] input = "dcba".ToCharArray();
            char[] sorted = StringProcessor.TreeSort(input);
            Assert.That(new string(sorted), Is.EqualTo("abcd"));
        }

        [Test]
        public async Task GetRandomNumber_ShouldReturnValidNumber()
        {
            int max = 10;
            int result = await StringProcessor.GetRandomNumber(max);
            Assert.IsTrue(result >= 0 && result < max);
        }
    }
}