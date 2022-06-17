using System;
using UnityEngine;

public class EquipmentPanel : MonoBehaviour
{
    [SerializeField] Transform equipmentSlotsParent;
    [SerializeField] public EquipmentSlot[] equipmentSlots;
   
    public event Action<ItemSlot> OnItemRightClickedEvent;
    public event Action<ItemSlot> OnItemBeginDragEvent;
    public event Action<ItemSlot> OnItemDraggingEvent;
    public event Action<ItemSlot> OnItemEndDragEvent;
    public event Action<ItemSlot> OnItemDroppedEvent;
    
    private void Start()
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            equipmentSlots[i].OnRightClickEvent += OnItemRightClickedEvent;
            equipmentSlots[i].OnBeginDragEvent += OnItemBeginDragEvent; 
            equipmentSlots[i].OnDragEvent += OnItemDraggingEvent;
            equipmentSlots[i].OnEndDragEvent += OnItemEndDragEvent;
            equipmentSlots[i].OnDropEvent += OnItemDroppedEvent;
        }
    }
    private void OnValidate()
    {
        equipmentSlots = equipmentSlotsParent.GetComponentsInChildren<EquipmentSlot>();
    }

    public bool AddItem(EquipableItem item,out EquipableItem previousItem)
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (equipmentSlots[i].EquipmentType == item.EquipmentType)
            {
                previousItem = (EquipableItem) equipmentSlots[i].Item;
                equipmentSlots[i].Item = item;
                return true;
            }
        }
        previousItem = null;
        return false;
    }
    public bool RemoveItem(EquipableItem item)
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (equipmentSlots[i].Item == item)
            {
                equipmentSlots[i].Item = null;
                return true;
            }
        }
        return false;
    }
}
