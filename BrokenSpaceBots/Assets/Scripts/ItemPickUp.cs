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
        Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, 2f);
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
        holder.inventory.AddItemInEmptySlot(itemType);
    }
}
