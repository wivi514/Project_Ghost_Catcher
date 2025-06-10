using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Management;

public class InputManagerHub : MonoBehaviour
{
    [SerializeField] private InputActionAsset InputActions;

    private InputAction moveAction;
    private InputAction shootAction;
    private InputAction pauseAction;
    private InputAction interactAction;

    private Vector2 m_moveAmt;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] Camera flatscreenPlayerCamera;

    [Header("UI")]
    [SerializeField] PauseMenu pauseMenu;
    [SerializeField] CenterScreenRaycaster centerScreenRaycaster;

    private bool isVR;

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
        HideCursor();
        VrVerification();
        UIVerification();
        FindAction();
    }

    private void Update()
    {
        if (isVR == false)
        {
            //Lit la valeur de mouvement de l'input "Move"
            m_moveAmt = moveAction.ReadValue<Vector2>();
        }

        if (pauseAction.WasPressedThisFrame())
        {
            ShowCursor();
            pauseMenu.PauseGame();
        }

        if (interactAction.WasPressedThisFrame())
        {
            centerScreenRaycaster.ButtonClick();
        }
    }

    private void FixedUpdate()
    {
        if (isVR == false)
        {
            //Movement du joueur (WASD ou stick)
            playerMovement.Move(m_moveAmt.x, m_moveAmt.y, flatscreenPlayerCamera);
        }
    }

    //Assigne les InputAction de Unity Input System
    private void FindAction()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        shootAction = InputSystem.actions.FindAction("Attack");
        pauseAction = InputSystem.actions.FindAction("Pause");
        interactAction = InputSystem.actions.FindAction("Interact");
    }

    #region cursor
    private void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    #endregion

    #region Verification
    private void VrVerification()
    {
        //Regarde si le joueur est en vr lorsque le jeu est lancé
        isVR = XRGeneralSettings.Instance.Manager.isInitializationComplete;
    }

    private void UIVerification()
    {
        if (pauseMenu == null)
        {
            pauseMenu = FindFirstObjectByType<PauseMenu>();
            Debug.LogWarning($"Ajouter référence à PauseMenu dans {TransformUtils.GetFullPath(this.transform)} pour meilleur performance");
        }
        if (isVR == false)
        {
            if (centerScreenRaycaster == null)
            {
                centerScreenRaycaster = FindFirstObjectByType<CenterScreenRaycaster>();
                Debug.LogWarning($"Ajouter référence à centerScreenRaycaster dans {TransformUtils.GetFullPath(this.transform)} pour meilleur performance");
            }
        }
    }
    #endregion

    #region InputMap_Enable/Disable
    private void EnablePlayerActionMap()
    {
        InputActions.FindActionMap("Player").Enable();
    }
    private void DisablePlayerActionMap()
    {
        InputActions.FindActionMap("Player").Disable();
    }
    private void EnableUIActionMap()
    {
        InputActions.FindActionMap("UI").Enable();
    }
    private void DisableUIActionMap()
    {
        InputActions.FindActionMap("UI").Disable();
    }
    #endregion
}
