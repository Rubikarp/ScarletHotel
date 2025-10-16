using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class FlexGridLayout : LayoutGroup
{
    public Vector2Int size = 2 * Vector2Int.one; 
    public Vector2 spacing = Vector2.zero;
    [Space]
    public int numberOfCellsInScreen = 4;
    public override void SetLayoutHorizontal()
    {
        CalculateLayoutInputHorizontal();
    }
    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        // Calculate the number of rows and columns based on the number of cells
        size.x = Mathf.CeilToInt((float)numberOfCellsInScreen / size.y);

        float cellWidth = (rectTransform.rect.width - padding.left - padding.right - (spacing.x * (size.y - 1))) / size.y;
        float cellHeight = (rectTransform.rect.height - padding.top - padding.bottom - (spacing.y * (size.x - 1))) / size.x;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            int row = i / size.y;
            int column = i % size.y;

            var item = rectChildren[i];

            float xPos = padding.left + (cellWidth + spacing.x) * column;
            float yPos = padding.top + (cellHeight + spacing.y) * row;

            SetChildAlongAxis(item, 0, xPos, cellWidth);
            SetChildAlongAxis(item, 1, yPos, cellHeight);
        }
    }

    public override void SetLayoutVertical()
    {
        CalculateLayoutInputHorizontal();
    }
    public override void CalculateLayoutInputVertical()
    {
        // No additional vertical calculation needed
    }
}
