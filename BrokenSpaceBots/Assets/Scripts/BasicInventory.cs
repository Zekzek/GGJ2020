using System;

[Serializable]
public class BasicInventory
{
    public const int ROW_WIDTH = 3;

    // Publicly accessible version of the items
    public InventoryItemType[] items;

    public InventoryItemType GetItemInGrid(int x, int y)
    {
        if (x < 0 || y < 0 || x >= ROW_WIDTH)
        {
            throw new ArgumentException();
        }

        int index = ROW_WIDTH * y + x;
        if (index >= items.Length)
        {
            throw new ArgumentOutOfRangeException("Out of bounds [" + x + "," + y + "]");
        }

        return items[index];
    }
    public bool IsInBounds(int x, int y)
    {
        int index = ROW_WIDTH * y + x;
        return index >= 0 && index < items.Length;
    }
}
