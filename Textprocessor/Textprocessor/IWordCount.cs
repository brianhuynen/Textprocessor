namespace TextProcessor
{
    public interface IWordCount
    {
        public string Word { get; set; }
        public int Count { get; set; }
        public string ToString();
    }
}
