using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GraphicsSettingManager : MonoBehaviour
{
    public TMP_Dropdown qualityDropdown;
    public TMP_Dropdown displayModeDropdown;
    public TMP_Dropdown resolutionDropdown;

    private Resolution[] availableResolutions;
    private SaveData saveData;

    void Start()
    {
        // Charger les données sauvegardées
        saveData = SaveSystem.Load();

        // === QUALITÉ ===
        qualityDropdown.ClearOptions();
        var qualityOptions = new List<string> { "Low", "Medium", "High" };
        qualityDropdown.AddOptions(qualityOptions);

        // Appliquer la qualité sauvegardée (ou défaut High)
        int qualityIndex = Mathf.Clamp(qualityOptions.IndexOf(saveData.quality), 0, 2);
        qualityDropdown.value = qualityIndex;
        qualityDropdown.RefreshShownValue();
        QualitySettings.SetQualityLevel(qualityIndex);

        qualityDropdown.onValueChanged.AddListener(index =>
        {
            saveData.quality = qualityOptions[index];
            QualitySettings.SetQualityLevel(index);
            SaveSystem.Save(saveData);
        });

        // === MODE D'AFFICHAGE ===
        displayModeDropdown.ClearOptions();
        var modeOptions = new List<string> { "Fullscreen", "Windowed", "Fullscreen Windowed" };
        displayModeDropdown.AddOptions(modeOptions);

        int displayIndex = saveData.fullscreen switch
        {
            true when Screen.fullScreenMode == FullScreenMode.FullScreenWindow => 2,
            true => 0,
            false => 1
        };

        displayModeDropdown.value = displayIndex;
        displayModeDropdown.RefreshShownValue();
        ApplyDisplayMode(displayIndex);

        displayModeDropdown.onValueChanged.AddListener(index =>
        {
            ApplyDisplayMode(index);
            saveData.fullscreen = index != 1; // windowed = false
            SaveSystem.Save(saveData);
        });

        // === RÉSOLUTION ===
        resolutionDropdown.ClearOptions();
        availableResolutions = Screen.resolutions;

        var resOptions = new List<string>();
        int resIndex = 0;

        for (int i = 0; i < availableResolutions.Length; i++)
        {
            var res = availableResolutions[i];
            string option = res.width + " x " + res.height;
            resOptions.Add(option);

            if (res.width == saveData.resolutionWidth && res.height == saveData.resolutionHeight)
                resIndex = i;
        }

        resolutionDropdown.AddOptions(resOptions);
        resolutionDropdown.value = resIndex;
        resolutionDropdown.RefreshShownValue();
        ApplyResolution(resIndex);

        resolutionDropdown.onValueChanged.AddListener(index =>
        {
            ApplyResolution(index);
            saveData.resolutionWidth = availableResolutions[index].width;
            saveData.resolutionHeight = availableResolutions[index].height;
            SaveSystem.Save(saveData);
        });
    }

    void ApplyDisplayMode(int index)
    {
        switch (index)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            case 1:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
            case 2:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
        }
    }

    void ApplyResolution(int index)
    {
        Resolution res = availableResolutions[index];
        Screen.SetResolution(res.width, res.height, Screen.fullScreenMode);
    }
}
