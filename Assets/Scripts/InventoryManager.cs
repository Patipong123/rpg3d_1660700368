using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] itemPrefabs;
    public GameObject[] ItemPrefabs { get { return itemPrefabs; } set { itemPrefabs = value; } }

    [SerializeField]
    private ItemData[] itemData;
    public ItemData[] ItemData { get { return itemData; } set { itemData = value; } }

    public const int MAXSLOT = 16;

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
}
