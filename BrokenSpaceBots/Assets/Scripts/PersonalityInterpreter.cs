using System.Linq;
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

    private void Start()
    {
        holder = GetComponent<InventoryHolder>();
        botController = GetComponent<BotController>();
        Reinterpret();
    }

    public void Reinterpret()
    {
        if (holder.inventory.items.SequenceEqual(FIX_HEALTH_STATION))
        {
            botController.CurrentPersonality = BotController.Personality.FIX;
            botController.FocusStationType = "Health";
        }
        else if (holder.inventory.items.SequenceEqual(FIX_WEAPON_STATION))
        {
            botController.CurrentPersonality = BotController.Personality.FIX;
            botController.FocusStationType = "Weapon";
        }
        else if (holder.inventory.items.SequenceEqual(KILL_STATION))
            botController.CurrentPersonality = BotController.Personality.KILL;
        else
        {
            int numEmpty = 0;
            int numBolts = 0;
            int numResistors = 0;
            int numChips = 0;

            foreach (InventoryItemType itemType in holder.inventory.items)
            {
                if (itemType == InventoryItemType.Empty)
                    numEmpty++;
                else if (itemType == InventoryItemType.Resistor)
                    numResistors++;
                else if (itemType == InventoryItemType.Bolt)
                    numBolts++;
                else if (itemType == InventoryItemType.Chip)
                    numChips++;
            }

            if (numBolts > numEmpty && numBolts > numResistors && numBolts > numChips)
                botController.CurrentPersonality = BotController.Personality.SPIN;
            else if (numResistors > numEmpty && numBolts > numResistors && numResistors > numChips)
                botController.CurrentPersonality = BotController.Personality.STARE;
            else if (numChips > numEmpty && numChips > numResistors && numChips > numBolts)
                botController.CurrentPersonality = BotController.Personality.MIMIC;
            else
                botController.CurrentPersonality = BotController.Personality.DISABLED;
        }
    }
}
