using LManager;
using TMPro;
using UnityEngine;

public class LocalizedText : MonoBehaviour
{
    public string key;
    public TextMeshProUGUI mText;

    public void Start()
    {
        RefreshText();
        LocalizationManager.Instance.onLocalizationChanged.AddListener(OnLanguageChanged);
    }

    private void OnLanguageChanged(Locale locale) => RefreshText();

    private void RefreshText()
    {
        mText.text = LocalizationManager.Instance.Get(key);
    }
}
