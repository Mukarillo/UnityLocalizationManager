using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;

namespace LManager
{
    public class OnLocalizationChanged : UnityEvent<Locale>{}

    public class LocalizationManager
    {
        public const char PARAMETER_DELIMITER = '@';
        private const string PLAYER_PREFS_LANGUAGE_KEY = "_currentLanguageCode";

        public static LocalizationManager Instance = new LocalizationManager();
        public OnLocalizationChanged onLocalizationChanged { get; set; } = new OnLocalizationChanged();
        public Locale CurrentLocale { get; private set; }

        private readonly Dictionary<string, string> mLanguageDictionary = new Dictionary<string, string>();
        private string mFilePath;

        public LocalizationManager(string filePath = "Assets/Resources/Localization.csv")
        {
            CurrentLocale = Locale.Get(PlayerPrefs.GetString(PLAYER_PREFS_LANGUAGE_KEY, LManager.LocaleCode.GetLocaleCode().code));
            ChangeReferenceFile(filePath); 
        }

        public void ChangeReferenceFile(string filePath)
        {
            mFilePath = filePath;
            
            ChangeLanguage(CurrentLocale);
        }

        public void ResetLanguageToDeviceLanguage() => ChangeLanguage(LManager.LocaleCode.GetLocaleCode());

        public void ChangeLanguage(string code) => ChangeLanguage(Locale.Get(code));
        public void ChangeLanguage(Locale locale)
        {
            var fileLines = File.ReadAllLines(mFilePath);

            var lIndex = fileLines[0].Split(',').ToList().FindIndex((c) => c.ToLower().Equals(locale.code.ToLower()));
            var CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            for (var i = 1; i < fileLines.Length; i++)
            {
                var line = CSVParser.Split(fileLines[i]);
                var key = line[0];
                var value = line[lIndex].Replace("\"", "");

                if (mLanguageDictionary.ContainsKey(key))
                    mLanguageDictionary[key] = value;
                else
                    mLanguageDictionary.Add(key, value);
            }

            CurrentLocale = locale;
            PlayerPrefs.SetString(PLAYER_PREFS_LANGUAGE_KEY, CurrentLocale.code);
            onLocalizationChanged?.Invoke(locale);
        }

        public bool Exists(string key) => mLanguageDictionary.ContainsKey(key);
        
        public string Get(string key, Dictionary<string, string> replaces = null)
        {
            var value = "";
            if (!TryGet(key, out value, replaces))
                Debug.LogWarning($"Key {key} is not present in {mFilePath} file.");

            return value;
        }

        public string GetUpper(string key, Dictionary<string, string> replaces = null) => Get(key, replaces).ToUpper();
        public string GetLower(string key, Dictionary<string, string> replaces = null) => Get(key, replaces).ToLower();
        public string GetFirstLetterUpper(string key, Dictionary<string, string> replaces = null)
        {
            var result = Get(key, replaces);
            return result.First().ToString().ToUpper() + result.Substring(1);
        }

        public bool TryGet(string key, out string result, Dictionary<string, string> replaces = null)
        {
            var hasValue = mLanguageDictionary.TryGetValue(key, out result);
            if(hasValue && result.Contains(PARAMETER_DELIMITER) && replaces != null)
            {
                var elements = result.Split(PARAMETER_DELIMITER);
                result = elements[0];
                for (var i = 1; i < elements.Length; i++)
                {
                    var replace = elements[i];
                    if (i % 2 != 0)
                        replaces.TryGetValue(replace, out replace);

                    result += replace;
                }
            }

            return hasValue;
        }

        public string FormatDate(DateTime dt) => CurrentLocale.FormatDate(dt);
        public string FormatTime(DateTime dt) => CurrentLocale.FormatTime(dt);
        public string FormatCurrency(double value) => CurrentLocale.FormatCurrency(value);
        public string FormatNumber(double value, int decimals) => CurrentLocale.FormatNumber(value, decimals);
        public string FormatPercent(int decimals, double percentValue) => CurrentLocale.FormatPercent(decimals, percentValue);
        public float ParseToFloat(string str) => CurrentLocale.ParseToFloat(str);
        public int ParseToInt(string str) => CurrentLocale.ParseToInt(str);
    }
}
