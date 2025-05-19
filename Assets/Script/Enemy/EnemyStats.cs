using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] VacuumableObject vacuumableObject;
    [HideInInspector]
    public byte timeToCapture;
    [HideInInspector]
    public Vector3 positionOffSet;

    private void Awake()
    {
        timeToCapture = vacuumableObject.timeToCapture;
        positionOffSet = vacuumableObject.positionOffSet;
    }
}
