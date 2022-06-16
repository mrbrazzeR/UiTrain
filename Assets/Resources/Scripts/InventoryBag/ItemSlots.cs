using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlots : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] Image Image;
    [SerializeField] ItemTooltip itemTooltip;
    private Item _item;
    public event Action<Item> OnLeftClickEvent;
    public Item Item
    {
        get { return _item;}
        set
        {
            _item = value;
            if (_item == null)
            {
                Image.enabled = false;
            }
            else
            {
                Image.sprite = _item.art;
                Image.enabled = true;
                
            }
        }
    }

    protected virtual void OnValidate()
    {
        if (Image == null)
            Image = GetComponent<Image>();
        if (itemTooltip == null)
            itemTooltip = FindObjectOfType<ItemTooltip>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData != null && eventData.button == PointerEventData.InputButton.Left)
        {
            if (Item != null && OnLeftClickEvent != null)
            {
                OnLeftClickEvent(Item);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(Item is EquipableItem)
        {itemTooltip.ShowTooltip((EquipableItem)Item);}
        else if (Item is Item)
        {
            itemTooltip.ShowTooltip((Item));
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemTooltip.HideToolTip();       
    }
}
