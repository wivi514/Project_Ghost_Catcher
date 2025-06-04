using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemData", menuName = "Scriptable Objects/ShopItemData")]
public class ShopItemData : ScriptableObject
{
    [Tooltip("Nom de l'objet")]
    [SerializeField] string itemName;
    [Tooltip("Description de l'objet")]
    [TextArea][SerializeField] string description;
    [Tooltip("Icon de l'objet")]
    [SerializeField] Sprite icon;
    [Tooltip("Prix de l'objet")]
    [SerializeField] int price;
    [Tooltip("Type de l'objet")]
    [SerializeField] ShopItemType type;
    public enum ShopItemType
    {
        Cosmetic,
        Upgrade
    }
}
