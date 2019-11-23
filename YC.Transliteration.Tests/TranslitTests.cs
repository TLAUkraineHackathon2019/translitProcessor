using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using NUnit.Framework;
using YC.Transliteration.Config;
using YC.Transliteration.Models;

namespace TranslitTest
{
    public class Tests
    {
        private TranslitProcessor _tp { get; set; }
        private LexiconConfig _config;
        
        [SetUp]
        public void Setup()
        {
            string jsonBody = ReadManifestData("YC.Transliteration", "lexicon.json");
           _config= JsonConvert.DeserializeObject<LexiconConfig>(jsonBody);
           _tp = new TranslitProcessor();
        }

        [Test]
        public void UkrainianKMUTest()
        {
            var expected1 = _tp.Translit("Дмитро Згуровский", _config.UkrainianKMU);
            var expected2 = _tp.Translit("Дмитро ЗГуровский", _config.UkrainianKMU);
            var expected3 = _tp.Translit("Дмитро згуровский", _config.UkrainianKMU);
            var expected4 = _tp.Translit("Євген Петренко", _config.UkrainianKMU);
            var expected5 = _tp.Translit("Петренко Євген", _config.UkrainianKMU);
            var expected6 = _tp.Translit("Петренко.Євген", _config.UkrainianKMU);
            var expected7 = _tp.Translit("Петренко,Євген", _config.UkrainianKMU);
            var expected8 = _tp.Translit("Петренко/Євген", _config.UkrainianKMU);
            var expected9 = _tp.Translit("Євгєн", _config.UkrainianKMU);
            var expected10 = _tp.Translit("Яготин", _config.UkrainianKMU);
            var expected11 = _tp.Translit("Ярошенко", _config.UkrainianKMU);
            var expected12 = _tp.Translit("Костянтин", _config.UkrainianKMU); 
            var expected13 = _tp.Translit("Знам'янка", _config.UkrainianKMU); 
            var expected14 = _tp.Translit("Феодосія", _config.UkrainianKMU);
            var expected15 = _tp.Translit("Ньютон", _config.UkrainianKMU);
            var expected16 = _tp.Translit("піранья", _config.UkrainianKMU);
            var expected17 = _tp.Translit("кур'єр", _config.UkrainianKMU); 
            var expected18 = _tp.Translit("ЗГУРОВСЬКИЙ", _config.UkrainianKMU); 
            var expected19 = _tp.Translit("ЗГУРОВСЬКИЙ", _config.UkrainianKMU, false); 
            
            Assert.AreEqual("Dmytro Zghurovskyi", expected1);
            Assert.AreEqual("Dmytro ZGhurovskyi", expected2);
            Assert.AreEqual("Dmytro zghurovskyi", expected3);
            Assert.AreEqual("Yevhen Petrenko", expected4);
            Assert.AreEqual("Petrenko Yevhen", expected5);
            Assert.AreEqual("Petrenko.Yevhen", expected6);
            Assert.AreEqual("Petrenko,Yevhen", expected7);
            Assert.AreEqual("Petrenko/Yevhen", expected8);
            Assert.AreEqual("Yevhien", expected9);
            Assert.AreEqual("Yahotyn", expected10);
            Assert.AreEqual("Yaroshenko", expected11); 
            Assert.AreEqual("Kostiantyn", expected12);
            Assert.AreEqual("Znamianka", expected13);
            Assert.AreEqual("Feodosiia", expected14);
            Assert.AreEqual("Niuton", expected15);
            Assert.AreEqual("pirania", expected16);
            Assert.AreEqual("kurier", expected17);
            Assert.AreEqual("ZGHUROVSKYI", expected18);
            Assert.AreEqual("ZGhUROVSKYI", expected19);
        }

        [Test]
        public void UkrainianSimpleTest()
        {
            var expected = _tp.Translit("Дмитро Згуровский", _config.UkrainianSimple);
            
            Assert.AreEqual("Dmytro Zhurovskyj", expected);
        }

