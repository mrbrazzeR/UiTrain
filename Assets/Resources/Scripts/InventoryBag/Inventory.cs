using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Inventory : MonoBehaviour
{
    
    [SerializeField] private List<Item> startingItems;
    [SerializeField] private Transform itemsParent;
    [SerializeField] private ItemSlot[] itemSlots;

    public event Action<ItemSlot> OnItemRightClickedEvent;
    public event Action<ItemSlot> OnItemBeginDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnItemEndDragEvent;
    public event Action<ItemSlot> OnItemDroppedEvent;

    [SerializeField] private int currentLastIndex;

    private void Start()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].OnRightClickEvent += OnItemRightClickedEvent;
            itemSlots[i].OnBeginDragEvent += OnItemBeginDragEvent;
            itemSlots[i].OnDragEvent += OnDragEvent;
            itemSlots[i].OnEndDragEvent += OnItemEndDragEvent;
            itemSlots[i].OnDropEvent += OnItemDroppedEvent;
        }
        SetStartingItems();
    }
    private void OnValidate()
    {
        if (itemsParent != null)
        {
            itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();
        }
        SetStartingItems();
    }
    private void SetStartingItems()
    {
        int i = 0;
        for (; i < startingItems.Count && i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = startingItems[i];
        }

        for (; i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = null;
        }
    }

    public bool AddItem(Item item)
    {
        startingItems.Add(item);
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].Item==null)
            {
                itemSlots[i].Item = item;
                SetStartingItems();
                return true;
            }
        }
        SetStartingItems();
        return false;
    }

    public bool RemoveItem(Item item)
    {
        if (!startingItems.Remove(item)) return false;
        SetStartingItems();
        return true;
    }
}
