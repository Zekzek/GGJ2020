using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class InventoryHolder : MonoBehaviour
{
    public BasicInventory inventory;

    public int NumEmptySlots { get => inventory.items.Where(item => item == InventoryItemType.Empty).Count(); }
}
