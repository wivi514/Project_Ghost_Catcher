using UnityEngine;
using UnityEngine.UI;

public class CenterScreenRaycaster : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask uiLayerMask;
    [SerializeField] private float rayDistance = 5f;


    //Est appeler lorsqu'on appuie sur le bouton d'intéraction sert a appuyer sur des bouton World Space UI
    public void ButtonClick()
    {
        Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray ray = mainCamera.ScreenPointToRay(center);

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, uiLayerMask))
        {
            Button button = hit.collider.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.Invoke();
                Debug.Log("Bouton cliqué via le centre de l’écran !");
            }
        }
    }
}
