using UnityEngine;

public class InventoryHolder : MonoBehaviour
{
    public BasicInventory inventory;

    public int NumEmptySlots { get => inventory.items.Where(item => item == InventoryItemType.Empty).Count(); }
}
