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

    [Tooltip("Mettre l'orientation du canon de l'aspirateur dans cette variable")]
    [SerializeField] Transform vacuumCannonOrientationTransform;
    [SerializeField] MinigameUIManager minigameUIManager;
    private TargetDirection targetDirection;

    private enum TargetDirection { Up, Down, Left, Right }

    public void Awake()
    {
        if (vacuumCannonOrientationTransform == null || minigameUIManager == null)
        {
            if(vacuumCannonOrientationTransform == null && minigameUIManager == null)
            {
                Debug.LogError("Mettre les références pour l'orientation du cannon et pour minigameUIManager");
            }
            else if(minigameUIManager == null)
            {
                Debug.LogError("Mettre la référence pour minigameUIManager");
            }
            else
            {
                Debug.LogError("Mettre la référence pour l'orientation du cannon");
            }
        }
    }

    public void Init(CaptureMinigameData data, GameObject ghost)
    {
        duration = data.duration;
        completed = false;

        initialForward = vacuumCannonOrientationTransform.forward.normalized;
        //Prend aléatoirement la direction dans lequel le mini-jeu va demander au joueur de tourner l'arme
        targetDirection = (TargetDirection)Random.Range(0, 4);
        Debug.Log($"[ResistanceMinigame] Diriger l'arme vers : {targetDirection} (±{requiredAngle}°)");
        Debug.LogWarning("Ajouter UI selon la direction");
        StartCoroutine(ResistanceTimer());
    }

    private void Update()
    {
        if (completed || successTriggered) return;

        Vector3 currentForward = vacuumCannonOrientationTransform.forward.normalized;

        // Angle entre direction initiale et actuelle
        float angle = Vector3.Angle(initialForward, currentForward);

        // Direction du changement
        Vector3 rotationOffset = currentForward - initialForward;
        Vector3 localOffset = vacuumCannonOrientationTransform.InverseTransformDirection(rotationOffset).normalized;

        bool directionMatch = false;

        switch (targetDirection)
        {
            case TargetDirection.Up:
                directionMatch = localOffset.y > 0.5f;
                break;
            case TargetDirection.Down:
                directionMatch = localOffset.y < -0.5f;
                break;
            case TargetDirection.Left:
                directionMatch = localOffset.x < -0.5f;
                break;
            case TargetDirection.Right:
                directionMatch = localOffset.x > 0.5f;
                break;
        }

        if (angle >= requiredAngle - angleThreshold && directionMatch)
        {
            successTriggered = true;
            Debug.Log("[ResistanceMinigame] Réussi !");
            CompleteMinigame();
        }

    }

    private IEnumerator ResistanceTimer()
    {
        yield return new WaitForSeconds(duration);
        if (!successTriggered)
        {
            Debug.Log("[ResistanceMinigame] Échec.");
        }
        CompleteMinigame();
    }

    private void CompleteMinigame()
    {
        completed = true;
        Destroy(this);
    }

    public bool IsComplete() => completed;
}
