using UnityEngine;

public class LootBag : MonoBehaviour
{
    private Item[] items;
    private InventoryManager inventoryManager;
    private PartyManager partyManager;

    public void Init(Item[] items, InventoryManager invManager, PartyManager ptyManager)
    {
        this.items = (Item[])items.Clone();
        inventoryManager = invManager;
        partyManager = ptyManager;
    }

    public void PickUp()
    {
        Characters hero = partyManager.SelectChars.Count > 0
            ? partyManager.SelectChars[0]
            : (partyManager.Members.Count > 0 ? partyManager.Members[0] : null);

        if (hero == null) return;

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null && inventoryManager.AddItem(hero, items[i].ID))
                items[i] = null;
        }

        if (IsEmpty())
            Destroy(gameObject);
    }

    private bool IsEmpty()
    {
        foreach (Item item in items)
            if (item != null) return false;
        return true;
    }
}
