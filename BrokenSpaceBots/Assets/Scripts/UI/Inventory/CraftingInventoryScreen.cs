using UnityEngine;
using UnityEngine.UI;

public class CraftingInventoryScreen : MonoBehaviour
{
    public Image cursor;

    public InventoryItemIconRef iconRef;

    public InventoryGrid botInventoryGrid;
    public InventoryGrid playerInventoryGrid;

    public InventoryHolder bot;
    public InventoryHolder player;

    private InventoryItemType cursorItem;
    private InventoryGridCell lastSelectedCell;

    // Start is called before the first frame update
    void Start()
    {
        ResetScreen();

        foreach (InventoryGridCell cell in botInventoryGrid.cells)
        {
            cell.OnGridCellClick += HandleGridCellClick;
        }

        foreach (InventoryGridCell cell in playerInventoryGrid.cells)
        {
            cell.OnGridCellClick += HandleGridCellClick;
        }

        Populate();
    }
    private void OnEnable()
    {
        ResetScreen();

        Populate();
    }
    private void ResetScreen()
    {
        cursorItem = InventoryItemType.Empty;
        cursor.sprite = iconRef.emptyIconRef;
        lastSelectedCell = null;
    }

    private void Populate()
    {
        botInventoryGrid.SetUpWithInventory(bot.inventory);
        playerInventoryGrid.SetUpWithInventory(player.inventory);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HandleGridCellClick(InventoryGridCell cell)
    {
        InventoryItemType oldItem = cursorItem;
        InventoryItemType newItem = cell.GetItem();

        if (oldItem != newItem)
        {
            // Select item into new cursor
            cursorItem = newItem;
            cursor.sprite = iconRef.GetSpriteForItem(newItem);

            // Swap old cursor item into inventory
            cell.SetItem(oldItem);


            lastSelectedCell = cell; // mostly debugging
        }
    }
}
