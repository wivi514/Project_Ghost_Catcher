using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LanguageSelector : MonoBehaviour
{
    public TMP_Dropdown languageDropdown;

    void Start()
    {
        // Remplir le dropdown (manuellement ou dynamiquement si besoin)
        languageDropdown.onValueChanged.AddListener(OnLanguageChanged);

        // Appliquer la langue actuelle à l'ouverture
        SetCurrentLocale();
    }

    void SetCurrentLocale()
    {
        Locale currentLocale = LocalizationSettings.SelectedLocale;
        if (currentLocale.Identifier.Code == "fr")
            languageDropdown.value = 0;
        else if (currentLocale.Identifier.Code == "en")
            languageDropdown.value = 1;

        languageDropdown.RefreshShownValue();
    }

    void OnLanguageChanged(int index)
    {
        switch (index)
        {
            case 0: // Français
                SetLocale("fr");
                break;
            case 1: // English
                SetLocale("en");
                break;
        }
    }

    void SetLocale(string code)
    {
        var locales = LocalizationSettings.AvailableLocales.Locales;
        foreach (var locale in locales)
        {
            if (locale.Identifier.Code == code)
            {
                LocalizationSettings.SelectedLocale = locale;
                break;
            }
        }
    }
}
