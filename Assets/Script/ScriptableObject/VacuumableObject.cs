using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VacuumableObject", menuName = "Scriptable Objects/VacuumableObject")]
public class VacuumableObject : ScriptableObject
{
    public string ghostName;
    [Tooltip("Temps n�cessaire pour la capture d'un enemi mettre 0 pour capture instantann� " +
        "si le Fant�me � des mini-jeux associ� � lui mettre au moins un temps minimal pour les faire sinon il n'y aura pas de temps pour les faire.")]
    [Range(0, 100)]
    public int timeToCapture;
    [Tooltip("Offset utilis� pour g�rer ou est-ce que le fant�me sera attacher sur l'aspirateur")]
    public Vector3 positionOffSet;

    // Pour ajouter plus de mini-jeux allez dans Assets/Scripts/Enum et s�lectionner CaptureMinigameType.cs
    [Header("Mini-jeu de capture")]
    [Tooltip("Si le joueur doit utiliser le flash pour permettre la capture du fant�me")]
    public bool requireFlash;
    [Tooltip("Seulement ajouter les mini-jeux une seul fois par mini-jeu " +
        "il ne seront pas choisi par ordre dans la liste mais al�atoirement selon ce qu'il y a dans la liste")]
    public List<CaptureMinigameData> captureMinigames;
}
