using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "inventory_data",menuName = "Resources/ScriptableObjects/inventory_data")]
public class InventoryCollection : ScriptableObject
{
    public List<Item> dataGroups;
    
}
