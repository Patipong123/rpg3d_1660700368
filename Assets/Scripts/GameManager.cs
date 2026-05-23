using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] heroPregabs;
    public GameObject[] HeroPrefabs { get {  return heroPregabs; } }

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (Settings.isNewGame) 
        {
            Settings.isNewGame = false;
            GeneratePlayerHero();
        }

        if (Settings.isWarping)
        {
            Settings.isWarping = false;
            WarpPlayers();
        }
    }

    private void GeneratePlayerHero() 
    {
        int i = Settings.playerPrefabId;

        GameObject heroObj = Instantiate(heroPregabs[i],
            new Vector3(46f, 0, 38f), Quaternion.identity);

        heroObj.tag = "Player";

        Characters hero = heroObj.GetComponent<Characters>();
        PartyManager.instance.Members.Add(hero);

        hero.CharInit(VFXManager.instance, UIManager.instance,
            InventoryManager.instance, PartyManager.instance);

        InventoryManager.instance.AddItem(hero, 0);
        InventoryManager.instance.AddItem(hero, 2);
    }

    private void WarpPlayers() 
    {
        PartyManager.instance.LoadAllHeroData();
    }

}
