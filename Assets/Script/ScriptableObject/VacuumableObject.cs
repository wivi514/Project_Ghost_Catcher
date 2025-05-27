using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VacuumableObject", menuName = "Scriptable Objects/VacuumableObject")]
public class VacuumableObject : ScriptableObject
{
    public string ghostName;
    [Tooltip("Temps nécessaire pour la capture d'un enemi mettre 0 pour capture instantanné " +
        "si le Fantôme à des mini-jeux associé à lui mettre au moins un temps minimal pour les faire sinon il n'y aura pas de temps pour les faire.")]
    [Range(0, 100)]
    public int timeToCapture;
    [Tooltip("Offset utilisé pour gérer ou est-ce que le fantôme sera attacher sur l'aspirateur")]
    public Vector3 positionOffSet;

    // Pour ajouter plus de mini-jeux allez dans Assets/Scripts/Enum et sélectionner CaptureMinigameType.cs
    [Header("Mini-jeu de capture")]
    [Tooltip("Si le joueur doit utiliser le flash pour permettre la capture du fantôme")]
    public bool requireFlash;
    [Tooltip("Seulement ajouter les mini-jeux une seul fois par mini-jeu " +
        "il ne seront pas choisi par ordre dans la liste mais aléatoirement selon ce qu'il y a dans la liste")]
    public List<CaptureMinigameData> captureMinigames;
}
