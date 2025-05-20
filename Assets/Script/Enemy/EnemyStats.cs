using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Tooltip("Mettre le ScriptableObject qui � les donn�es sur l'enemi voulu")]
    [SerializeField] VacuumableObject vacuumableObject;
    [HideInInspector]
    public int timeToCapture;
    [HideInInspector]
    public Vector3 positionOffSet;

    private void Awake()
    {
        timeToCapture = vacuumableObject.timeToCapture;
        positionOffSet = vacuumableObject.positionOffSet;
    }
}
