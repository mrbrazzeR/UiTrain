using UnityEngine;
using UiTrain.CharacterStats;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public CharacterStat Strength;
    public CharacterStat Agility;
    public CharacterStat Intelligence;
    public CharacterStat Vitality;

    [SerializeField] InfinityInventory InfinityInventory;
    [SerializeField] Inventory inventory;
    [SerializeField] EquipmentPanel equipmentPanel;
    [SerializeField] StatPanel statPanel;

    [SerializeField] Image draggableImage;
    private ItemSlot draggedSlot;

    private void Awake()
    {
        statPanel.SetStats(Strength, Agility, Intelligence, Vitality);
        statPanel.UpdateStatValues();
        inventory.OnItemRightClickedEvent += Equip;
        InfinityInventory.OnItemRightClickedEvent += Equip;
        equipmentPanel.OnItemRightClickedEvent += UnEquip;

        inventory.OnItemBeginDragEvent += BeginDrag;
        InfinityInventory.OnItemBeginDragEvent += BeginDrag;
        equipmentPanel.OnItemBeginDragEvent += BeginDrag;

        inventory.OnDragEvent += Drag;
        InfinityInventory.OnDragEvent += Drag;
        equipmentPanel.OnItemDraggingEvent += Drag;

        inventory.OnItemDroppedEvent += Drop;
        InfinityInventory.OnItemDroppedEvent += Drop;
        equipmentPanel.OnItemDroppedEvent += Drop;
        
        inventory.OnItemEndDragEvent += EndDrag;
        InfinityInventory.OnItemEndDragEvent += EndDrag;
        equipmentPanel.OnItemEndDragEvent += EndDrag;
    }


    private void Equip(ItemSlot itemSlot)
    {
        EquipableItem equipableItem = itemSlot.Item as EquipableItem;
        if (equipableItem != null)
        {
            Equip(equipableItem);
        }
    }

    private void Equip(EquipableItem item)
    {
        if (inventory.RemoveItem(item))
        {
            EquipableItem previousItem;
            if (equipmentPanel.AddItem(item, out previousItem))
            {
                if (previousItem != null)
                {
                    inventory.AddItem(previousItem);
                    previousItem.UnEquip(this);
                    statPanel.UpdateStatValues();
                }

                item.Equip(this);
                statPanel.UpdateStatValues();
            }
            else
            {
                inventory.AddItem(item);
            }
        }
        if (InfinityInventory.RemoveItem(item))
        {
            EquipableItem previousItem;
            if (equipmentPanel.AddItem(item, out previousItem))
            {
                if (previousItem != null)
                {
                    InfinityInventory.AddItem(previousItem);
                    previousItem.UnEquip(this);
                    statPanel.UpdateStatValues();
                }

                item.Equip(this);
                statPanel.UpdateStatValues();
            }
            else
            {
                InfinityInventory.AddItem(item);
            }
        }
    }

    private void UnEquip(ItemSlot itemSlot)
    {
        EquipableItem equipableItem = itemSlot.Item as EquipableItem;
        if (equipableItem != null)
        {
            UnEquip(equipableItem);
        }
    }

    private void UnEquip(EquipableItem item)
    {
        if (equipmentPanel.RemoveItem(item))
        {
            item.UnEquip(this);
            statPanel.UpdateStatValues();
            inventory.AddItem(item);
        }
    }


    private void BeginDrag(ItemSlot itemSlot)
    {
        if (itemSlot.Item != null)
        {
            draggedSlot = itemSlot;
            draggableImage.sprite = itemSlot.Item.art;
            draggableImage.transform.position = Input.mousePosition;
            draggableImage.enabled = true;
        }
    }
    private void EndDrag(ItemSlot itemSlot)
    {
        draggedSlot = null;
        draggableImage.enabled = false;
    }
    private void Drag(ItemSlot itemSlot)
    {
        if (draggableImage.enabled)
        {
            draggableImage.transform.position = Input.mousePosition;
        }
    }
    private void Drop(ItemSlot dropItemSlot)
    {
        if (dropItemSlot.CanReceiveItem(draggedSlot.Item) && draggedSlot.CanReceiveItem(dropItemSlot.Item))
        {
            EquipableItem dragItem = draggedSlot.Item as EquipableItem;
            EquipableItem dropItem = dropItemSlot.Item as EquipableItem;
            if (draggedSlot is EquipmentSlot)
            {
                if (dragItem != null) dragItem.Equip(this);
                if (dropItem != null) dropItem.UnEquip(this);
            }

            if (dropItemSlot is EquipmentSlot)
            {
                if (dragItem != null) dragItem.Equip(this);
                if (dropItem != null) dropItem.UnEquip(this);
            }
            statPanel.UpdateStatValues();
            Item dragged = draggedSlot.Item;
            draggedSlot.Item = dropItemSlot.Item;
            dropItemSlot.Item = dragged;
        }
    }
}