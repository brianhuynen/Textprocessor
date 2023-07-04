using System.Text;

namespace TextProcessor
{
    public class WordCounter : IWordCountAnalyser
    {
        public WordCounter() { }
        /// <summary>
        /// Calculates highest word count in a given string of text
        /// </summary>
        /// <param name="inputText"></param>
        /// <returns>An integer representing the highest word count</returns>
        public int CalculateHighestWordCount(string inputText)
        {
            var wordList = ConvertTextToList(inputText);
            return CountAllWords(wordList).FirstOrDefault().Count;
        }
        /// <summary>
        /// Calculates the word count of an input word in a given string of text
        /// </summary>
        /// <param name="inputText"></param>
        /// <param name="word">A string representing the word to count its frequency of</param>
        /// <returns>An integer representing the word cound of the input word</returns>
        public int CalculateWordCount(string inputText, string word)
        {
            var wordList = ConvertTextToList(inputText);
            var result = CountAllWords(wordList).Find(e => e.Word.Equals(word.ToLower()));
            return result == null ? 0 : result.Count;
        }
        /// <summary>
        /// Gets the top N most counted words
        /// </summary>
        /// <param name="inputText"></param>
        /// <param name="limiter">An integer representing the top N most counted words</param>
        /// <returns></returns>
        public List<WordCountEntry> GetMostCountedWords(string inputText, int limiter)
        {
            var wordList = ConvertTextToList(inputText);
            List<WordCountEntry> result = CountAllWords(wordList).Take(limiter).ToList();
            return result;
        }
        /// <summary>
        /// Method that splits a string of text into a list of words
        /// </summary>
        /// <param name="inputText"></param>
        /// <returns></returns>
        public List<string> ConvertTextToList(string inputText)
        {
            //Trim the text to remove eventual unnecessary white spaces at the beginning or end of the text
            inputText = inputText.Trim();

            //Remove all symbols except apostrophes from text
            string processedText = RemoveSymbols(inputText);

            //Convert the text to a list of words
            var wordlist = processedText.Split(' ').ToList();

            //If there are multiple subsequent spaces within a text, empty entries can occur in the list
            wordlist.RemoveAll(EmptyEntry); //Remove all empty entries from the word list
            wordlist.RemoveAll(SymbolEntry); //Remove all entries that only contain a symbol

            //In case there are entries in the list such as 'word' or -word, etc.
            var convertedList = RemoveSymbolsAtBeginningOrEndOfEntry(wordlist);
            return convertedList;
        }
        /// <summary>
        /// Method that counts all words in a list of strings
        /// </summary>
        /// <param name="wordList"></param>
        /// <returns></returns>
        private List<WordCountEntry> CountAllWords(List<string> wordList)
        {
            List<WordCountEntry> wordCounts = new List<WordCountEntry>();
            foreach (string word in wordList)
            {
                //Checks if the wordcount list already has an entry using the word
                if (!wordCounts.Exists(e => e.Word.Equals(word))) //if it doesn't exist, add it to the list
                {
                    WordCountEntry wce = new WordCountEntry() { Word = word, Count = 1 };
                    wordCounts.Add(wce);
                }
                else
                {
                    wordCounts.Find(e => e.Word.Equals(word)).Count++; //if it does exist, increase its counter by 1
                }
            }
            return wordCounts.OrderByDescending(e => e.Count).ThenBy(e => e.Word).ToList();
        }
        /// <summary>
        /// Method that removes the symbols at the beginning or end of the word
        /// </summary>
        /// <param name="wordList"></param>
        /// <returns></returns>
        private List<string> RemoveSymbolsAtBeginningOrEndOfEntry(List<string> wordList)
        {
            List<string> newList = new List<string>();

            foreach (var entry in wordList)
            {
                var newEntry = entry;
                if (entry.First().Equals('\'') || entry.First().Equals('-'))
                    newEntry = newEntry.Remove(0, 1); //remove first entry if it is a symbol
                if (entry.Last().Equals('\'') || entry.Last().Equals('-'))
                    newEntry = newEntry.Remove(newEntry.Length - 1); //remove last entry if it is a symbol
                newList.Add(newEntry);
            }
            return newList;
        }

        private bool EmptyEntry(string e)
        {
            return e == "";
        }

        private bool SymbolEntry(string e)
        {
            return !e.Any(c => char.IsLetter(c));
        }
        /// <summary>
        /// Removes all symbols except apostrophes and hyphens in a text
        /// </summary>
        /// <param name="inputText"></param>
        /// <returns></returns>
        private string RemoveSymbols(string inputText)
        {
            var sb = new StringBuilder();
            foreach (char c in inputText)
            {
                //include apostrophes and hyphens
                if (c == '\'' || c == '-' || !char.IsPunctuation(c))
                    sb.Append(char.ToLower(c));
            }
            return sb.ToString();
        }
        /// <summary>
        /// Prints all entries of a list 
        /// </summary>
        public void PrintList<T>(IEnumerable<T> list)
        {
            foreach (var entry in list)
                Console.WriteLine(entry.ToString());
        }
    }
}
