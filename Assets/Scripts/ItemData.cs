using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Striptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType{Melee, Range, Glove, Shoe, Heal};
    [Header("# Main Info")]
    public ItemType itemtype;
    public int itemId;
    public string itemName;
    public string itemDesc;
    public Sprite itemIcon;

    [Header("# Level Data")]
    public float baseDamage;
    public int baseCount;
    public float[] damages;
    public int[] counts;

    [Header("# Weapon")]
    public GameObject projectile;
}
