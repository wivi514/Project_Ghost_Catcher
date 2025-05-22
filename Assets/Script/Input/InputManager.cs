using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovement))]
public class InputManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset InputActions;

    private InputAction moveAction;
    private InputAction shootAction;

    private Vector2 m_moveAmt;
    private PlayerMovement playerMovement;
    private Vacuum vacuum;
    private Camera playerCamera;

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
        playerCamera = Camera.main;
        playerMovement = GetComponent<PlayerMovement>();
        vacuum = transform.Find("Vacuum")?.GetComponent<Vacuum>();
        FindAction();
        HideCursor();
    }

    private void Update()
    {
        //Lit la valeur de mouvement de l'input "Move"
        m_moveAmt = moveAction.ReadValue<Vector2>();

        //Lorsque le joueur maintien la touche pour tirer
        if (shootAction.IsPressed())
        {
            vacuum.Attract();
            Debug.LogWarning("Changer pour que ça soit le souffle du patient plutôt qu'une touche");
        }
    }

    private void FixedUpdate()
    {
        //Movement du joueur (WASD ou stick)
        playerMovement.Move(m_moveAmt.x, m_moveAmt.y, playerCamera);
    }

    //Assigne les InputAction de Unity Input System
    private void FindAction()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        shootAction = InputSystem.actions.FindAction("Attack");
    }

    private void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }
}
