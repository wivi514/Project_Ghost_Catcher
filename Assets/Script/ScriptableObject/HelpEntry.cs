using UnityEngine;

//Scriptable Object pour tout ce qui va être dans le menu Aide/Help
[CreateAssetMenu(fileName = "HelpEntry", menuName = "Help/Help Entry")]
public class HelpEntry : ScriptableObject
{
    [Tooltip("Le titre de l'aide")]
    public string title;
    [Tooltip("La description qui va être afficher lorsque le joueur va appuyer sur le titre")]
    [TextArea]
    public string description;
    [Tooltip("L'image qui va être afficher lorsque le joueur va appuyer sur le titre")]
    public Sprite image;
}
