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
                break;
            case CaptureMinigameType.TargetingTrap:
                Debug.LogWarning("Ajouter mini jeu Targeting Trap");
                break;
        }

        minigame?.Init(data, ghost);
    }
}
