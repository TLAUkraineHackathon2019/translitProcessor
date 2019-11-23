using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace YC.Transliteration.Extensions
{
    internal static class TextExtensions
    {
        private static readonly Regex Whitespaces = new Regex("\\s",RegexOptions.Compiled);
        
        public static Dictionary<string, string> UppercaseDictionary(this Dictionary<string, string> lexiconMap)
        {

            IEnumerable<KeyValuePair<string, string>> ProcessDictionary()
            {
                foreach (var item in lexiconMap)
                {
                    yield return new KeyValuePair<string, string>(item.Key.Capitalize(), item.Value.Capitalize());
                }
            }

            return ProcessDictionary().ToDictionary(x => x.Key, x => x.Value);
        }
        
        public static Dictionary<decimal, string> PreprocessLexiconMap(this Dictionary<string, string> lexiconMap)
        {

            IEnumerable<KeyValuePair<decimal, string>> ProcessDictionary()
            {
                foreach (var item in lexiconMap)
                {
                    yield return new KeyValuePair<decimal, string>((decimal)item.Key.FirstOrDefault(), item.Value.Capitalize());
                }
            }

            return ProcessDictionary().ToDictionary(x => x.Key, x => x.Value);
        }

        public static bool IsUpper(this string text)
        {
            IEnumerable<bool> ProcessString()
            {
                foreach (var symbol in text)
                {
                    yield return Char.IsUpper(symbol) || Whitespaces.IsMatch(symbol.ToString());
                }
            }

            return ProcessString().All(x => x == true);
        }

        public static string Translate(this string text, Dictionary<decimal, string> dictionary)
        {
            StringBuilder strBuilder = new StringBuilder();
            foreach (char symbol in text)
            {
                dictionary.TryGetValue(Char.ToUpper(symbol), out string value);
                if (!Whitespaces.IsMatch(symbol.ToString()) && !string.IsNullOrEmpty(value))
                {
                   strBuilder.Append(Char.IsUpper(symbol) ? value?.Capitalize() : value?.ToLower());
                }
                else
                {
                    strBuilder.Append(symbol);
                }
            }
            return strBuilder.ToString();
        }
        
        public static string Capitalize(this string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": return string.Empty;//throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input.First().ToString().ToUpper() + (input.Length > 1 ? input.Substring(1) : string.Empty);
            }
        }
    }
}