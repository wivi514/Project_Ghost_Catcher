using UnityEngine;
using System.Linq;

public class GraphicsStartupLoader : MonoBehaviour
{
    void Awake()
    {
        if (!System.IO.File.Exists(Application.persistentDataPath + "/save.json"))
            return;

        SaveData saveData = SaveSystem.Load();

        // === Appliquer la qualité ===
        int qualityIndex = QualitySettings.names.ToList().IndexOf(saveData.quality);
        if (qualityIndex >= 0)
            QualitySettings.SetQualityLevel(qualityIndex, true);

        // === Appliquer le mode d'affichage ===
        FullScreenMode mode = saveData.fullscreen ? FullScreenMode.ExclusiveFullScreen : FullScreenMode.Windowed;

        // === Vérifier la résolution supportée ===
        Resolution[] availableRes = Screen.resolutions;
        Resolution chosenRes = availableRes
            .FirstOrDefault(r => r.width == saveData.resolutionWidth && r.height == saveData.resolutionHeight);

        if (chosenRes.width == 0 || chosenRes.height == 0)
        {
            // Résolution non trouvée — appliquer une valeur par défaut
            chosenRes = availableRes.Length > 0 ? availableRes.Last() : new Resolution { width = 800, height = 600 };
        }

        // === Appliquer la résolution et le mode ===
        Screen.SetResolution(chosenRes.width, chosenRes.height, mode);
    }
}