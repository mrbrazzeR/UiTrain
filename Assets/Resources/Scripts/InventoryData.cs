using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "inventory_data",menuName = "Resources/ScriptableObjects/items")]
public class InventoryData:ScriptableObject
{
    public int id;
    public string name;
    public Sprite art;
    public int value;
}
