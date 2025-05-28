using UnityEngine;

public static class TransformUtils
{
    //Permet de donner le path complet du Transform d'un objet dans la scène
    public static string GetFullPath(Transform current)
    {
        if (current == null) return "(null)";

        string path = current.name;
        while (current.parent != null)
        {
            current = current.parent;
            path = current.name + "/" + path;
        }
        return path;
    }
}
