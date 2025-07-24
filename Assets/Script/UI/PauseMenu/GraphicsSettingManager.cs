using UnityEngine;
using TMPro;

public class GraphicsSettingManager : MonoBehaviour
{
    public TMP_Dropdown qualityDropdown;
    public TMP_Dropdown displayModeDropdown;
    public TMP_Dropdown resolutionDropdown;

    private Resolution[] availableResolutions;

    void Start()
    {
        //Qualité graphique
        qualityDropdown.ClearOptions();
        qualityDropdown.AddOptions(new System.Collections.Generic.List<string> { "Low", "Medium", "High" });
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        qualityDropdown.RefreshShownValue();
        qualityDropdown.onValueChanged.AddListener(SetQuality);

        //Mode d'affichage
        displayModeDropdown.ClearOptions();
        displayModeDropdown.AddOptions(new System.Collections.Generic.List<string> { "Fullscreen", "Windowed", "Fullscreen Windowed" });

        int currentModeIndex = Screen.fullScreenMode switch
        {
            FullScreenMode.ExclusiveFullScreen => 0,
            FullScreenMode.Windowed => 1,
            FullScreenMode.FullScreenWindow => 2,
            _ => 0
        };
        displayModeDropdown.value = currentModeIndex;
        displayModeDropdown.RefreshShownValue();
        displayModeDropdown.onValueChanged.AddListener(SetDisplayMode);

        //Résolutions
        resolutionDropdown.ClearOptions();
        availableResolutions = Screen.resolutions;

        var options = new System.Collections.Generic.List<string>();
        int currentResIndex = 0;

        for (int i = 0; i < availableResolutions.Length; i++)
        {
            var res = availableResolutions[i];
            string option = res.width + " x " + res.height;
            options.Add(option);

            if (res.width == Screen.currentResolution.width && res.height == Screen.currentResolution.height)
                currentResIndex = i;
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
    }

    void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index, true);
    }

    void SetDisplayMode(int index)
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

    void SetResolution(int index)
    {
        Resolution res = availableResolutions[index];
        Screen.SetResolution(res.width, res.height, Screen.fullScreenMode);
    }
}
