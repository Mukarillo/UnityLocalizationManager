using LManager;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationTest : MonoBehaviour
{
    public Dropdown dropdown;
    public void OnDropdownChanged(int index)
    {
        LocalizationManager.Instance.ChangeLanguage(dropdown.options[index].text);
    }
}
