using UnityEngine;
using System.Collections.Generic;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] itemPrefabs;
    public GameObject[] ItemPrefabs { get { return itemPrefabs; } set { itemPrefabs = value; } }

    [SerializeField]
    private ItemData[] itemData;
    public ItemData[] ItemData { get { return itemData; } set { itemData = value; } }

    public const int MAXSLOT = 18;

    public static InventoryManager instance;

    void Awake()
    {
        instance = this;
    }

    public bool AddItem(Characters character, int id) 
    {
        Item item = new Item(ItemData[id]);

        for (int i = 0; i < character.InventoryItem.Length; i++) 
        {
            if (character.InventoryItem[i] == null) 
            {
                character.InventoryItem[i] = item;
                return true;
            }
        }
        Debug.Log("Inventory Full");
        return false;
    }

    public void SaveItemInBag(int index, Item item) 
    {
        if (PartyManager.instance.SelectChars.Count == 0)
            return;

        PartyManager.instance.SelectChars[0].InventoryItem[index] = item;

        switch (index) 
        {
            case 16:
                PartyManager.instance.SelectChars[0].EquipShield(item);
                break;
            case 17:
                PartyManager.instance.SelectChars[0].EquipWeapon(item);
                break;
        }
    }

    public void RemoveItemInBag(int index) 
    {
        if (PartyManager.instance.SelectChars.Count == 0)
            return;

        PartyManager.instance.SelectChars[0].InventoryItem[index] = null;

        switch (index)
        {
            case 16:
                PartyManager.instance.SelectChars[0].UnEquipShield();
                break;
            case 17:
                PartyManager.instance.SelectChars[0].UnEquipWeapon();
                break;
        }
    }

    private void SpawnDropItem(Item item, Vector3 pos) 
    {
        int id;
        switch (item.Type)
        {
            case ItemType.Consumable:
                id = 1;
                break;
            default:
                id = 0;
                break;
        }

        
        float scatterRadius = 1.5f;
        Vector3 randomPos = new Vector3(
            Random.Range(-scatterRadius, scatterRadius),
            0, 
            Random.Range(-scatterRadius, scatterRadius)
        );

        Vector3 finalPos = pos + randomPos;
        
        GameObject itemObj = Instantiate(ItemPrefabs[id], finalPos, Quaternion.identity);

        itemObj.AddComponent<ItemPick>();
        ItemPick itemPick = itemObj.GetComponent<ItemPick>();
        itemPick.Init(item, this, PartyManager.instance);


    }

    public void SpawnDropInventory(Item[] items, Vector3 pos) 
    {
        for (int i = 0; i < items.Length; i++) 
        {
            if (items[i] != null)
                SpawnDropItem(items[i], pos);
        }
    }

    public void DrinkConsumableItem(Item item, int slotId) 
    {
        string s = string.Format("Drink: {0}", item.ItemName);
        Debug.Log(s);

        if (PartyManager.instance.SelectChars.Count > 0) 
        {
            PartyManager.instance.SelectChars[0].Recover(item.Power);
            RemoveItemInBag(slotId);
        }
    }

    public bool CheckPartyForItem(int id) 
    {
        Item item = new Item(itemData[id]);
        Debug.Log(item.ItemName);

        List<Characters> party = PartyManager.instance.Members;

        foreach (Characters hero in party) 
        {
            for (int i = 0; i < hero.InventoryItem.Length; i++) 
            {
                if (hero.InventoryItem[i] == null)
                    continue;

                Debug.Log(hero.InventoryItem[i].ItemName);

                if (hero.InventoryItem[i].ID == item.ID)
                    return true;
            }
        }
        return false;
    }
}
