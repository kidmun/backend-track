static Dictionary<string, int> counter(string inputWord){
    string[] words = inputWord.Split(' ');
    char punctuation = '!';
    Dictionary<string, int> count = new Dictionary<string, int>();
    foreach (string word in words)
        {
           string newWord = word.ToLower().Replace(punctuation.ToString(), "");
           if (newWord.Length > 0){
             if (count.ContainsKey(newWord))
            {
                count[newWord]++;
            }
            else
            {
                count[newWord] = 1;
            }
           }
            
        }
    
return count;
}
Dictionary<string, int> ans = counter("this ! is for the test kidus KIDUS");
foreach (KeyValuePair<string, int> entry in ans)
{
    string word = entry.Key;
    double value = entry.Value;
    Console.WriteLine($"{word}: {value}");
}

