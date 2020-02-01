using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingInventoryScreen : MonoBehaviour
{
    public InventoryGrid botInventoryGrid;
    public InventoryGrid playerInventoryGrid;


    public InventoryHolder bot;
    public InventoryHolder player;

    // Start is called before the first frame update
    void Start()
    {
        Populate();
    }

    private void Populate()
    {
        botInventoryGrid.SetUpInventory(bot.inventory);
        playerInventoryGrid.SetUpInventory(player.inventory);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
