using System.Collections.Generic;

namespace YC.Transliteration.Models
{
    public interface ITranslit
    {
        string Translit(string inputStr, LexiconRules rules, bool preserveCase = true);
        string Translit(string inputStr, Rule lexiconRule, bool preserveCase = true);
        IEnumerable<string> Translit(string inputStr, RuleSet ruleSet, bool preserveCase = true);
    }
}
