using System.Collections;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerController : MonoBehaviour
{
    public CraftingInventoryScreen botHackScreen;

    public FirstPersonController firstPersonController;
    public UseBotBack useBotBack;
    public ItemPickUp itemPickUp;

    public InventoryHolder inventory;

    private BotController botToHack = null;

    // Start is called before the first frame update
    void Awake()
    {
        useBotBack.OnHackBot += HandleBotHack;
    }

    void HandleBotHack(BotController bot)
    {
        botToHack = bot;

        StopCoroutine("HackingMinigame");
        StartCoroutine(HackingMinigame());
    }

    IEnumerator HackingMinigame()
    {
        SetPlayerActive(false);

        botToHack.CurrentPersonality = BotController.Personality.FREEZE;

        // set up screen
        botHackScreen.player = inventory;
        botHackScreen.bot = botToHack.inventory;

        firstPersonController.m_MouseLook.SetCursorLock(false);
        botHackScreen.gameObject.SetActive(true);
        botHackScreen.Startup();

        while (botHackScreen.gameObject.activeSelf)
            yield return null;


        // TODO Here goes the part where the bot starts acting up again.

        firstPersonController.m_MouseLook.SetCursorLock(true);
        SetPlayerActive(true);
    }

    void SetPlayerActive(bool isPlayerActive)
    {
        itemPickUp.enabled = isPlayerActive;
        useBotBack.enabled = isPlayerActive;
        firstPersonController.enabled = isPlayerActive;
    }
}
