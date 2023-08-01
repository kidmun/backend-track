using System.Text.RegularExpressions;
static bool IsPalindrome(string str)
    {
        string newStr = Regex.Replace(str, @"[\W_]", "").ToLower();
        for (int i = 0; i < newStr.Length / 2; i++)
        {
            if (newStr[i] != newStr[newStr.Length - i - 1])
            {
                return false;
            }
        }

        return true;
    }
Console.WriteLine(IsPalindrome("aba   !"));
