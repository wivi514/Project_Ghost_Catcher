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
            miniGameManager.LaunchMinigame(minigame, this.gameObject);
            index++;
            Debug.LogWarning("Changer pour que �a change de mini-jeux al�atoirement lorsque test termin�");
        }
        else
        {
            Destroy(this.gameObject);
            Debug.LogWarning("Ajouter l'ajout de point pour la capture");
        }
    }
}
