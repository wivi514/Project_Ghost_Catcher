using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class HoldToFillGauge : MonoBehaviour
{
    [SerializeField] private Image gaugeFill; // image avec fill
    [SerializeField] private float fillSpeed = 0.5f; 
    [SerializeField] private float emptySpeed = 1f;  

    private bool isHolding = false;

    void Update()
    {
        if (Keyboard.current != null)
        {
            isHolding = Keyboard.current.spaceKey.isPressed;
        }

        if (isHolding)
        {
            // remplir la jauge
            gaugeFill.fillAmount += Time.deltaTime / fillSpeed;
        }
        else
        {
            // vider la jauge
            gaugeFill.fillAmount -= Time.deltaTime / emptySpeed;
        }

        // Clamp 
        gaugeFill.fillAmount = Mathf.Clamp01(gaugeFill.fillAmount);
    }
}
