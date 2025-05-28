using UnityEngine;

public class CaptureMiniGameManager : MonoBehaviour
{
    private GameObject cannonOrientation;
    private MinigameUIManager minigameUIManager;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
        minigameUIManager = FindFirstObjectByType<MinigameUIManager>();
        #region Verification
        if (minigameUIManager == null || gameManager == null)
        {
            if(minigameUIManager == null && gameManager == null)
            {
                Debug.LogError("Mettre minigameUIManager et GameManager dans la scène");
            }
            else if (minigameUIManager == null)
            {
                Debug.LogError("Mettre minigameUIManager dans la scène");
            }
            else
            {
                Debug.LogError("Mettre GameManager dans la scène");
            }
        }
        #endregion
    }

    private void Start()
    {
        cannonOrientation = gameManager.getCannonOrientation();
    }

    public void LaunchMinigame(CaptureMinigameData data, GameObject ghost)
    {
        ICaptureMinigame minigame = null;

        switch (data.captureMinigameType)
        {
            case CaptureMinigameType.Resistance:
                minigame = ghost.AddComponent<ResistanceMinigame>();
                ghost.GetComponent<ResistanceMinigame>().SetCannonAndUI(cannonOrientation, minigameUIManager);
                break;
            case CaptureMinigameType.TargetingTrap:
                minigame = ghost.AddComponent<TargetingTrapMiniGame>();
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
