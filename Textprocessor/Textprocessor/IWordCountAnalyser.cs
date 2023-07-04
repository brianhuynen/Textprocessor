namespace TextProcessor
{
    public interface IWordCountAnalyser
    {
        public int CalculateHighestWordCount(string inputText);
        public int CalculateWordCount(string inputText, string word);
        public List<WordCountEntry> GetMostCountedWords(string inputText, int limiter);
    }
}
