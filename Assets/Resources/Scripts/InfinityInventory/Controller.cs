using System.Collections;
using System.Collections.Generic;
using EnhancedUI;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

public class Controller : Inventory,IEnhancedScrollerDelegate
{
    public EnhancedScrollerCellView cellViewPrefab;
    public EnhancedScroller scroller;
    public int numberOfCellsPerRow = 6;
    [SerializeField] private GameObject itemSlotPrefab;

    protected override void Start()
    {
        scroller.Delegate = this;
        base.Start();
        scroller.ReloadData();
    }

    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        return Mathf.CeilToInt((float)itemSlots.Count / (float)numberOfCellsPerRow);
    }

    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return 100f;
    }

    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
       CellView cellView=scroller.GetCellView(cellViewPrefab) as CellView;
       cellView.name = "Cell " + (dataIndex * numberOfCellsPerRow).ToString() + " to " + ((dataIndex * numberOfCellsPerRow) + numberOfCellsPerRow - 1).ToString();
       cellView.SetData(ref currentItems,dataIndex*numberOfCellsPerRow);
       GameObject itemSlotGameObject = Instantiate(itemSlotPrefab, itemsParent, false) as GameObject;
       itemSlots.Add(itemSlotGameObject.GetComponentInChildren<ItemSlot>());
       scroller.ReloadData();
       scroller.Delegate = this;
       return cellView;
    }
}
