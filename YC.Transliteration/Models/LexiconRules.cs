using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using YC.Transliteration.Extensions;

namespace YC.Transliteration.Models
{
    public class LexiconRules
    {
        public Dictionary<string, string> Dictionary { get; set; }
        public string[] DeleteCases { get; set; }
        public Dictionary<string, string> SpecialCases { get; set; }
        public Dictionary<string, string> FirstCharacters { get; set; }
        public Regex SpecialCasesPattern
        {
            get
            {
                if (SpecialCases != null)
                {
                    var keys = (SpecialCases.UppercaseDictionary().Keys.Concat(SpecialCases.Keys)).Distinct();
                    return new Regex($"{string.Join("|", keys)}", RegexOptions.Compiled);
                }
                
                return null;
            }
        }

        public Regex FirstCharsPattern
        {
            get
            {
                if (FirstCharacters != null)
                {
                    return new Regex($"\\b({string.Join("|", FirstCharacters.UppercaseDictionary().Keys)})", RegexOptions.Compiled);
                }

                return null;
            }
        }

        public Regex DeleteCasesPattern
        {
            get
            {
                if (DeleteCases != null)
                {
                    return new Regex($"{string.Join("|", DeleteCases)}", RegexOptions.Compiled);
                }

                return null;
            }
        }
    }
}