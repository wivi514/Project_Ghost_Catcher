using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class LevelSelectionUIManager : MonoBehaviour
{
    [SerializeField] GameObject levelPanel;
    [SerializeField] GameObject levelCanvas;

    private void Awake()
    {
        Verification();
    }

    private void Verification()
    {
        if (levelPanel == null || levelCanvas == null)
        {
            if (levelPanel == null && levelCanvas == null)
            {
                levelPanel = GameObject.Find("LevelPanel");
                levelCanvas = GameObject.Find("LevelCanvas");
                if (levelPanel == null && levelCanvas == null)
                {
                    Debug.LogError($"N'a pas trouver LevelPanel et LevelCanvas, Ajouter la r�f�rence � Levelpanel sur {TransformUtils.GetFullPath(this.transform)} " +
                            $"pour �viter l'erreur et des meilleurs performances");
                }
                else
                {
                    Debug.LogWarning($"Ajouter la r�f�rence � Levelpanel et levelCanvas sur {TransformUtils.GetFullPath(this.transform)} pour des meilleurs performances");
                }
            }
            else
            {
                if (levelPanel == null)
                {
                    levelPanel = GameObject.Find("LevelPanel");
                    if (levelPanel == null)
                    {
                        Debug.LogError($"N'a pas trouver LevelPanel, Ajouter la r�f�rence � Levelpanel sur {TransformUtils.GetFullPath(this.transform)} " +
                            $"pour �viter l'erreur et des meilleurs performances");
                    }
                    else
                    {
                        Debug.LogWarning($"Ajouter la r�f�rence � Levelpanel sur {TransformUtils.GetFullPath(this.transform)} pour des meilleurs performances");
                    }
                }
                else
                {
                    levelCanvas = GameObject.Find("LevelCanvas");
                    if (levelCanvas == null)
                    {
                        Debug.LogError($"N'a pas trouver LevelCanvas, Ajouter la r�f�rence � LevelCanvas sur {TransformUtils.GetFullPath(this.transform)} " +
                            $"pour �viter l'erreur et des meilleurs performances");
                    }
                    else
                    {
                        Debug.LogWarning($"Ajouter la r�f�rence � Levelpanel sur {TransformUtils.GetFullPath(this.transform)} pour des meilleurs performances");
                    }
                }
            }
        }
        levelPanel.SetActive(false);
        levelCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Le joueur entre dans la zone de s�lection de niveau");
            levelCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Le joueur sort de la zone de s�lection de niveau");
            levelCanvas.SetActive(false);
        }
    }
}
