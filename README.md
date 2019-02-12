# UnityLocalizationManager
Localization system to manage multiple languages including date time, currencies, and other informations that change depending on current language.

## How to use
*you can find a pratical example inside this repository in PoolingScene scene*

### 1 - Exporta a CSV file with all the languages and translations (use [this file](https://github.com/Mukarillo/UnityLocalizationManager/blob/master/Assets/Resources/Localization.csv) as base)
### 2 - Place the file wherever you want in the project and change the path in ```LocalizationManager``` [constructor](https://github.com/Mukarillo/UnityLocalizationManager/blob/d43883b7595f3a5851a15978db43d31b1f1274a2/Assets/Scripts/LManager/LocalizationManager.cs#L25)
```c#
public LocalizationManager(string filePath = "PATH/TO/FILE.CSV")
{
```
### 3 - The project will initially use the system language as default. If you wish to change, use ```LocalizationManager.Instance.ChangeLanguage``` using the language code or Locale (you can find/add Locales in [```Locale.cs```](https://github.com/Mukarillo/UnityLocalizationManager/blob/master/Assets/Scripts/LManager/Locale.cs)
```c#
LocalizationManager.Instance.ChangeLanguage("en_US");
```
### 4 - To get a localized text, use ```LocalizationManager.Instance.Get```. You can also use parameters (explained below)
```c#
LocalizationManager.Instance.ChangeLanguage("es_ES");
var localizedHello = LocalizationManager.Instance.Get("hello"); //will return Hola
```
### 5 - To use dynamic values with localized text, use the LocalizationManager.PARAMETER_DELIMITER between a key and call ```Get```with the respective dictionary.
```c#
//Example of a localized text with parameter
//playerPoints = You have @points@ points!
var pp = 10; 
var localizedPoints = LocalizationManager.Instance.Get("playerPoints", new Dictionary<string, string>() {{"points", pp.ToString()}});//will return You have 10 points!
```

## LocalizationManager `public` overview
### Properties
|name  |type  |description  |
|--|--|--|
|`CurrentLocale` |**Locale** |*The current Locale being use to gather the translations.*  |
|`onLocalizationChanged` |**UnityEvent** |*Callback triggered when language is changed (usefull to change text/sprites that is already on screen)*  |

### Methods

</br>

> `LocalizationManager.ChangeReferenceFile`
- *Description*: Change the file to get the translations data.

- *Parameters*:

|name  |type  |description  |
|--|--|--|
|`filePath` |**string** |*Path to the file.*  |

</br>

> `LocalizationManager.ResetLanguageToDeviceLanguage`
- *Description*: Resets the language to the system's default.

</br>

> `LocalizationManager.ChangeLanguage`
- *Description*: Changes the language.

- *Parameters* :

|name  |type  |description  |
|--|--|--|
|`code` |**string** |*ISO language and country code. Previous registered in Locale*  |

</br>

> `LocalizationManager.ChangeLanguage`
- *Description*: Changes the language.

- *Parameters* :

|name  |type  |description  |
|--|--|--|
|`locale` |**Locale** |*The locale to be change*  |

</br>

> `LocationManager.Exists`
- *Description*: Returns true if the key is present in the language dictionary.

- *Parameters* :

|name  |type  |description  |
|--|--|--|
|`key` |**string** |*Key to compare*  |

</br>

> `LocationManager.Get`
- *Description*: Returns the localized string if it has the key. Otherwise will return DEFAULT_VALUE_MISSING_KEY; 

- *Parameters* :

|name  |type  |description  |
|--|--|--|
|`key` |**string** |*Key to compare*  |
|`replaces` |**Dictionary< string, string >** |*Replaces texts that are between PARAMETER_DELIMITER character and replace with the value*  |

- *Variants* :
--GetUpper: Return all the characters in uppercase;
--GetLower: Return all the characters in lowercase;
--GetFirstLetterUpper: Return with just the first letter uppercase and the rest lowercase;
--TryGet: Returns true if there is a translation to that key and out the translation value;

</br>

> `LocationManager.FormatDate`
- *Description*: Format the date based on the locale culture info.

- *Parameters* :

|name  |type  |description  |
|--|--|--|
|`dt` |**DateTime** |*Date to be formated*  |

</br>

> `LocationManager.FormatTime`
- *Description*: Format the time based on the locale culture info.

- *Parameters* :

|name  |type  |description  |
|--|--|--|
|`dt` |**DateTime** |*Time to be formated*  |

</br>

> `LocationManager.FormatCurrency`
- *Description*: Format the currency based on the locale culture info.

- *Parameters* :

|name  |type  |description  |
|--|--|--|
|`value` |**Double** |*Value to be formated*  |

</br>

> `LocationManager.FormatNumber`
- *Description*: Format the number based on the locale culture info.

- *Parameters* :

|name  |type  |description  |
|--|--|--|
|`value` |**Double** |*Value to be formated*  |
|`decimals` |**int** |*Value to be formated*  |

</br>

> `LocationManager.FormatPercent`
- *Description*: Format the number percentage based on the locale culture info.

- *Parameters* :

|name  |type  |description  |
|--|--|--|
|`value` |**Double** |*Value to be formated*  |
|`percentValue` |**Double** |*Value to be formated*  |

</br>

> `LocationManager.ParseToFloat`
- *Description*: Parse the string into float based on the locale culture info.

- *Parameters* :

|name  |type  |description  |
|--|--|--|
|`str` |**string** |*Value to be parsed*  |

</br>

> `LocationManager.ParseToInt`
- *Description*: Parse the string into int based on the locale culture info.

- *Parameters* :

|name  |type  |description  |
|--|--|--|
|`str` |**string** |*Value to be parsed*  |

### Future releases
- [ ] Allow line jump in localized text
- [ ] Allow csv files that are not local (internet)

