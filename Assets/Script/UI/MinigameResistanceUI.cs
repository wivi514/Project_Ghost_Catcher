using UnityEngine;
using UnityEngine.UI;

public class MinigameResistanceUI : MonoBehaviour
{
    [Header("Sprite pour la direction des fl�ches")]
    [SerializeField] Sprite arrowUp;
    [SerializeField] Sprite arrowDown;
    [SerializeField] Sprite arrowLeft;
    [SerializeField] Sprite arrowRight;
    private Image m_arrowImage;

    private void Awake()
    {
        //V�rification
        #region Verification
        if (arrowUp == null || arrowDown == null || arrowLeft == null || arrowRight == null)
        {
            if (arrowUp == null && arrowDown == null && arrowLeft == null && arrowRight == null)
            {
                Debug.LogError("Mettre les r�f�rence pour toutes les Sprite des fl�ches");
            }
            else if (arrowUp == null)
            {
                Debug.LogError("Mettre la r�f�rence pour le sprite de la fl�che up");
            }
            else if (arrowDown == null)
            {
                Debug.LogError("Mettre la r�f�rence pour le sprite de la fl�che Down");
            }
            else if (arrowLeft == null)
            {
                Debug.LogError("Mettre la r�f�rence pour le sprite de la fl�che Left");
            }
            else
            {
                Debug.LogError("Mettre la r�f�rence pour le sprite de la fl�che Right");
            }
        }
        #endregion
    }

    //Activer les fl�ches selon la r�gion
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

    //Enlever les fl�ches de l'�cran
    public void DisableArrow()
    {
        m_arrowImage.gameObject.SetActive(false);
    }

    //Prend le component Image qui est donn� du MinigameUIManager.cs
    public void SetArrowImage(Image arrowImage)
    {
        m_arrowImage = arrowImage;
    }
}
