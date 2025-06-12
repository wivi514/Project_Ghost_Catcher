using System.Collections;
using UnityEngine;

public class ResistanceMinigame : MonoBehaviour, ICaptureMinigame
{
    private float duration;
    private int repeat;
    private bool completed;

    private Vector3 initialForward;
    private float requiredAngle = 45f;
    private float angleThreshold = 5f;
    private bool successTriggered = false;

    private Transform cannonOrientation;
    private CaptureMiniGameManager captureMiniGameManager;
    private MinigameUIManager minigameUIManager;
    private TargetDirection targetDirection;

    private enum TargetDirection { Up, Down, Left, Right }

    public void Awake()
    {
        captureMiniGameManager = FindFirstObjectByType<CaptureMiniGameManager>();
        if (captureMiniGameManager == null)
        {
            Debug.LogError("Ajouter captureMiniGameManager à la scène");
        }
        else
        {
            minigameUIManager = captureMiniGameManager.minigameUIManager;
            if (minigameUIManager == null)
            {
                Debug.LogError("N'a pas réussi à attribuer minigameUIManager");
            }
        }
    }

    public void Init(CaptureMinigameData data, GameObject ghost)
    {
        duration = data.duration;
        repeat = data.repeat;
        completed = false;

        initialForward = cannonOrientation.forward.normalized;
        
        StartCoroutine(RepeatResistanceMinigame());
    }

    private void Update()
    {
        if (successTriggered) return;

        Vector3 currentForward = cannonOrientation.forward.normalized;

        // Angle entre direction initiale et actuelle
        float angle = Vector3.Angle(initialForward, currentForward);

        // Direction du changement
        Vector3 rotationOffset = currentForward - initialForward;
        Vector3 localOffset = cannonOrientation.InverseTransformDirection(rotationOffset).normalized;

        bool directionMatch = false;

        switch (targetDirection)
        {
            case TargetDirection.Up:
                minigameUIManager.ResistanceUI((int)targetDirection);
                directionMatch = localOffset.y > 0.5f;
                break;
            case TargetDirection.Down:
                minigameUIManager.ResistanceUI((int)targetDirection);
                directionMatch = localOffset.y < -0.5f;
                break;
            case TargetDirection.Left:
                minigameUIManager.ResistanceUI((int)targetDirection);
                directionMatch = localOffset.x < -0.5f;
                break;
            case TargetDirection.Right:
                minigameUIManager.ResistanceUI((int)targetDirection);
                directionMatch = localOffset.x > 0.5f;
                break;
        }

        if (angle >= requiredAngle - angleThreshold && directionMatch)
        {
            successTriggered = true;
            Debug.Log("[ResistanceMinigame] Réussi !");
            //CompleteMinigame();
        }

    }

    private IEnumerator RepeatResistanceMinigame()
    {
        for (int i = 0; i < repeat; i++)
        {
            successTriggered = false;

            //Prend aléatoirement la direction dans lequel le mini-jeu va demander au joueur de tourner l'arme
            targetDirection = (TargetDirection)Random.Range(0, 4);
            Debug.Log($"[ResistanceMinigame] Étape {i + 1}/{repeat} : Diriger l'arme vers {targetDirection}");

            float elapsed = 0f;
            while (elapsed < duration && !successTriggered)
            {
                elapsed += Time.deltaTime;
                yield return null;
            }

            if (!successTriggered)
            {
                Debug.Log($"[ResistanceMinigame] Échec à l'étape {i + 1}/{repeat}");
                break; // sort de la boucle si le joueur échoue une étape
            }

            yield return new WaitForSeconds(0.5f); // petite pause entre les répétitions
        }

        CompleteMinigame();
    }


    private void CompleteMinigame()
    {
        completed = true;
        minigameUIManager.clearMinigameUI();
        ScoreManager.addScore(100);
        this.gameObject.GetComponent<EnemyBehaviour>().LaunchNextMinigame();
    }

    public bool IsComplete() => completed;

    public void SetCannonAndUI(GameObject cannonOrientation, MinigameUIManager minigameUIManager)
    {
        this.cannonOrientation = cannonOrientation.transform;
        this.minigameUIManager = minigameUIManager;
    }
}
