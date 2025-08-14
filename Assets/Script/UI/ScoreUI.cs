using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    private TMP_Text m_TextMeshPro;

    private void Awake()
    {
        m_TextMeshPro = GetComponent<TMP_Text>();
        SetScoreText();
    }

    private void SetScoreText()
    {
        m_TextMeshPro.text = "" + ScoreManager.CurrentScore;
    }
}
