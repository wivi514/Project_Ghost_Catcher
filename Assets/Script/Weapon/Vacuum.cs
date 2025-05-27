using UnityEngine;

public class Vacuum : MonoBehaviour
{
    [Header("Vacuum stats")]
    [Tooltip("Distance maximal que l'aspirateur peut atteindre")]
    [SerializeField] byte range = 15;
    [Tooltip("Force � laquelle l'aspirateur attire des objets")]
    [SerializeField] byte pullSpeed = 15;

    //Distance � laquelle l'objet doit �tre avant qu'il soit accroch� � l'aspirateur
    private byte captureDistance = 1;
    private bool capturing = false;

    private Rigidbody rbObject;
    private GameObject cannonOrientation; //Est plac� au bout du cannon et le X doit �tre positionn� vers ou le canon pointe
    private GameObject lockPosition; //L'endroit ou le fant�me sera coinc� le temps de la capture

    private void Awake()
    {
        assignGameObject();
    }

    private void Update()
    {
        Capturing();
    }

    //Fonction qui permet d'attirer les objets vers le joueur (Fant�me ou n'importe quoi qui � un component rigidbody)
    public void Attract()
    {
        if (capturing != true)
        {
            RaycastHit hit;
            if (Physics.Raycast(cannonOrientation.transform.position, cannonOrientation.transform.forward, out hit, range)) //Lance un raycast ou l'arme pointe
            {
                rbObject = hit.collider.GetComponent<Rigidbody>();
                if (rbObject != null) // S'assure qu'il y a un objet � attirer
                {
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

    // Fonction qui permet de faire en sorte que lorsque le fant�me � capturer est assez proche il le bloque � une certaine position
    private void CaptureLock()
    {
        //D�sactive la physique de l'objet attir� pour emp�cher qu'il tombe pendant la capture.
        rbObject.isKinematic = true;
        rbObject.detectCollisions = false;
        capturing = true;
    }

    //Accroche l'objet au bout de l'arme lorsqu'il est assez pr�t pour la capture
    private void Capturing()
    {
        if (capturing == true && rbObject != null)
        {
                rbObject.transform.position = lockPosition.transform.position;
        }
    }

    private void assignGameObject()
    {
        //Recherche CannonOrientation et lockPosition en tant qu'enfant de l'objet et l'applique donc s'assur� qu'il existe bien sinon erreur
        cannonOrientation = transform.Find("CannonOrientation")?.gameObject;
        if (cannonOrientation == null)
        {
            Debug.LogError("CannonOrientation introuvable!");
        }
        lockPosition = transform.Find("LockPosition")?.gameObject;
        if (lockPosition == null)
        {
            Debug.LogError("LockPosition introuvable!");
        }
    }
}
