using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Transform pauseUI;
    [SerializeField] Transform quitPanel;
    private void Awake()
    {
        if (pauseUI == null)
        {
            Debug.LogWarning($"PauseUI n'a pas été trouvé par le script PauseMenu.cs attaché à {TransformUtils.GetFullPath(this.transform)} ajouter le pour meilleur performance");
            pauseUI = transform.Find("PauseUI");
        }
        if (quitPanel == null)
        {
            Debug.LogWarning($"quitPanel n'a pas été trouvé par le script PauseMenu.cs attaché à {TransformUtils.GetFullPath(this.transform)} ajouter le pour meilleur performance");
            quitPanel = transform.Find("Quit_Panel");
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseUI.gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseUI.gameObject.SetActive(false);
        HideCursor();
    }

    public void Options()
    {

    }

    public void Help()
    {

    }

    public void QuitConfirmPanel()
    {
        quitPanel.gameObject.SetActive(true);
    }

    public void QuitMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitDesktop()
    {
        Application.Quit();
    }

    public void QuitCancel()
    {
        quitPanel.gameObject.SetActive(false);
    }

    private void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }
}
