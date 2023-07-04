using TextProcessor;
using Xunit.Abstractions;

namespace TextprocessorTests
{
    public class TextProcessorTests
    {
        WordCounter wc = new();
        private readonly ITestOutputHelper output;

        public TextProcessorTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void CheckHandlingOf_Apostrophes_WhenConverting_ToWordList1()
        {
            string input = "'every' 'word' 'is' 'enclosed' 'in' 'apostrophes'";
            List<string> expected = new List<string>()
            {
                "every",
                "word",
                "is",
                "enclosed",
                "in",
                "apostrophes"
            };
            List<string> actual = wc.ConvertTextToList(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CheckHandlingOf_Apostrophes_WhenConverting_ToWordList2()
        {
            string input = "'Contractions' 'shouldn't' 'be' 'removed'";
            List<string> expected = new List<string>()
            {
                "contractions",
                "shouldn't",
                "be",
                "removed"
            };
            List<string> actual = wc.ConvertTextToList(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CheckHandlingOf_Hyphens_WhenConverting_ToWordList() 
        {
            string input = "Dash-like punctuation should be preserved as well";
            List<string> expected = new List<string>()
            {
                "dash-like",
                "punctuation",
                "should",
                "be",
                "preserved",
                "as",
                "well"
            };
            List<string> actual = wc.ConvertTextToList(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CheckHandlingOf_Interpunction_WhenConverting_ToWordList1()
        {
            string input = "Interpunction: Shouldn't be included. There are exceptions, such as hyphens and apostrophes used in hyphenations and contractions";
            List<string> expected = new List<string>()
            {
                "interpunction", 
                "shouldn't", 
                "be", 
                "included",
                "there",
                "are",
                "exceptions", 
                "such", 
                "as", 
                "hyphens", 
                "and", 
                "apostrophes", 
                "used", 
                "in", 
                "hyphenations", 
                "and", 
                "contractions"
            };
            List<string> actual = wc.ConvertTextToList(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CheckHandlingOf_Interpunction_WhenConverting_ToWordList2()
        {
            string input = "All. Interpunction, should' be? removed! in: the; \"list\" in-ten tion'ally b-r-o-k-e-n t'e'x't ";
            List<string> expected = new List<string>()
            {
                "all",
                "interpunction",
                "should",
                "be",
                "removed",
                "in",
                "the",
                "list",
                "in-ten",
                "tion'ally",
                "b-r-o-k-e-n",
                "t'e'x't"
            };
            List<string> actual = wc.ConvertTextToList(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void HighestWordCountTest1()
        {
            string input = "A a Aa aA B B b c";
            int expected = 3;
            int actual = wc.CalculateHighestWordCount(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void HighestWordCountTest2()
        {
            string input = "A 'a' Aa aA B -B- b c";
            int expected = 3;
            int actual = wc.CalculateHighestWordCount(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void HighestWordCountTest3()
        {
            string input = "A'a A-a Aa aA B -B- b c";
            int expected = 3;
            int actual = wc.CalculateHighestWordCount(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WordCountTest()
        {
            string input = "The quick brown fox jumps over the lazy dog";
            string searchWord = "the";
            int expected = 2;
            int actual = wc.CalculateWordCount(input, searchWord);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WordCountTest_IsCaseInsensitive()
        {
            string input = "The quick brown fox jumps over the lazy dog";
            string searchWord = "The";
            int expected = 2;
            int actual = wc.CalculateWordCount(input, searchWord);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WordCountTest_IsCaseInsensitive2()
        {
            string input = "THe quick brown fox jumps over thE lazy dog";
            string searchWord = "the";
            int expected = 2;
            int actual = wc.CalculateWordCount(input, searchWord);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MostCountedWordsTest1()
        {
            string input = "This is The tHe thE THE tExT TEXT texT Test test text";
            int limiter = 3;
            List<WordCountEntry> expected = new()
            {
                new WordCountEntry() { Word = "text", Count = 4 },
                new WordCountEntry() { Word = "the", Count = 4 },
                new WordCountEntry() { Word = "test", Count = 2 }
            };
            List<WordCountEntry> actual = wc.GetMostCountedWords(input, limiter);
            Assert.Equivalent(expected, actual, true);
        }

        [Fact]
        public void MostCountedWordsTest2()
        {
            string input = "The car in the garage should be repaired";
            int limiter = 3;
            List<WordCountEntry> expected = new()
            {
                new WordCountEntry() { Word = "the", Count = 2 },
                new WordCountEntry() { Word = "be", Count = 1 },
                new WordCountEntry() { Word = "car", Count = 1 }
            };
            List<WordCountEntry> actual = wc.GetMostCountedWords(input, limiter);
            Assert.Equivalent(expected, actual, true);
        }

    }
}