using UnityEngine;

[CreateAssetMenu(fileName = "VacuumableObject", menuName = "Scriptable Objects/VacuumableObject")]
public class VacuumableObject : ScriptableObject
{
    public byte timeToCapture;
    public Vector3 positionOffSet;
}
