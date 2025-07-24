using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Transform pauseUI;
    [SerializeField] GameObject PauseCanvas;
    [SerializeField] GameObject OptionsCanvas;
    [SerializeField] GameObject HelpCanvas;
    [SerializeField] Transform quitPanel;

    private bool isPaused = false;
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
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            isPaused = true;
            Time.timeScale = 0;
            pauseUI.gameObject.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        CloseAllPanel();
        pauseUI.gameObject.SetActive(false);
        HideCursor();
    }

    public void Options()
    {
        PauseCanvas.SetActive(false);
        OptionsCanvas.SetActive(true);
    }

    public void Help()
    {
        PauseCanvas.SetActive(false);
        HelpCanvas.SetActive(true);
    }

    public void CloseAllPanel()
    {
        OptionsCanvas.SetActive(false);
        HelpCanvas.SetActive(false);
        quitPanel.gameObject.SetActive(false);
        PauseCanvas.SetActive(true);
    }

    public void QuitConfirmPanel()
    {
        quitPanel.gameObject.SetActive(true);
    }

    public void QuitMainMenu()
    {
        Time.timeScale = 1;
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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
