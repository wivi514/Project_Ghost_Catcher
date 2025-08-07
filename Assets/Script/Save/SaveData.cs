using System.Collections.Generic;

//Dans un autre script
/*[System.Serializable]
public class LevelData
{
    public string levelName;
    public int score;
    public int timesCompleted;
}*/

[System.Serializable]
public class SaveData
{
    public List<LevelData> levels = new List<LevelData>();
    public string quality;
    public bool fullscreen;

    public int resolutionWidth;
    public int resolutionHeight;
}
