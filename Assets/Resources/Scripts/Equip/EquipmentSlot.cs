using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class EquipmentSlot : ItemSlots,IPointerClickHandler
{
    public event Action<Item> OnLeftClick;
    public EquipmentType EquipmentType;
    protected override void OnValidate()
    {
        base.OnValidate();
        gameObject.name = EquipmentType.ToString() + "Slot";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData != null && eventData.button == PointerEventData.InputButton.Left)
        {
            if (Item != null && OnLeftClick != null)
            {
                OnLeftClick(Item);
            }
        }
    }
}
