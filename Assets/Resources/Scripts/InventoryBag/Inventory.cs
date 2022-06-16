using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    [SerializeField] private List<Item> items;
    [SerializeField] private Transform itemsParent;
    [SerializeField] private ItemSlots[] itemSlots;

    public event Action<Item> OnItemLeftClickedEvent;



    private void Start()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].OnLeftClickEvent += OnItemLeftClickedEvent;
        }
    }
    private void OnValidate()
    {
        if (itemsParent != null)
        {
            itemSlots = itemsParent.GetComponentsInChildren<ItemSlots>();
        }
        RefreshUI();
    }
    private void RefreshUI()
    {
        int i = 0;
        for (; i < items.Count && i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = items[i];
        }

        for (; i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = null;
        }
    }

    public bool AddItem(EquipableItem item)
    {
        items.Add(item);
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i]!=null)
            {
                EquipableItem currenItem = (EquipableItem)itemSlots[i].Item;
                if (currenItem.EquipmentType == item.EquipmentType)
                {
                    
                    itemSlots[i].Item = item;
                    
                }
                RefreshUI();
                return true;
            }
        }

        return false;
    }

    public bool RemoveItem(Item item)
    {
        if (items.Remove(item))
        {
            RefreshUI();
            return true;
        }

        return false;
    }
    
    public bool IsFull()
    {
        return items.Count >= itemSlots.Length;
    }

    
}
