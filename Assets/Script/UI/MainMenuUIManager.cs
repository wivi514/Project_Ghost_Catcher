using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene(1);
        Debug.LogWarning("Ajouter Confirmation d'�crasement de sauvegarde si le joueur � une sauvegarde");
    }

    public void Continue()
    {
        SceneManager.LoadScene(1);
        Debug.LogError("Ajouter chargement des donn�es du joueur");
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
