using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    [SerializeField] Image Image;
    private InventoryData _item;


    public InventoryData Item
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

    private void OnValidate()
    {
        if (Image == null)
            Image = GetComponent<Image>();
    }
    
}
