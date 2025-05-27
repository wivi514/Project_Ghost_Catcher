using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Management;

public class InputManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset InputActions;

    private InputAction moveAction;
    private InputAction shootAction;

    private Vector2 m_moveAmt;
    [SerializeField] PlayerMovement playerMovement;
    private Vacuum vacuum;
    [SerializeField] Vacuum vacuumFlatScreen;
    [SerializeField] Vacuum vacuumVR;
    [SerializeField] Camera flatscreenPlayerCamera;

    private bool isVR;

    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable();
    }

    private void Awake()
    {
        //Regarde si le joueur est en vr lorsque le jeu est lancé
        isVR = XRGeneralSettings.Instance.Manager.isInitializationComplete;
        //Trouve la caméra et prend le component player movement si le joueur n'est pas en vr
        if (isVR == false)
        {
            vacuum = vacuumFlatScreen;
            HideCursor();
        }
        else
        {
            vacuum = vacuumVR;
        }
        FindAction();
    }

    private void Update()
    {
        if (isVR == false)
        {
            //Lit la valeur de mouvement de l'input "Move"
            m_moveAmt = moveAction.ReadValue<Vector2>();
        }

        //Lorsque le joueur maintien la touche pour tirer
        if (shootAction.IsPressed())
        {
            vacuum.Attract();
            Debug.LogWarning("Changer pour que ça soit l'inspiration du patient plutôt qu'une touche");
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
    }

    #region cursor
    private void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void ShowCursor()
    {
        Cursor.visible = true;
    }
    #endregion
}
