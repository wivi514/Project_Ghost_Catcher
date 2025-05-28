using UnityEngine;
using UnityEngine.UI;

public class MinigameResistanceUI : MonoBehaviour
{
    [Header("Sprite pour la direction des flêches")]
    [SerializeField] Sprite arrowUp;
    [SerializeField] Sprite arrowDown;
    [SerializeField] Sprite arrowLeft;
    [SerializeField] Sprite arrowRight;
    private Image m_arrowImage;

    private void Awake()
    {
        //Vérification
        #region Verification
        if (arrowUp == null || arrowDown == null || arrowLeft == null || arrowRight == null)
        {
            if (arrowUp == null && arrowDown == null && arrowLeft == null && arrowRight == null)
            {
                Debug.LogError("Mettre les référence pour toutes les Sprite des flêches");
            }
            else if (arrowUp == null)
            {
                Debug.LogError("Mettre la référence pour le sprite de la flêche up");
            }
            else if (arrowDown == null)
            {
                Debug.LogError("Mettre la référence pour le sprite de la flêche Down");
            }
            else if (arrowLeft == null)
            {
                Debug.LogError("Mettre la référence pour le sprite de la flêche Left");
            }
            else
            {
                Debug.LogError("Mettre la référence pour le sprite de la flêche Right");
            }
        }
        #endregion
    }

    //Activer les flêches selon la région
    #region DirectionArrow
    public void ArrowUp()
    {
        m_arrowImage.gameObject.SetActive(true);
        m_arrowImage.sprite = arrowUp;
    }

    public void ArrowDown()
    {
        m_arrowImage.gameObject.SetActive(true);
        m_arrowImage.sprite = arrowDown;
    }

    public void ArrowLeft()
    {
        m_arrowImage.gameObject.SetActive(true);
        m_arrowImage.sprite = arrowLeft;
    }

    public void ArrowRight()
    {
        m_arrowImage.gameObject.SetActive(true);
        m_arrowImage.sprite = arrowRight;
    }
    #endregion

    //Enlever les flêches de l'écran
    public void DisableArrow()
    {
        m_arrowImage.gameObject.SetActive(false);
    }

    //Prend le component Image qui est donné du MinigameUIManager.cs
    public void SetArrowImage(Image arrowImage)
    {
        m_arrowImage = arrowImage;
    }
}
