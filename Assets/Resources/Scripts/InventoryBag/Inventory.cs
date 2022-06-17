using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    [SerializeField] private List<Item> items;
    [SerializeField] private Transform itemsParent;
    [SerializeField] private ItemSlot[] itemSlots;

    public event Action<ItemSlot> OnItemRightClickedEvent;
    public event Action<ItemSlot> OnItemBeginDragEvent;
    public event Action<ItemSlot> OnItemDraggingEvent;
    public event Action<ItemSlot> OnItemEndDragEvent;
    public event Action<ItemSlot> OnItemDroppedEvent;

    [SerializeField] private int currentLastIndex;

    private void Start()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].OnRightClickEvent += OnItemRightClickedEvent;
            itemSlots[i].OnBeginDragEvent += OnItemBeginDragEvent;
            itemSlots[i].OnDragEvent += OnItemDraggingEvent;
            itemSlots[i].OnEndDragEvent += OnItemEndDragEvent;
            itemSlots[i].OnDropEvent += OnItemDroppedEvent;
        }
        RefreshUI();
    }
    private void OnValidate()
    {
        if (itemsParent != null)
        {
            itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();
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

    public bool AddItem(Item item)
    {
        items.Add(item);
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].Item==null)
            {
                itemSlots[i].Item = item;
                RefreshUI();
                Debug.Log("add to inventory");
                return true;
            }
        }
        RefreshUI();
        return false;
    }

    public bool RemoveItem(Item item)
    {
        if (!items.Remove(item)) return false;
        RefreshUI();
        Debug.Log("remove from invent");
        return true;
    }
}
