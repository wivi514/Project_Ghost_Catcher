using System.Collections;
using UnityEngine;

public class ResistanceMinigame : MonoBehaviour, ICaptureMinigame
{
    private float duration;
    private bool completed;

    private Vector3 initialForward;
    private float requiredAngle = 45f;
    private float angleThreshold = 5f;
    private bool successTriggered = false;

    private Transform cannonOrientation;
    private MinigameUIManager minigameUIManager;
    private TargetDirection targetDirection;

    private enum TargetDirection { Up, Down, Left, Right }

    public void Awake()
    {
        if (minigameUIManager == null)
        {
            Debug.LogWarning($"Mettre la r�f�rence pour minigameUIManager sur {TransformUtils.GetFullPath(this.transform)} pour meilleur performance");
            minigameUIManager = FindFirstObjectByType<MinigameUIManager>();
            if(minigameUIManager == null)
            {
                Debug.LogError("Ajouter minigameUIManager � la sc�ne");
            }
        }
    }

    public void Init(CaptureMinigameData data, GameObject ghost)
    {
        duration = data.duration;
        completed = false;

        initialForward = cannonOrientation.forward.normalized;
        //Prend al�atoirement la direction dans lequel le mini-jeu va demander au joueur de tourner l'arme
        targetDirection = (TargetDirection)Random.Range(0, 4);
        Debug.Log($"[ResistanceMinigame] Diriger l'arme vers : {targetDirection} (�{requiredAngle}�)");
        Debug.LogWarning("Ajouter UI selon la direction");
        StartCoroutine(ResistanceTimer());
    }

    private void Update()
    {
        if (completed || successTriggered) return;

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
            CompleteMinigame();
        }

    }

    private IEnumerator ResistanceTimer()
    {
        yield return new WaitForSeconds(duration);
        if (!successTriggered)
        {
            Debug.Log("[ResistanceMinigame] �chec.");
        }
        CompleteMinigame();
    }

    private void CompleteMinigame()
    {
        completed = true;
        minigameUIManager.clearMinigameUI();
        ScoreManager.addScore(100);
        Destroy(this);
    }

    public bool IsComplete() => completed;

    public void SetCannonAndUI(GameObject cannonOrientation, MinigameUIManager minigameUIManager)
    {
        this.cannonOrientation = cannonOrientation.transform;
        this.minigameUIManager = minigameUIManager;
    }
}
