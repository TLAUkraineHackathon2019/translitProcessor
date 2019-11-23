using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using YC.Transliteration.Config;
using YC.Transliteration.Extensions;

namespace YC.Transliteration.Models
{
    public class TranslitProcessor: ITranslit
    {
        private static LexiconConfig _config { get; set; }
        static TranslitProcessor()
        {
            string jsonBody = ReadManifestData("YC.Transliteration", "lexicon.json");
            _config= JsonConvert.DeserializeObject<LexiconConfig>(jsonBody);
        }
        public string Translit(string inputStr, LexiconRules rules, bool preserveCase = true)
        {
            var text = inputStr.ToString();
            var isUpperCase = inputStr.IsUpper();

            if (rules.DeleteCasesPattern != null)
            {
                text = rules.DeleteCasesPattern.Replace(text, "");
            }
            if (rules.SpecialCasesPattern != null)
            {
                var match = rules.SpecialCasesPattern.Match(text);
                foreach (var group in match.Groups)
                {
                    rules.SpecialCases.UppercaseDictionary().TryGetValue(group.ToString(), out string value);
                    if (string.IsNullOrEmpty(value))
                    {
                        rules.SpecialCases.TryGetValue(group.ToString(), out value);
                    }
                    if (!string.IsNullOrEmpty(value))
                    {
                        text = new Regex($"{group.ToString()}", RegexOptions.Compiled).Replace(text, value);
                    }
                }
            }
            if (rules.FirstCharsPattern != null)
            {
                var match = rules.FirstCharsPattern.Match(text);
                foreach (var group in match.Groups)
                {
                    rules.FirstCharacters.UppercaseDictionary().TryGetValue(group.ToString(), out string value);
                    if (!string.IsNullOrEmpty(value))
                    {
                        text = new Regex($"{group.ToString()}", RegexOptions.Compiled).Replace(text, value);
                    }
                }
            }

            text = text.Translate(rules.Dictionary.UppercaseDictionary().PreprocessLexiconMap());
            if (isUpperCase && preserveCase)
            {
                return text.ToUpper();
            }
            else
            {
                return text;
            }
        }

        public string Translit(string inputStr, Rule lexiconRule, bool preserveCase = true)
        {
            switch (lexiconRule)
            {
                case Rule.UkrainianKMU:
                    return Translit(inputStr, _config.UkrainianKMU, preserveCase);
                case Rule.UkrainianSimple:
                    return Translit(inputStr, _config.UkrainianSimple, preserveCase);
                case Rule.RussianSimple:
                    return Translit(inputStr, _config.RussianSimple, preserveCase);
                case Rule.UkrainianWWS:
                    return Translit(inputStr, _config.UkrainianWWS, preserveCase);
                case Rule.RussianGOST2006:
                    return Translit(inputStr, _config.RussianGOST2006, preserveCase);
                case Rule.UkrainianBritish:
                    return Translit(inputStr, _config.UkrainianBritish, preserveCase);
                case Rule.UkrainianBGN:
                    return Translit(inputStr, _config.UkrainianBGN, preserveCase);
                case Rule.UkrainianISO9:
                    return Translit(inputStr, _config.UkrainianISO9, preserveCase);
                case Rule.UkrainianFrench:
                    return Translit(inputStr, _config.UkrainianFrench, preserveCase);
                case Rule.UkrainianGerman:
                    return Translit(inputStr, _config.UkrainianGerman, preserveCase);
                case Rule.UkrainianGOST1971:
                    return Translit(inputStr, _config.UkrainianGOST1971, preserveCase);
                case Rule.UkrainianGOST1986:
                    return Translit(inputStr, _config.UkrainianGOST1986, preserveCase);
                case Rule.UkrainianPassport2007:
                    return Translit(inputStr, _config.UkrainianPassport2007, preserveCase);
                case Rule.UkrainianNational1996:
                    return Translit(inputStr, _config.UkrainianNational1996, preserveCase);
                case Rule.UkrainianPassport2004Alt:
                    return Translit(inputStr, _config.UkrainianPassport2004Alt, preserveCase);
                case Rule.RussianICAO:
                    return Translit(inputStr, _config.RussianICAO, preserveCase);
                case Rule.RussianISOR9Table2:
                    return Translit(inputStr, _config.RussianISOR9Table2, preserveCase);
                case Rule.RussianTelegram:
                    return Translit(inputStr, _config.RussianTelegram, preserveCase);
                case Rule.RussianISO9SystemA:
                    return Translit(inputStr, _config.RussianISO9SystemA, preserveCase);
                case Rule.RussianISO9SystemB:
                    return Translit(inputStr, _config.RussianISO9SystemB, preserveCase);
                case Rule.RussianInternationalPassport1997:
                    return Translit(inputStr, _config.RussianInternationalPassport1997, preserveCase);
                case Rule.RussianInternationalPassport1997Reduced:
                    return Translit(inputStr, _config.RussianInternationalPassport1997Reduced, preserveCase);
                case Rule.RussianDriverLicense:
                    return Translit(inputStr, _config.RussianDriverLicense, preserveCase);
                default:
                    throw new ArgumentOutOfRangeException(nameof(lexiconRule), lexiconRule, null);
            }
        }
        
