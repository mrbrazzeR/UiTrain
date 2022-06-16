using UiTrain.CharacterStats;
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
    
    [Space]
    public EquipmentType EquipmentType;

    public void Equip(InventoryManager inventory)
    {
        if (strength != 0)
        {
            inventory.Strength.AddModifier(new StatModifier(strength,StatModifierType.Flat,this));
        }
        if (agility != 0)
        {
            inventory.Agility.AddModifier(new StatModifier(agility,StatModifierType.Flat,this));
        }
        if (intelligence != 0)
        {
            inventory.Intelligence.AddModifier(new StatModifier(intelligence,StatModifierType.Flat,this));
        }
        if (vitality != 0)
        {
            inventory.Vitality.AddModifier(new StatModifier(vitality,StatModifierType.Flat,this));
        }
    }
    public void UnEquip(InventoryManager inventory)
    {
        inventory.Strength.RemoveAllModifiersFromSource(this);
        inventory.Agility.RemoveAllModifiersFromSource(this); 
        inventory.Intelligence.RemoveAllModifiersFromSource(this);
        inventory.Vitality.RemoveAllModifiersFromSource(this);
    }
}
