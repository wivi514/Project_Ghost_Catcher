using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LightScrollSelector : MonoBehaviour
{
    [SerializeField] private Image glowImage; // glow
    [SerializeField] private RectTransform[] lightPositions; // positions lumières (index)
    private int currentIndex = 0;

    void Update()
    {
        if (Mouse.current == null) return;

        float scrollValue = Mouse.current.scroll.ReadValue().y;

        if (scrollValue < 0f) // Scroll up
        {
            currentIndex--;
            if (currentIndex < 0) currentIndex = lightPositions.Length - 1; // Wrap around
            MoveGlow();
        }
        else if (scrollValue > 0f) // Scroll down
        {
            currentIndex++;
            if (currentIndex >= lightPositions.Length) currentIndex = 0; // Wrap around
            MoveGlow();
        }
    }

    private void MoveGlow()
    {
        // Le glow va sur la bonne lumière
        glowImage.rectTransform.position = lightPositions[currentIndex].position;
    }
}
