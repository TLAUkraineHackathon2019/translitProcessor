# translitProcessor

This basic for UA to EN, RU to EN transliteration library. File ITranslit.cs contains three ways to perform translitertion.
1) Translit(text, CONFIG_OBJECT) // returns one transformed string according to passed configuration
2) Translit(text, RuleSet.UkrRules) // returns a an array of transformed strings for a rule sets(configs for the rules are loading in the background)
3) Translit(text, Rule.UkrainianKMU) // returns one transformed string according to passed rule enum(config for the rule loads in the background)
