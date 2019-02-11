using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

namespace LManager
{
    public sealed class Locale
    {
        private static readonly Dictionary<string, Locale> mHash = new Dictionary<string, Locale>();

        public static readonly Locale EN_US = new Locale("en_US", "en-US", "English", SystemLanguage.English);
        public static readonly Locale PT_BR = new Locale("pt_BR", "pt-BR", "Português Brasil", SystemLanguage.Portuguese);
        public static readonly Locale PT_PT = new Locale("pt_PT", "pt-PT", "Português Portugal");
        public static readonly Locale ES_LA = new Locale("es_LA", "es-MX", "Español Américas", SystemLanguage.Spanish);
        public static readonly Locale ES_ES = new Locale("es_ES", "es-ES", "Español España", SystemLanguage.Basque, SystemLanguage.Catalan);
        public static readonly Locale FR_FR = new Locale("fr_FR", "fr-FR", "Français", SystemLanguage.French);
        public static readonly Locale IT_IT = new Locale("it_IT", "it-IT", "Italiano", SystemLanguage.Italian);

        public static readonly Locale DE_DE = new Locale("de_DE", "de-DE", "Deutsch", SystemLanguage.German);
        public static readonly Locale TR_TR = new Locale("tr_TR", "tr-TR", "Türkçe", SystemLanguage.Turkish);
        public static readonly Locale PL_PL = new Locale("pl_PL", "pl-PL", "Polski", SystemLanguage.Polish);
        public static readonly Locale RO_RO = new Locale("ro_RO", "ro-RO", "Română", SystemLanguage.Romanian);

        public static readonly Locale BG_BG = new Locale("bg_BG", "bg-BG", "Български", SystemLanguage.Bulgarian);
        public static readonly Locale NL_NL = new Locale("nl_NL", "nl-NL", "Nederlands", SystemLanguage.Dutch);
        public static readonly Locale DA_DK = new Locale("da_DK", "da-DK", "Dansk", SystemLanguage.Danish);
        public static readonly Locale SV_SE = new Locale("sv_SE", "sv-SE", "Svenska", SystemLanguage.Swedish);

        public static readonly Locale NB_NO = new Locale("nb_NO", "nb-NO", "Norsk", SystemLanguage.Norwegian);
        public static readonly Locale FI_FI = new Locale("fi_FI", "fi-FI", "Suomi", SystemLanguage.Finnish);
        public static readonly Locale TL_PH = new Locale("tl_PH", "en-PH", "Filipino", SystemLanguage.Thai);

        public static readonly Locale RU_RU = new Locale("ru_RU", "ru-RU", "Русский", SystemLanguage.Russian);

        public readonly string code;
        public readonly string name;

        public string language => code.Substring(0, 2);
        public CultureInfo cultureInfo { get; }

        private readonly List<string> mDateSeparators = new List<string> { ".", "-", "/" };
        private readonly SystemLanguage[] mSystemLanguages;

        public string FormatDayMonth(DateTime dt)
        {
            var format = cultureInfo.DateTimeFormat.ShortDatePattern;
            var dateSeparator = "";
            foreach (var separator in mDateSeparators)
            {
                if (!format.Contains(separator)) continue;
                dateSeparator = separator;
                break;
            }
            format = format.Replace(dateSeparator + "y", "").Replace("y" + dateSeparator, "").Replace("y", "");
            format = format.Replace("d", "dd");
            format = format.Replace("M", "MM");
            return dt.ToString(format);
        }

        public string FormatDate(DateTime dt) => dt.ToString("d", cultureInfo);
        public string FormatTime(DateTime dt) => dt.ToString("t", cultureInfo);
        public string FormatCurrency(double value) => value.ToString("C", cultureInfo);
        public string FormatNumber(double value, int decimals) => value.ToString(decimals >= 0 ? "N" + decimals : "N0", cultureInfo);
        public string FormatPercent(int decimals, double percentValue) => (percentValue / 100).ToString("P0" + decimals, cultureInfo);
        public float ParseToFloat(string str) => float.Parse(str, cultureInfo);
        public int ParseToInt(string str) => int.Parse(str, cultureInfo);
        
        private Locale(string code, string cultureName, string name, params SystemLanguage[] mSystemLanguages)
        {
            this.code = code;
            cultureInfo = new CultureInfo(cultureName);
            this.name = name;
            this.mSystemLanguages = mSystemLanguages;

            if (mHash.ContainsKey(code)) throw new Exception("duplicated code " + code);
            mHash[code] = this;
        }

        public static Locale Get(string code)
        {
            if (code != null && mHash.ContainsKey(code)) return mHash[code];
            return EN_US;
        }

        public static Locale GetFromSystem(SystemLanguage language)
        {
            if (EN_US.mSystemLanguages.Any(sl => sl == language)) return EN_US;
            if (PT_BR.mSystemLanguages.Any(sl => sl == language)) return PT_BR;
            if (PT_PT.mSystemLanguages.Any(sl => sl == language)) return PT_PT;
            if (ES_LA.mSystemLanguages.Any(sl => sl == language)) return ES_LA;
            if (ES_ES.mSystemLanguages.Any(sl => sl == language)) return ES_ES;
            if (FR_FR.mSystemLanguages.Any(sl => sl == language)) return FR_FR;
            if (IT_IT.mSystemLanguages.Any(sl => sl == language)) return IT_IT;
            if (DE_DE.mSystemLanguages.Any(sl => sl == language)) return DE_DE;
            if (TR_TR.mSystemLanguages.Any(sl => sl == language)) return TR_TR;
            if (PL_PL.mSystemLanguages.Any(sl => sl == language)) return PL_PL;
            if (RO_RO.mSystemLanguages.Any(sl => sl == language)) return RO_RO;
            if (BG_BG.mSystemLanguages.Any(sl => sl == language)) return BG_BG;
            if (NL_NL.mSystemLanguages.Any(sl => sl == language)) return NL_NL;
            if (DA_DK.mSystemLanguages.Any(sl => sl == language)) return DA_DK;
            if (SV_SE.mSystemLanguages.Any(sl => sl == language)) return SV_SE;
            if (NB_NO.mSystemLanguages.Any(sl => sl == language)) return NB_NO;
            if (FI_FI.mSystemLanguages.Any(sl => sl == language)) return FI_FI;
            if (TL_PH.mSystemLanguages.Any(sl => sl == language)) return TL_PH;
            if (RU_RU.mSystemLanguages.Any(sl => sl == language)) return RU_RU;
            return EN_US;
        }
    }
}