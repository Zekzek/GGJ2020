using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PersonalityInterpreter : MonoBehaviour
{
    private static readonly InventoryItemType[] FIX_HEALTH_STATION = {
        InventoryItemType.Bolt, InventoryItemType.Empty, InventoryItemType.Bolt,
        InventoryItemType.Empty, InventoryItemType.Chip, InventoryItemType.Empty,
        InventoryItemType.Bolt, InventoryItemType.Empty, InventoryItemType.Bolt
    };

    private static readonly InventoryItemType[] FIX_WEAPON_STATION = {
        InventoryItemType.Empty, InventoryItemType.Resistor, InventoryItemType.Empty,
        InventoryItemType.Bolt, InventoryItemType.Chip, InventoryItemType.Bolt,
        InventoryItemType.Bolt, InventoryItemType.Chip, InventoryItemType.Bolt
    };

    private static readonly InventoryItemType[] KILL_STATION = {
        InventoryItemType.Empty, InventoryItemType.Empty, InventoryItemType.Empty,
        InventoryItemType.Bolt, InventoryItemType.Chip, InventoryItemType.Bolt,
        InventoryItemType.Bolt, InventoryItemType.Chip, InventoryItemType.Bolt
    };

    private InventoryHolder holder;
    private BotController botController;



    // Start is called before the first frame update
    void Start()
    {
        holder = GetComponent<InventoryHolder>();
        botController = GetComponent<BotController>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if(holder.inventory.items.SequenceEqual(FIX_HEALTH_STATION))
        {
            botController.CurrentPersonality = BotController.Personality.FIX;
            botController.FocusStationType = "Health";
        }
        else if(holder.inventory.items.SequenceEqual(FIX_WEAPON_STATION))
        {
            botController.CurrentPersonality = BotController.Personality.FIX;
            botController.FocusStationType = "Weapon";
        }
        else if (holder.inventory.items.SequenceEqual( KILL_STATION))
            botController.CurrentPersonality = BotController.Personality.KILL;
        else
            botController.CurrentPersonality = BotController.Personality.DISABLED;
    }
}
