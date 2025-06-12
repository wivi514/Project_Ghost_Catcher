using System.Collections;
using UnityEngine;

public class ResistanceMinigame : MonoBehaviour, ICaptureMinigame
{
    private float duration;
    private int repeat;
    private bool completed;

    private Vector3 initialForward;
    private float requiredAngle = 30f;
    private float angleThreshold = 5f;
    private bool successTriggered = false;

    private Transform cannonOrientation;
    private CaptureMiniGameManager captureMiniGameManager;
    private MinigameUIManager minigameUIManager;
    private TargetDirection targetDirection;
    private TargetDirection lastDirection = (TargetDirection)(-1); // valeur invalide au d�part

    private enum TargetDirection { Up, Down, Left, Right }

    public void Awake()
    {
        captureMiniGameManager = FindFirstObjectByType<CaptureMiniGameManager>();
        if (captureMiniGameManager == null)
        {
            Debug.LogError("Ajouter captureMiniGameManager � la sc�ne");
        }
        else
        {
            minigameUIManager = captureMiniGameManager.minigameUIManager;
            if (minigameUIManager == null)
            {
                Debug.LogError("N'a pas r�ussi � attribuer minigameUIManager");
            }
        }
        Debug.LogWarning("Modifier fonctionnement ResistanceMiniGame pour direction Up et Down");
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
            Debug.Log("[ResistanceMinigame] R�ussi !");
            //CompleteMinigame();
        }

    }

    private IEnumerator RepeatResistanceMinigame()
    {
        for (int i = 0; i < repeat; i++)
        {
            successTriggered = false;

            //Prend al�atoirement la direction dans lequel le mini-jeu va demander au joueur de tourner l'arme et fait en sorte que �a ne soit pas la m�me que la pr�c�dente
            /*do
            {
                //Remettre Range (0, 4) quand Up et Down Fix
                targetDirection = (TargetDirection)Random.Range(2, 4);
            } while (targetDirection == lastDirection);*/

            // Enlever quand Up et down est fix et enlever commentaire en haut
            targetDirection = (TargetDirection)Random.Range(2, 4);
            lastDirection = targetDirection;

            Debug.Log($"[ResistanceMinigame] �tape {i + 1}/{repeat} : Diriger l'arme vers {targetDirection}");

            float elapsed = 0f;
            while (elapsed < duration && !successTriggered)
            {
                elapsed += Time.deltaTime;
                yield return null;
            }

            if (!successTriggered)
            {
                Debug.Log($"[ResistanceMinigame] �chec � l'�tape {i + 1}/{repeat}");
                break; // sort de la boucle si le joueur �choue une �tape
            }

            yield return new WaitForSeconds(0.5f); // petite pause entre les r�p�titions
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
