using UnityEngine;

public class ResistanceMinigame : MonoBehaviour, ICaptureMinigame
{
    private float duration;
    private bool completed;

    public void Init(CaptureMinigameData data, GameObject ghost)
    {
        duration = data.duration;
        completed = false;
        Debug.Log($"Mini jeu de r�sistance commenc� pour {duration} secondes.");
    }

    public bool IsComplete() => completed;
}
