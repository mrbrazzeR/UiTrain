using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    
    [SerializeField] protected List<Item> startingItems;
    [SerializeField] protected List<Item> currentItems;
    [SerializeField] protected Transform itemsParent;
    [SerializeField] protected List<ItemSlot> itemSlots;

    [SerializeField] private Button addButton;
    [SerializeField] private Button removeButton;
    public event Action<ItemSlot> OnItemRightClickedEvent;
    public event Action<ItemSlot> OnItemBeginDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnItemEndDragEvent;
    public event Action<ItemSlot> OnItemDroppedEvent;
    

    protected virtual void Start()
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            itemSlots[i].OnRightClickEvent += OnItemRightClickedEvent;
            itemSlots[i].OnBeginDragEvent += OnItemBeginDragEvent;
            itemSlots[i].OnDragEvent += OnDragEvent;
            itemSlots[i].OnEndDragEvent += OnItemEndDragEvent;
            itemSlots[i].OnDropEvent += OnItemDroppedEvent;
        }

        currentItems.RemoveAll(CanRemove);
        SetCurrentItems();
        addButton.onClick.AddListener(AddItemToInventory);
        removeButton.onClick.AddListener(RemoveItemFromInventory);
    }
    private void OnValidate()
    {
        if (itemsParent != null)
        {
            itemsParent.GetComponentsInChildren(true, itemSlots);
        }
    }

    private void SetCurrentItems()
    {
        int i = 0;
        for (; i < currentItems.Count && i<itemSlots.Count; i++)
        {
            itemSlots[i].Item = currentItems[i];
        }

        for (; i < itemSlots.Count; i++)
        {
            itemSlots[i].Item = null;
        }
    }

    private void AddItemToInventory()
    {
        currentItems.Add(startingItems[Random.Range(0,startingItems.Count)]);
        SetCurrentItems();
    }

    private void RemoveItemFromInventory()
    {
        if(currentItems.Count>0) 
            currentItems.RemoveAt(currentItems.Count-1);
        SetCurrentItems();
    }
    public bool AddItem(Item item)
    {
        currentItems.Add(item);
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (itemSlots[i].Item==null)
            {
                itemSlots[i].Item = item;
                SetCurrentItems();
                return true;
            }
        }
        SetCurrentItems();
        return false;
    }

    public bool RemoveItem(Item item)
    {
        if (!currentItems.Remove(item)) return false;
        SetCurrentItems();
        return true;
    }

    public bool CanRemove(Item item)
    {
        return true;
    }
    
    
    
}
