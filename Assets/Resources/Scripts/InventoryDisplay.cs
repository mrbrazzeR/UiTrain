using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{

    [SerializeField] private List<InventoryData> inventory;

    [SerializeField] private Transform itemsParent;

    [SerializeField] private ItemDisplay[] itemSlots;

    private void OnValidate()
    {
        if (itemsParent != null)
        {
            itemSlots = itemsParent.GetComponentsInChildren<ItemDisplay>();
        }
        RefreshUI();
    }

    private void RefreshUI()
    {
        int i = 0;
        for (; i < inventory.Count && i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = inventory[i];
        }

        for (; i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = null;
        }
    }
}
