using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemIconRef : MonoBehaviour
{
    public Sprite emptyIconRef, boltIconRef, resistorIconRef, chipIconRef;

    public Sprite GetSpriteForItem(InventoryItemType item)
    {
        switch (item)
        {
            case InventoryItemType.Empty:
                return emptyIconRef;
            case InventoryItemType.Bolt:
                return boltIconRef;
            case InventoryItemType.Chip:
                return chipIconRef;
            case InventoryItemType.Resistor:
                return resistorIconRef;
            default:
                Debug.LogError("Unknown type passed in for item type: " + item.ToString() + ". No icon could be made.");
                return null;
        }
    }
}
