using UnityEngine;

[CreateAssetMenu(fileName = "WorldData", menuName = "Scriptable Objects/WorldData")]
public class WorldData : ScriptableObject
{
    [Tooltip("Nom du monde qui va être afficher dans le UI")]
    public string worldName;
    [Tooltip("Ajout de niveau au monde")]
    public LevelData[] levels;
}

[System.Serializable]
public class LevelData
{
    [Tooltip("Nom à afficher dans le UI de sélection de niveau")]
    public string levelName;
    [Tooltip("Nom de la scene exact selon les fichier")]
    public string sceneName; 
}
