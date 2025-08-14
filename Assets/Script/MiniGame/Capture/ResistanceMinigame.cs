using System.Collections;
using UnityEngine;
using TMPro;
using UnityEditor;

public class ResistanceMinigame : MonoBehaviour, ICaptureMinigame
{
    private float duration;
    private int repeat;
    private bool completed;

    private Vector3 initialForward;
    private float requiredAngle = 28f;
    private float angleThreshold = 5f;
    private bool successTriggered = false;

    private Transform cannonOrientation;
    private CaptureMiniGameManager captureMiniGameManager;
    private MinigameUIManager minigameUIManager;
    private TargetDirection targetDirection;
    private TargetDirection lastDirection = (TargetDirection)(-1); // valeur invalide au départ

    private TMP_Text scoreFlat; // à changer

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
        Debug.LogWarning("Modifier fonctionnement ResistanceMiniGame pour direction Up et Down");

        scoreFlat = GameObject.Find("ScoreFlat").GetComponent<TMP_Text>();
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

            do
            {
                targetDirection = (TargetDirection)Random.Range(0, 4);
                if (targetDirection == TargetDirection.Up)
                {
                    if (cannonOrientation.transform.rotation.eulerAngles.x > 322 )
                    {
                        targetDirection = lastDirection;
                    }
                }
                else if(targetDirection == TargetDirection.Down)
                {
                    if (cannonOrientation.transform.rotation.eulerAngles.x < 38)
                    {
                        targetDirection = lastDirection;
                    }
                }
            } while (targetDirection == lastDirection);

            lastDirection = targetDirection;

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
        scoreFlat.text = $"{ScoreManager.GetScore()}";
        this.gameObject.GetComponent<EnemyBehaviour>().LaunchNextMinigame();
    }



    public bool IsComplete() => completed;

    public void SetCannonAndUI(GameObject cannonOrientation, MinigameUIManager minigameUIManager)
    {
        this.cannonOrientation = cannonOrientation.transform;
        Debug.Log(TransformUtils.GetFullPath(this.cannonOrientation.transform));
        this.minigameUIManager = minigameUIManager;
    }
}
