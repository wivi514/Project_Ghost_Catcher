using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private Transform pauseUI;
    private void Awake()
    {
        pauseUI = transform.Find("PauseUI");
        if (pauseUI == null)
        {
            Debug.LogError($"PauseUI n'a pas été trouvé par le script PauseMenu.cs attaché à {TransformUtils.GetFullPath(this.transform)}");
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

    public void Quit()
    {
        
    }

    private void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }
}