        [Test]
        public void UkrainianWWSTest()
        {
            var expected = _tp.Translit("Дмитро Щуровский", _config.UkrainianWWS);
            
            Assert.AreEqual("Dmytro Ščurovskyj", expected);
        }
        
        [Test]
        public void UkrainianBritishTest()
        {
            var expected = _tp.Translit("Дмитро Щуровский", _config.UkrainianBritish);
            
            Assert.AreEqual("Dmȳtro Shchurovskȳĭ", expected);
        }
        
        [Test]
        public void UkrainianFrenchTest()
        {
            var expected = _tp.Translit("Дмитро Щуровский", _config.UkrainianFrench);
            
            Assert.AreEqual("Dmytro Chtchourovskyy", expected);
        }
        [Test]
        public void UkrainianGermanTest()
        {
            var expected = _tp.Translit("Дмитро Щуровский", _config.UkrainianGerman);
            
            Assert.AreEqual("Dmytro Schtschurowskyj", expected);
        }
        
        [Test]
        public void UkrainianGOST1971Test()
        {
            var expected = _tp.Translit("Дмитро Щуровский", _config.UkrainianGOST1971);
            
            Assert.AreEqual("Dmitro Shhurovskij", expected);
        }
        
        [Test]
        public void RussianInternationalPassport1997Test()
        {
            var expected1 = _tp.Translit("Варенье", _config.RussianInternationalPassport1997);
            var expected2 = _tp.Translit("Новьё", _config.RussianInternationalPassport1997);
            var expected3 = _tp.Translit("Красный", _config.RussianInternationalPassport1997);
            var expected4 = _tp.Translit("Полоний", _config.RussianInternationalPassport1997);
            
            Assert.AreEqual("Varen'ye", expected1);
            Assert.AreEqual("Nov'ye", expected2);
            Assert.AreEqual("Krasnyy", expected3);
            Assert.AreEqual("Poloniy", expected4);
        }
        
        [Test]
        public void RussianInternationalPassport1997ReducedTest()
        {
            var expected1 = _tp.Translit("Красный", _config.RussianInternationalPassport1997Reduced);
            var expected2 = _tp.Translit("Полоний", _config.RussianInternationalPassport1997Reduced);
            
            Assert.AreEqual("Krasny", expected1);
            Assert.AreEqual("Polony", expected2);
        }
        
        [Test]
        public void RussianDriverLicenseReducedTest()
        {
            var expected1 = _tp.Translit("Варенье", _config.RussianDriverLicense);
            var expected2 = _tp.Translit("Подъезд", _config.RussianDriverLicense);
            var expected3 = _tp.Translit("Новьё", _config.RussianDriverLicense);
            var expected4 = _tp.Translit("Подъёб", _config.RussianDriverLicense);
            var expected5 = _tp.Translit("Ель", _config.RussianDriverLicense);
            var expected6 = _tp.Translit("Ёж", _config.RussianDriverLicense);
            var expected7 = _tp.Translit("Щёки", _config.RussianDriverLicense);
            var expected8 = _tp.Translit("Соловьи", _config.RussianDriverLicense);
            
            Assert.AreEqual("Varen'ye", expected1);
            Assert.AreEqual("Pod'yezd", expected2);
            Assert.AreEqual("Nov'yo", expected3);
            Assert.AreEqual("Pod'yob", expected4);
            Assert.AreEqual("Yel'", expected5);
            Assert.AreEqual("Yozh", expected6);
            Assert.AreEqual("Shcheki", expected7);
            Assert.AreEqual("Solov'yi", expected8);
        }
        
        
        [Test]
        public void RussianISO9SystemBTest()
        {
            var expected1 = _tp.Translit("Цёмки", _config.RussianISO9SystemB);
            var expected2 = _tp.Translit("Цыц", _config.RussianISO9SystemB);
            
            Assert.AreEqual("Cyomki", expected1);
            Assert.AreEqual("Cy'cz", expected2);
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