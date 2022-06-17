using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler,IBeginDragHandler, IEndDragHandler,IDragHandler,
    IDropHandler
{
    [SerializeField] Image Image;
    [SerializeField] ItemTooltip itemTooltip;
    private Item _item;
    public event Action<ItemSlot> OnRightClickEvent;
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDropEvent;

    
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

    public virtual bool CanReceiveItem(Item item)
    {
        return true;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if (OnRightClickEvent != null)
            {
                OnRightClickEvent(this);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(Item is EquipableItem)
        {itemTooltip.ShowTooltip((EquipableItem)Item);}
        else if (!(Item is null))
        {
            itemTooltip.ShowTooltip((Item));
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemTooltip.HideToolTip();       
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (OnBeginDragEvent != null)
        {
            OnBeginDragEvent(this);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (OnDragEvent != null)
        {
            OnDragEvent(this);
        }
    }

    
    public void OnEndDrag(PointerEventData eventData)
    {
        if (OnEndDragEvent != null)
        {
            OnEndDragEvent(this);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (OnDropEvent != null)
        {
            OnDropEvent(this);
        }
    }
}
