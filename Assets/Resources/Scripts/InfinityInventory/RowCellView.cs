using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RowCellView : ItemSlot
{
    public void SetData(Item data)
    {
        if (data != null)
        {
            Item = data;
            Image.sprite = data.art;
        }
    }
}
