using UnityEngine;
using UiTrain.CharacterStats;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public CharacterStat Strength;
    public CharacterStat Agility;
    public CharacterStat Intelligence;
    public CharacterStat Vitality;

    [SerializeField] Inventory inventory;
    [SerializeField] EquipmentPanel equipmentPanel;
    [SerializeField] StatPanel statPanel;

    [SerializeField] Image dragImage;
    private ItemSlot draggedSlot;
    private Item draggedItem;

    private void Awake()
    {
        statPanel.SetStats(Strength, Agility, Intelligence, Vitality);
        statPanel.UpdateStatValues();
        inventory.OnItemDroppedEvent += OnDrop;
        equipmentPanel.OnItemDroppedEvent += OnDrop;

        inventory.OnItemRightClickedEvent += Equip;
        equipmentPanel.OnItemRightClickedEvent += UnEquip;

        inventory.OnItemBeginDragEvent += BeginDrag;
        equipmentPanel.OnItemBeginDragEvent += BeginDrag;

        inventory.OnItemDraggingEvent += OnDrag;
        equipmentPanel.OnItemDraggingEvent += OnDrag;

        inventory.OnItemEndDragEvent += EndDrag;
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
            dragImage.sprite = itemSlot.Item.art;
            dragImage.transform.position = Input.mousePosition;
            dragImage.enabled = true;
        }
    }

    private void OnDrag(ItemSlot itemSlot)
    {
        if (dragImage != null)
            dragImage.transform.position = Input.mousePosition;
    }

    private void EndDrag(ItemSlot itemSlot)
    {
        draggedSlot = null;
        dragImage.enabled = false;
    }

    private void OnDrop(ItemSlot itemSlot)
    {
        if (itemSlot.CanReceiveItem(draggedSlot.Item) && draggedSlot.CanReceiveItem(itemSlot.Item))
        {
            EquipableItem dragItem = draggedSlot.Item as EquipableItem;
            EquipableItem dropItem = itemSlot.Item as EquipableItem;
            if (draggedSlot is EquipmentSlot)
            {
                if (dragItem != null) dragItem.UnEquip(this);
                if (dropItem != null) dropItem.Equip(this);
            }

            if (itemSlot is EquipmentSlot)
            {
                if (dragItem != null) dragItem.Equip(this);
                if (dropItem != null) dropItem.UnEquip(this);
            }

            Item dragged = draggedSlot.Item;
            draggedSlot.Item = itemSlot.Item;
            itemSlot.Item = dragged;
        }
    }
}