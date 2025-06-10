using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MinigameResistanceUI))]
public class MinigameUIManager : MonoBehaviour
{
    [Header("Mini-jeu de r�sistance")]
    private MinigameResistanceUI m_ResistanceUI;
    [SerializeField] Image ArrowImage;

    private void Awake()
    {
        #region ResistanceMiniGameInitialization
        m_ResistanceUI = GetComponent<MinigameResistanceUI>();
        if (ArrowImage != null)
        {
            m_ResistanceUI.SetArrowImage(ArrowImage);
            m_ResistanceUI.DisableArrow();
        }
        else
        {
            Debug.LogError("Mettre la r�f�rence � l'image de la fl�che");
        }
        #endregion
    }

    //Fait apparaitre la fl�che n�cessaire selon la direction donn� dans ResistanceMinigame.cs
    public void ResistanceUI(int targetDirection)
    {
        switch (targetDirection)
        {
            case 0:
                m_ResistanceUI.ArrowUp();
                break;
            case 1:
                m_ResistanceUI.ArrowDown();
                break;
            case 2:
                m_ResistanceUI.ArrowLeft();
                break;
            case 3:
                m_ResistanceUI.ArrowRight();
                break;
        }
    }

    public void clearMinigameUI()
    {
        m_ResistanceUI.DisableArrow();
    }
}
