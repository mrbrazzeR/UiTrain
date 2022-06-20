using System;
using System.Collections;
using System.Collections.Generic;
using EnhancedUI;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

public class CellView : EnhancedScrollerCellView
{
    public RowCellView[] rowCellViews;

    public void SetData(ref List<Item> data, int startingIndex)
    {
        for(var i=0;i<rowCellViews.Length;i++)
        {
            rowCellViews[i].SetData(startingIndex + i < data.Count ? data[startingIndex + i] : null);
        }
    }
    }
