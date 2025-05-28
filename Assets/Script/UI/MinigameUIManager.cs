using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(MinigameResistanceUI))]
public class MinigameUIManager : MonoBehaviour
{
    private MinigameResistanceUI m_ResistanceUI;
    [Header("Mini-jeu de r�sistance")]
    [SerializeField] Image m_ArrowImage;

    private void Awake()
    {
        #region ResistanceMiniGameInitialization
        m_ResistanceUI = GetComponent<MinigameResistanceUI>();
        if (m_ArrowImage != null)
        {
            m_ResistanceUI.SetArrowImage(m_ArrowImage);
            m_ResistanceUI.DisableArrow();
        }
        else
        {
            Debug.LogError("Mettre la r�f�rence � l'image de la fl�che");
        }
        #endregion
    }
}
