using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public List<LevelData> levels = new List<LevelData>();
    public float masterVolume;
    public string quality;
    public bool fullscreen;
}
