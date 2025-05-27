using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Tooltip("Mettre le ScriptableObject qui à les données sur l'enemi voulu")]
    [SerializeField] VacuumableObject vacuumableObject;
    [SerializeField] CaptureMiniGameManager miniGameManager;

    private int index = 0;

    void LaunchNextMinigame()
    {
        if (index < vacuumableObject.captureMinigames.Count)
        {
            var minigame = vacuumableObject.captureMinigames[index];
            miniGameManager.LaunchMinigame(minigame, this.gameObject);
            index++;
            Debug.LogWarning("Changer pour que ça change de mini-jeux aléatoirement lorsque test terminé");
        }
        else
        {
            Destroy(this.gameObject);
            Debug.LogWarning("Ajouter l'ajout de point pour la capture");
        }
    }
}
