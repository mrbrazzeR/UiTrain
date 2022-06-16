using UnityEngine;

[CreateAssetMenu(fileName = "item",menuName = "Resources/ScriptableObjects/items")]
public class Item:ScriptableObject
{
    public int id;
    public string itemName;
    public Sprite art;
    public int strength;
    public int agility;
    public int intelligence;
    public int vitality;
}

