using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] GameObject MainMenuCanvas;
    [SerializeField] GameObject OptionsCanvas;
    public void NewGame()
    {
        SceneManager.LoadScene(1);
        Debug.LogWarning("Ajouter Confirmation d'écrasement de sauvegarde si le joueur à une sauvegarde");
    }

    public void Continue()
    {
        Debug.LogError("Pas de données du joueur à charger");
    }

    public void Options()
    {
        MainMenuCanvas.SetActive(false);
        OptionsCanvas.SetActive(true);
    }

    public void CloseOptions()
    {
        OptionsCanvas.SetActive(false);
        MainMenuCanvas.SetActive(true);
    }

    public void Quit()
    {
        Debug.Log("Quitte l'application");
        Application.Quit();
    }
}
