using UnityEngine;
using UiTrain.CharacterStats;
public class InventoryManager : MonoBehaviour
{
    public CharacterStat Strength;
    public CharacterStat Agility;
    public CharacterStat Intelligence;
    public CharacterStat Vitality;
    
    [SerializeField] Inventory inventory;
    [SerializeField] EquipmentPanel equipmentPanel;
    [SerializeField] StatPanel statPanel;

    private void Awake()
    {
        statPanel.SetStats(Strength,Agility,Intelligence,Vitality);
        statPanel.UpdateStatValues();
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

    public void UnEquip(EquipableItem item)
    {
        if (!inventory.IsFull() && equipmentPanel.RemoveItem(item))
        {
            item.UnEquip(this);
            statPanel.UpdateStatValues();
            inventory.AddItem(item);
        }
        
    }
}
