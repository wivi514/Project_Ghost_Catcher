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

    //for the arrow movement
    [SerializeField] private bool hoverX = false;
    [SerializeField] private bool hoverY = true;
    [SerializeField] private float hoverAmplitude = 5f;
    [SerializeField] private float hoverFrequency = 1f;
    private Vector2 startAnchoredPos;
    private RectTransform arrowRect;
    private bool hoverInitialized = false;

    private void InitHover()
    {
        if (!hoverInitialized && m_arrowImage != null)
        {
            arrowRect = m_arrowImage.GetComponent<RectTransform>();
            if (arrowRect != null)
                startAnchoredPos = arrowRect.anchoredPosition;

            hoverInitialized = true;
        }
    }

    private void UpdateHover()
    {
        if (arrowRect == null) return;

        float offset = Mathf.Sin(Time.time * hoverFrequency) * hoverAmplitude;
        Vector2 newPos = startAnchoredPos;

        if (hoverX) newPos.x += offset;
        if (hoverY) newPos.y += offset;

        arrowRect.anchoredPosition = newPos;
    }

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

        hoverX = false;
        hoverY = true;
        InitHover();
    }

    public void ArrowDown()
    {
        m_arrowImage.gameObject.SetActive(true);
        m_arrowImage.sprite = arrowDown;

        hoverX = false;
        hoverY = true;
        InitHover();
    }

    public void ArrowLeft()
    {
        m_arrowImage.gameObject.SetActive(true);
        m_arrowImage.sprite = arrowLeft;

        hoverX = true;
        hoverY = false;
        InitHover();
    }

    public void ArrowRight()
    {
        m_arrowImage.gameObject.SetActive(true);
        m_arrowImage.sprite = arrowRight;

        hoverX = true;
        hoverY = false;
        InitHover();
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

    private void Update()
    {
        UpdateHover();
    }
}
