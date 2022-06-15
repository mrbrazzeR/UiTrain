using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum EquipmentType
{
    Helmet,
    Armor,
    Gloves,
    Weapon
}
[CreateAssetMenu]
public class EquipableItem : Item
{
    public EquipmentType EquipmentType;
}
