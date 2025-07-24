using UnityEngine;

public class HelpManager : MonoBehaviour
{
    public HelpDatabase helpDatabase;

    void Start()
    {
        Debug.LogWarning($"Ajouter le UI pour la section aide et ensuite faire la programmation dans le foreach en dessous dans" + TransformUtils.GetFullPath(this.transform));
        foreach (HelpEntry entry in helpDatabase.entries)
        {
            Debug.LogWarning("");
        }
    }
}

