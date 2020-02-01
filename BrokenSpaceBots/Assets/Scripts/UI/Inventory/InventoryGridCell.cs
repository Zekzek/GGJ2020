using UnityEngine;
using UnityEngine.UI;

public class InventoryGridCell : MonoBehaviour
{
    public InventoryItemIconRef iconRef;
    public Image icon;

    // Start is called before the first frame update
    void Start()
    {
    }
    public void SetItem(InventoryItemType item)
    {
        switch(item)
        {
            case InventoryItemType.Empty:
                icon.sprite = iconRef.emptyIconRef;
                break;
            case InventoryItemType.Bolt:
                icon.sprite = iconRef.boltIconRef;
                break;
            case InventoryItemType.Chip:
                icon.sprite = iconRef.chipIconRef;
                break;
            case InventoryItemType.Resistor:
                icon.sprite = iconRef.resistorIconRef;
                break;
            default:
                Debug.LogError("Unknown type passed in for item type: " + item.ToString() + ". No icon could be made.");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
