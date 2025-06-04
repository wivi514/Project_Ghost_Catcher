using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
public class LevelSelectionUIManager : MonoBehaviour
{
    [Header("Base UI")]
    [SerializeField] GameObject levelPanel;
    [SerializeField] GameObject levelCanvas;

    [Header("Gestion des mondes")]
    [SerializeField] private WorldData[] worlds;
    [SerializeField] private Button[] worldButtons;
    [SerializeField] private Button[] levelButtons;
    [SerializeField] private TextMeshProUGUI[] levelButtonTexts;

    private void Awake()
    {
        Verification();
    }

    private void Start()
    {
        for (int i = 0; i < worldButtons.Length; i++)
        {
            int index = i;
            worldButtons[i].onClick.AddListener(() => ShowLevels(index));
        }

        HideAllLevelButtons();
    }

    private void ShowLevels(int worldIndex)
    {
        HideAllLevelButtons();
        levelPanel.SetActive(true);
        WorldData selectedWorld = worlds[worldIndex];

        for (int i = 0; i < selectedWorld.levels.Length; i++)
        {
            int levelIndex = i;
            levelButtons[i].gameObject.SetActive(true);
            levelButtonTexts[i].text = selectedWorld.levels[i].levelName;

            string sceneToLoad = selectedWorld.levels[i].sceneName;
            levelButtons[i].onClick.RemoveAllListeners();
            levelButtons[i].onClick.AddListener(() => LoadLevel(sceneToLoad));
        }
    }

    private void HideAllLevelButtons()
    {
        foreach (var btn in levelButtons)
            btn.gameObject.SetActive(false);
    }

    private void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
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
                    Debug.LogError($"N'a pas trouver LevelPanel et LevelCanvas, Ajouter la référence à Levelpanel sur {TransformUtils.GetFullPath(this.transform)} " +
                            $"pour éviter l'erreur et des meilleurs performances");
                }
                else
                {
                    Debug.LogWarning($"Ajouter la référence à Levelpanel et levelCanvas sur {TransformUtils.GetFullPath(this.transform)} pour des meilleurs performances");
                }
            }
            else
            {
                if (levelPanel == null)
                {
                    levelPanel = GameObject.Find("LevelPanel");
                    if (levelPanel == null)
                    {
                        Debug.LogError($"N'a pas trouver LevelPanel, Ajouter la référence à Levelpanel sur {TransformUtils.GetFullPath(this.transform)} " +
                            $"pour éviter l'erreur et des meilleurs performances");
                    }
                    else
                    {
                        Debug.LogWarning($"Ajouter la référence à Levelpanel sur {TransformUtils.GetFullPath(this.transform)} pour des meilleurs performances");
                    }
                }
                else
                {
                    levelCanvas = GameObject.Find("LevelCanvas");
                    if (levelCanvas == null)
                    {
                        Debug.LogError($"N'a pas trouver LevelCanvas, Ajouter la référence à LevelCanvas sur {TransformUtils.GetFullPath(this.transform)} " +
                            $"pour éviter l'erreur et des meilleurs performances");
                    }
                    else
                    {
                        Debug.LogWarning($"Ajouter la référence à Levelpanel sur {TransformUtils.GetFullPath(this.transform)} pour des meilleurs performances");
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
            Debug.Log("Le joueur entre dans la zone de sélection de niveau");
            levelCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Le joueur sort de la zone de sélection de niveau");
            levelCanvas.SetActive(false);
        }
    }
}
