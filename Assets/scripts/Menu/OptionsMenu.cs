﻿using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using System.Collections;

public class OptionsMenu : MonoBehaviour
{
    [Header("UI References")]
    public Toggle fullscreenToggle;

    [Header("Text References")]
    public TMP_Text fullscreenText;

    private const string LanguageKey = "SelectedLanguage"; // Key for saved language
    private bool isSwitching = false; // Prevents multiple switching

    void Start()
    {
        StartCoroutine(InitializeLocalization());
        LoadSettings();

        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
        LocalizationSettings.SelectedLocaleChanged += OnLanguageChanged;
    }

    private IEnumerator InitializeLocalization()
    {
        yield return LocalizationSettings.InitializationOperation;

        // Set saved language from PlayerPrefs (default: English)
        int savedLocaleID = PlayerPrefs.GetInt(LanguageKey, 0);
        SetLanguage(savedLocaleID);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
        UpdateFullscreenText(isFullscreen);
    }

    private void SetLanguage(int localeID)
    {
        if (isSwitching) return;
        StartCoroutine(ChangeLanguage(localeID));
    }

    private IEnumerator ChangeLanguage(int localeID)
    {
        isSwitching = true;
        yield return LocalizationSettings.InitializationOperation;

        if (localeID < 0 || localeID >= LocalizationSettings.AvailableLocales.Locales.Count)
        {
            Debug.LogError($"Invalid Locale ID: {localeID}. Setting to default (0).");
            localeID = 0;
        }

        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
        PlayerPrefs.SetInt(LanguageKey, localeID);
        PlayerPrefs.Save();

        Debug.Log($"🌍 Language changed to: {LocalizationSettings.SelectedLocale.LocaleName}");

        // Update UI after language change
        UpdateFullscreenText(fullscreenToggle.isOn);

        isSwitching = false;
    }

    void LoadSettings()
    {
        bool savedFullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        fullscreenToggle.isOn = savedFullscreen;
        Screen.fullScreen = savedFullscreen;
        UpdateFullscreenText(savedFullscreen);
    }

    void UpdateFullscreenText(bool isFullscreen)
    {
        int localeID = PlayerPrefs.GetInt(LanguageKey, 0);
        string languageCode = LocalizationSettings.AvailableLocales.Locales[localeID].Identifier.Code;
        fullscreenText.text = GetLocalizedOnOff(isFullscreen, languageCode);
    }

    void OnLanguageChanged(Locale newLocale)
    {
        UpdateFullscreenText(fullscreenToggle.isOn);
    }

    string GetLocalizedOnOff(bool isOn, string languageCode)
    {
        if (languageCode == "de")
        {
            return isOn ? "On" : "Off";
        }
        else
        {
            return isOn ? "On" : "Off";
        }
    }
}
