using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VRInteractionUI : MonoBehaviour
{
    public GameObject leftController;
    public GameObject rightController;
    public LayerMask uiLayerMask;
    public float maxDistance = 10f;

    private Button lastHoveredButton;

    private void Awake()
    {
        if (leftController == null && rightController == null)
        {
            leftController = GameObject.Find("Left Controller");
            rightController = GameObject.Find("Right Controller");
        }
    }

    private void OnDrawGizmos()
    {
        if (rightController != null)
        {
            Gizmos.color = Color.red;
            Vector3 start = rightController.transform.position;
            Vector3 end = start + rightController.transform.forward * maxDistance;
            Gizmos.DrawLine(start, end);
        }
    }

    private void Update()
    {
        HandleUIHover();
    }

    private void HandleUIHover()
    {
        if (rightController == null)
            return;

        Transform controllerTransform = rightController.transform;
        Ray ray = new Ray(controllerTransform.position, controllerTransform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, uiLayerMask))
        {
            Button button = hit.collider.GetComponent<Button>();

            if (button != null)
            {
                if (lastHoveredButton != button)
                {
                    // Quitter l'ancien bouton
                    if (lastHoveredButton != null)
                        ExecuteEvents.Execute(lastHoveredButton.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);

                    // Entrer dans le nouveau bouton
                    ExecuteEvents.Execute(button.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
                    lastHoveredButton = button;
                }
            }
        }
        else
        {
            // Sortir du bouton si plus visé
            if (lastHoveredButton != null)
            {
                ExecuteEvents.Execute(lastHoveredButton.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
                lastHoveredButton = null;
            }
        }
    }

    public void TryInteractWithUI()
    {
        if (rightController == null)
            return;

        Transform controllerTransform = rightController.transform;
        Ray ray = new Ray(controllerTransform.position, controllerTransform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, uiLayerMask))
        {
            Button button = hit.collider.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.Invoke();
                Debug.Log("Bouton cliqué: " + button.name);
            }
        }
    }
}
