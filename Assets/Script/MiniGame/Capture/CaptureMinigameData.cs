using UnityEngine;

[System.Serializable]
public class CaptureMinigameData
{
    [Tooltip("Le type de mini jeu que c'est")]
    public CaptureMinigameType captureMinigameType;
    [Tooltip("Temps avant que le mini jeu expire (échec) si le jouer ne l'accompli pas avant")]
    public int duration;
    [Tooltip("Le nombre de fois que le mini-jeu est répété")]
    public int repeat;
}
