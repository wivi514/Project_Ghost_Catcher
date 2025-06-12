using UnityEngine;
using UnityEngine.InputSystem;

public class InputManagerMainMenu : MonoBehaviour
{
    [SerializeField] InputActionAsset InputActions;

    private InputAction interactAction;
    private VRInteractionUI vrInteractionUI;

    private void OnEnable()
    {
        EnablePlayerActionMap();
    }

    private void OnDisable()
    {
        DisablePlayerActionMap();
    }

    private void Awake()
    {
        FindAction();
        vrInteractionUI = FindFirstObjectByType<VRInteractionUI>(); 
    }

    private void Update()
    {
        if (interactAction.WasPressedThisFrame())
        {
            vrInteractionUI.TryInteractWithUI();
        }
    }

    //Assigne les InputAction de Unity Input System
    private void FindAction()
    {
        interactAction = InputSystem.actions.FindAction("Interact");
    }

    #region InputMap_Enable/Disable
    private void EnablePlayerActionMap()
    {
        InputActions.FindActionMap("Player").Enable();
    }
    private void DisablePlayerActionMap()
    {
        InputActions.FindActionMap("Player").Disable();
    }
    #endregion
}
