using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene(1);
        Debug.LogWarning("Ajouter Confirmation d'écrasement de sauvegarde si le joueur à une sauvegarde");
    }

    public void Continue()
    {
        SceneManager.LoadScene(1);
        Debug.LogError("Ajouter chargement des données du joueur");
    }

    public void Options()
    {
        Debug.LogWarning("Ajouter menu options au menu principal");
    }

    public void Quit()
    {
        Debug.Log("Quitte l'application");
        Application.Quit();
    }
}
