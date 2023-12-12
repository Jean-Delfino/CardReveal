using System.Text.RegularExpressions;
using TMPro;

namespace Reuse.Utils
{
    public static class UtilStrings
    {
        private static Regex upperCaseBroker = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);

        public static string BreakStringBasedOnUpperCases(string severalUpperCasePhrase)
        {
            return upperCaseBroker.Replace(severalUpperCasePhrase, " ");
        }
        
        public static void AddTextAndBreakLine(TextMeshProUGUI text, string str)
        {
            text.text += str + "\n";
        }
        
        public static string ConvertPositiveNumberToFixedSize(int number, int size)
        {
            return number.ToString().PadLeft(size, '0');
        }
        
        public static string RandomSubString(string origin, int lenghtCut, int min, int max){
            var position = UtilRandom.RandomGenerator.Next(min, max);
            return origin.Substring(position, lenghtCut);
        }
        
        public static string ConvertToString(string[] phrase){
            string res = ""; 
            int i;
        
            for(i = 0; i < phrase.Length ; i++){
                res += phrase[i];
            }

            return res;
        }
    }
}