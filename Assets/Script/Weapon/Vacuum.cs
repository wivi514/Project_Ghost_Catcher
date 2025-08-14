using UnityEngine;

public class Vacuum : MonoBehaviour
{
    [Header("Game Manager")]
    [SerializeField] GameManager gameManager;

    [Header("Vacuum stats")]
    [Tooltip("Distance maximal que l'aspirateur peut atteindre")]
    [SerializeField] byte range = 15;
    [Tooltip("Force à laquelle l'aspirateur attire des objets")]
    [SerializeField] byte pullSpeed = 15;
    [SerializeField] private ParticleSystem airVacuum;

    //Distance à laquelle l'objet doit être avant qu'il soit accroché à l'aspirateur
    private byte captureDistance = 1;
    private bool capturing = false;

    private Rigidbody rbObject;
    [HideInInspector]
    public GameObject cannonOrientation; //Est placé au bout du cannon et le X doit être positionné vers ou le canon pointe
    private GameObject lockPosition; //L'endroit ou le fantôme sera coincé le temps de la capture

    private void Awake()
    {
        if (gameManager == null)
        {
            Debug.LogWarning($"Assigner la référence à GameManager sur {TransformUtils.GetFullPath(this.transform)} pour meilleur performance");
            gameManager = FindFirstObjectByType<GameManager>();
        }
    }

    private void Start()
    {
        assignGameObject();
    }

    private void Update()
    {
        Capturing();
    }

    //Fonction qui permet d'attirer les objets vers le joueur (Fantôme ou n'importe quoi qui à un component rigidbody)
    public void Attract()
    {
        bool objectInRange = false;

        if ( gameManager.isVR)
        {
            if (capturing != true)
            {
                RaycastHit hit;
                if (Physics.Raycast(cannonOrientation.transform.position, cannonOrientation.transform.forward, out hit, range)) //Lance un raycast ou l'arme pointe
                {
                    rbObject = hit.collider.GetComponent<Rigidbody>();
                    if (rbObject != null) // S'assure qu'il y a un objet à attirer
                    {
                        objectInRange = true;

                        Vector3 pullDirection = (cannonOrientation.transform.position - hit.transform.position).normalized; // Calcule la direction que l'objet doit aller pour aller vers l'aspirateur
                        float distanceToVacuum = Vector3.Distance(hit.transform.position, cannonOrientation.transform.position); //Calcule la distance entre l'objet et l'aspirateur pour savoir quand il doit entrer en mode capture

                        if (distanceToVacuum > captureDistance)
                        {
                            rbObject.AddForce(pullDirection * (pullSpeed / rbObject.mass) * Time.deltaTime, ForceMode.VelocityChange); // Attire l'objet vers le joueur                          
                        }
                        else
                        {
                            CaptureLock();
                        }
                    }
                }
            }
        }
        else
        {
            if (capturing != true)
            {
                Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, range)) //Lance un raycast ou le joueur regarde
                {
                    rbObject = hit.collider.GetComponent<Rigidbody>();
                    if (rbObject != null) // S'assure qu'il y a un objet à attirer
                    {
                        objectInRange = true;

                        Vector3 pullDirection = (cannonOrientation.transform.position - hit.transform.position).normalized; // Calcule la direction que l'objet doit aller pour aller vers l'aspirateur
                        float distanceToVacuum = Vector3.Distance(hit.transform.position, cannonOrientation.transform.position); //Calcule la distance entre l'objet et l'aspirateur pour savoir quand il doit entrer en mode capture

                        if (distanceToVacuum > captureDistance)
                        {
                            rbObject.AddForce(pullDirection * (pullSpeed / rbObject.mass) * Time.deltaTime, ForceMode.VelocityChange); // Attire l'objet vers le joueur
                        }
                        else
                        {
                            CaptureLock();
                        }
                    }
                }
            }
        }

        // Démarre le particle system d'aspirateur si l'object est in range
        if (objectInRange)
        {
            if (!airVacuum.isPlaying)
                airVacuum.Play();
        }
        else
        {
            if (airVacuum.isPlaying)
                airVacuum.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

    }

    // Fonction qui permet de faire en sorte que lorsque le fantôme à capturer est assez proche il le bloque à une certaine position
    private void CaptureLock()
    {
        //Désactive la physique de l'objet attiré pour empêcher qu'il tombe pendant la capture.
        rbObject.isKinematic = true;
        rbObject.detectCollisions = false;
        capturing = true;
        rbObject.GetComponent<EnemyBehaviour>().LaunchNextMinigame();
    }

    //Accroche l'objet au bout de l'arme lorsqu'il est assez prêt pour la capture
    private void Capturing()
    {
        if (capturing == true && rbObject != null)
        {
                rbObject.transform.position = lockPosition.transform.position;
        }
        else
        {
            capturing = false;
        }
    }

    private void assignGameObject()
    {
        cannonOrientation = gameManager.getCannonOrientation();
        if (cannonOrientation == null)
        {
            Debug.LogError("cannonOrientation introuvable!");
        }
        lockPosition = transform.Find("LockPosition")?.gameObject;
        if (lockPosition == null)
        {
            Debug.LogError("LockPosition introuvable!");
        }
    }
}
