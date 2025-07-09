using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteManager : MonoBehaviour
{
    [Header("Configuration")]
    [Tooltip("Nombre de fantômes à capturer pour terminer le niveau.")]
    public int ghostsToCapture = 10;

    [Header("Progression")]
    [Tooltip("Nombre de fantômes capturés.")]
    public int ghostsCaptured = 0;

    private bool levelCompleted = false;

    void Update()
    {
        if (!levelCompleted && ghostsCaptured >= ghostsToCapture)
        {
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        levelCompleted = true;
        Debug.Log("Niveau terminé! Retour au hub");
        SceneManager.LoadScene(1); // Charge la scène du hub
    }

    // Appelle cette méthode depuis un autre script pour mettre à jour la capture (ex: MinigameCapture terminé)
    public void AddGhostCapture()
    {
        ghostsCaptured++;
    }
}
