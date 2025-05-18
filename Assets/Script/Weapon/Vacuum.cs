using System;
using UnityEngine;

public class Vacuum : MonoBehaviour
{
    [Header("Vacuum stats")]
    //Distance maximal que l'aspirateur peut atteindre
    [SerializeField] byte range = 15;
    //Force � laquelle l'aspirateur attire des objets
    [SerializeField] byte pullSpeed = 15;

    private byte captureDistance = 1;

    private Rigidbody rbObject;
    private GameObject cannonOrientation; //Est plac� au bout du cannon et le X est positionn� vers ou le canon pointe

    private void Awake()
    {
        //Recherche CannonOrientation en tant qu'enfant de l'objet et l'applique donc s'assur� qu'il existe bien sinon erreur
        cannonOrientation = transform.Find("CannonOrientation")?.gameObject;
        if(cannonOrientation == null)
        {
            Debug.LogError("CannonOrientation introuvable!");
        }
    }

    //Fonction qui permet d'attirer les objets vers le joueur
    public void Attract()
    {
        RaycastHit hit;
        if(Physics.Raycast(cannonOrientation.transform.position, cannonOrientation.transform.forward, out hit, range)) //Lance un raycast ou l'arme pointe
        {
            rbObject = hit.collider.GetComponent<Rigidbody>();
            if(rbObject != null) // S'assure qu'il y a un objet � attirer
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

    private void CaptureLock()
    {
        Debug.LogError("Besoin d'impl�menter la fonction CaptureLock");
    }
}
