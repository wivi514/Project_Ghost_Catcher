using UnityEngine;

public class CaptureMiniGameManager : MonoBehaviour
{
    public void LaunchMinigame(CaptureMinigameData data, GameObject ghost)
    {
        ICaptureMinigame minigame = null;

        switch (data.captureMinigameType)
        {
            case CaptureMinigameType.Resistance:
                Debug.LogWarning("Ajouter mini jeu Resistance");
                minigame = ghost.AddComponent<ResistanceMinigame>();
                break;
            case CaptureMinigameType.TargetingTrap:
                // À ajouter
                //minigame = ghost.AddComponent<TargetingTrapMiniGame>();
                Debug.LogWarning("Ajouter TargetingTrapMiniGame");
                break;
        }

        //Si le minigame n'est pas null lance le mini-jeu de capture
        if (minigame != null)
        {
            minigame.Init(data, ghost);
        }
    }
}
