using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlots : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] Image Image;
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
}
