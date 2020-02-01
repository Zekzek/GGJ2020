using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryGridCell : MonoBehaviour, IPointerClickHandler
{
    public InventoryItemIconRef iconRef;
    public Image icon;

    [HideInInspector]
    public BasicInventory inventory;
    private int x, y;

    public event Action<InventoryGridCell> OnGridCellClick = delegate { }; 

    // Start is called before the first frame update
    void Start()
    {
    }

    public void SetGridCell(BasicInventory inventory, int x, int y)
    {
        this.inventory = inventory;
        this.x = x;
        this.y = y;

        UpdateCell();
    }

    public void UpdateCell()
    {
        UpdateIcon(inventory.GetItemInGrid(x,y));
    }

    public void ClearCell()
    {
        inventory = null;
        icon.sprite = iconRef.emptyIconRef;
    }

    private void UpdateIcon(InventoryItemType item)
    {
        icon.sprite = iconRef.GetSpriteForItem(item);
    }

    public InventoryItemType GetItem()
    {
        return inventory.GetItemInGrid(x,y);
    }

    public InventoryItemType SetItem(InventoryItemType item)
    {
        InventoryItemType oldItem = inventory.GetItemInGrid(x, y);
        inventory.SetItemInGrid(x,y,item);

        UpdateCell();

        return oldItem;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnGridCellClick(this);
    }
}
