using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    private InventoryHolder holder;

    private void Start()
    {
        holder = GetComponent<InventoryHolder>();
    }

    private void Update()
    {
        if (Input.GetAxis("Fire1") > 0 && holder.NumEmptySlots > 0)
            PickUpNearbyItems();
    }

    private void PickUpNearbyItems()
    {
        Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, 1f);
        foreach (Collider collider in nearbyObjects)
        {
            GroundItem groundItem = collider.GetComponent<GroundItem>();
            if (groundItem != null)
            {
                PickUp(groundItem.ItemType);
                collider.gameObject.SetActive(false);
                Destroy(collider.gameObject);
                break;
            }
        }
    }

    private void PickUp(InventoryItemType itemType)
    {
        for (int i = 0; i < holder.inventory.items.Length; i++)
            if (holder.inventory.items[i] == InventoryItemType.Empty)
            {
                holder.inventory.items[i] = itemType;
                break;
            }
    }
}
