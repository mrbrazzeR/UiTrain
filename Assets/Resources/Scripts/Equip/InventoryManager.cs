
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] Inventory inventory;

    [SerializeField] EquipmentPanel equipmentPanel;


    private void Awake()
    {
        inventory.OnItemLeftClickedEvent += EquipFromInventory;
        equipmentPanel.OnItemClicked += UnEquipFromInventory;
    }

    private void EquipFromInventory(Item item)
    {
        if (item is EquipableItem)
        {
            Equip((EquipableItem)item);
        }
    }
    private void UnEquipFromInventory(Item item)
    {
        if (item is EquipableItem)
        {
            UnEquip((EquipableItem)item);
        }
    }
   
    public void Equip(EquipableItem item)
    {
        if (inventory.RemoveItem(item))
        {
            EquipableItem previousItem;
            if (equipmentPanel.AddItem(item, out previousItem))
            {
                if (previousItem != null)
                {
                    inventory.AddItem(previousItem);
                }
            }
            else
            {
                inventory.AddItem(item);
            }
        }
    }

    public void UnEquip(EquipableItem item)
    {
        if (!inventory.IsFull() && equipmentPanel.RemoveItem(item))
        {
            inventory.AddItem(item);
        }
        
    }
}
