using UnityEngine;
using UnityEngine.UI;

public class FlexibleGridLayout : LayoutGroup
{
    public int rows;

    public int collumns;

    public Vector2 cellSize;

    public Vector2 spacing;
    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        rows = 3;
        collumns = 6;

        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float cellWidth = (parentWidth / (float) collumns)-((spacing.x/(float)collumns)*2)-(padding.left/(float)collumns)-(padding.right/(float)collumns);
        float cellHeight = (parentHeight / (float)rows) -((spacing.y/(float)rows)*2)-(padding.left/(float)rows)-(padding.right/(float)rows);

        cellSize.x = cellWidth;
        cellSize.y = cellHeight;

        int collumnCount = 0;
        int rowCount = 0;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = i / collumns;
            collumnCount = i % collumns;
            var item = rectChildren[i];

            var xPos = (cellSize.x * collumnCount)+(spacing.x*collumnCount);
            var yPos = (cellSize.y * rowCount)+(spacing.y*rowCount);
            
            SetChildAlongAxis(item,0,xPos,cellSize.x);
            SetChildAlongAxis(item,1,yPos,cellSize.y);
        }
    }

    public override void CalculateLayoutInputVertical()
    {
       
    }

    public override void SetLayoutHorizontal()
    {
        
    }

    public override void SetLayoutVertical()
    {
        
    }
}
