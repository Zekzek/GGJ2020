using UnityEngine;

public class GroundItem : MonoBehaviour
{
    [SerializeField]
    private InventoryItemType itemType;
    public InventoryItemType ItemType { get => itemType; }
}
