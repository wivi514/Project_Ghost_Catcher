using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Tooltip("Mettre le ScriptableObject qui � les donn�es sur l'enemi voulu")]
    [SerializeField] VacuumableObject vacuumableObject;
    [SerializeField] CaptureMiniGameManager miniGameManager;

    private int index = 0;

    private void Awake()
    {
        if (vacuumableObject == null || miniGameManager == null)
        {
            if (vacuumableObject == null && miniGameManager == null)
            {
                Debug.LogError($"Assigner r�f�rence � vacuumableObject et miniGameManager sur {TransformUtils.GetFullPath(this.transform)}");
            }
            else if (vacuumableObject == null)
            {
                Debug.LogError($"Assigner r�f�rence � vacuumableObject sur {TransformUtils.GetFullPath(this.transform)}");
            }
            else
            {
                Debug.LogWarning($"Assigner r�f�rence �  miniGameManager sur {TransformUtils.GetFullPath(this.transform)} pour des meilleurs performance");
                miniGameManager = FindFirstObjectByType<CaptureMiniGameManager>();
            }
        }
    }

    public void LaunchNextMinigame()
    {
        if (index < vacuumableObject.captureMinigames.Count)
        {
            var minigame = vacuumableObject.captureMinigames[index];
            if(minigame == null)
            {
                Debug.LogError("Minigame est null");
            }
            miniGameManager.LaunchMinigame(minigame, this.gameObject);
            index++;
        }
        else
        {
            Destroy(this.gameObject);
            Debug.LogWarning("Ajouter l'ajout de point pour la capture");
            Debug.LogWarning("Ajouter VFX pour la capture");
        }
    }
}
