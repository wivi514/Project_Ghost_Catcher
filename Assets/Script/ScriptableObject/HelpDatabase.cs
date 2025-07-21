using UnityEngine;

//Va contenir toutes les entrées d'aide pour les afficher dans la section aide peut être modifier plus tard pour avoir plusieurs Help Database et séparé en plusieurs sections l'aide
[CreateAssetMenu(fileName = "HelpDatabase", menuName = "Help/Help Database")]
public class HelpDatabase : ScriptableObject
{
    [Tooltip("Ajouter tous les entées d'aide ici ou ceux en lien avec cet database")]
    public HelpEntry[] entries;
}
