namespace TextProcessor
{
    public class WordCountEntry : IWordCount
    {
        public int Count { get; set; }
        public string Word { get; set; }

        override public string ToString()
        {
            return "Word: " + Word + "\tCount: " + Count;
        }
    }
}
