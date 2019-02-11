using UnityEngine;

namespace LManager
{
    internal class LocaleCode
    {
        public static Locale GetLocaleCode()
        {
#if UNITY_ANDROID
            using (AndroidJavaClass cls = new AndroidJavaClass("java.util.Locale"))
            {
                if( cls != null )
                {
                    using(AndroidJavaObject locale = cls.CallStatic<AndroidJavaObject>("getDefault"))
                    {
                        if( locale != null )
                        {
                            localeVal = locale.Call<string>("getLanguage") + "_" + locale.Call<string>("getCountry");
                            Debug.Log("Android lang: " + localeVal );
                        }
                        else
                        {
                            Debug.Log( "locale null" );
                        }
                    }
                }
                else
                {
                    Debug.Log( "cls null" );
                }
            }

            Locale.Get(localeVal);
#endif
            return BruteForceLocale();
        }

        private static Locale BruteForceLocale()
        {
            return Locale.GetFromSystem(Application.systemLanguage);
        }
    }
}