        public IEnumerable<string> Translit(string inputStr, RuleSet ruleSet, bool preserveCase = true)
        {
            switch (ruleSet)
            {
                case RuleSet.UkrRules:
                    return UkrResults(inputStr, preserveCase);
                case RuleSet.RusRules:
                    return RusResults(inputStr, preserveCase);
                case RuleSet.UkrToFrench:
                    return FrenchResults(inputStr, preserveCase);
                case RuleSet.UkrToGerman:
                    return GermanResults(inputStr, preserveCase);
                case RuleSet.All:
                    var result = GermanResults(inputStr, preserveCase);
                    result.Concat(FrenchResults(inputStr, preserveCase));
                    result.Concat(RusResults(inputStr, preserveCase));
                    result.Concat(UkrResults(inputStr, preserveCase));
                    return result;
            }

            return new string[] {};
        }
        
        private IEnumerable<string> GermanResults(string inputStr, bool preserveCase = false)
        {
            yield return Translit(inputStr, _config.UkrainianGerman, preserveCase);
        }
        private IEnumerable<string> FrenchResults(string inputStr, bool preserveCase = false)
        {
            yield return Translit(inputStr, _config.UkrainianFrench, preserveCase);
        }
        private IEnumerable<string> UkrResults(string inputStr, bool preserveCase = false)
        {
            yield return Translit(inputStr, _config.UkrainianKMU, preserveCase);
            yield return Translit(inputStr, _config.UkrainianSimple, preserveCase);
            yield return Translit(inputStr, _config.UkrainianWWS, preserveCase);
            yield return Translit(inputStr, _config.UkrainianBritish, preserveCase);
            yield return Translit(inputStr, _config.UkrainianBGN, preserveCase);
            yield return Translit(inputStr, _config.UkrainianISO9, preserveCase);
            yield return Translit(inputStr, _config.UkrainianGOST1971, preserveCase);
            yield return Translit(inputStr, _config.UkrainianFrench, preserveCase);
            yield return Translit(inputStr, _config.UkrainianGerman, preserveCase);
            yield return Translit(inputStr, _config.UkrainianPassport2007, preserveCase);
            yield return Translit(inputStr, _config.UkrainianGOST1986, preserveCase);
            yield return Translit(inputStr, _config.UkrainianNational1996, preserveCase);
            yield return Translit(inputStr, _config.UkrainianPassport2004Alt, preserveCase);
        }
        private IEnumerable<string> RusResults(string inputStr, bool preserveCase = false)
        {
            yield return Translit(inputStr, _config.RussianSimple, preserveCase);
            yield return Translit(inputStr, _config.RussianTelegram, preserveCase);
            yield return Translit(inputStr, _config.RussianDriverLicense, preserveCase);
            yield return Translit(inputStr, _config.RussianInternationalPassport1997, preserveCase);
            yield return Translit(inputStr, _config.RussianInternationalPassport1997Reduced, preserveCase);
            yield return Translit(inputStr, _config.RussianGOST2006, preserveCase);
            yield return Translit(inputStr, _config.RussianICAO, preserveCase);
            yield return Translit(inputStr, _config.RussianISO9SystemA, preserveCase);
            yield return Translit(inputStr, _config.RussianISO9SystemB, preserveCase);
            yield return Translit(inputStr, _config.RussianISOR9Table2, preserveCase);
        }
        private static string ReadManifestData(string assemblyName, string embeddedFileName)
        {
            var assembly = GetAssemblyByName(assemblyName);
            var resourceName = assembly.GetManifestResourceNames().First(s => s.EndsWith(embeddedFileName,StringComparison.CurrentCultureIgnoreCase));

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException("Could not load manifest resource stream.");
                }
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        
        private static Assembly GetAssemblyByName(string name)
        {
            return AppDomain.CurrentDomain.GetAssemblies().
                SingleOrDefault(assembly => assembly.GetName().Name == name);
        }
    }
}
