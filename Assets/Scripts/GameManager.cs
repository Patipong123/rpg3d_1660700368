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
            AudioManager.instance.PlayBGM(3);
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

        Hero hero = heroObj.GetComponent<Hero>();
        hero.PrefabID = i;
        PartyManager.instance.Members.Add(hero);

        hero.CharInit(VFXManager.instance, UIManager.instance,
            InventoryManager.instance, PartyManager.instance);

        InventoryManager.instance.AddItem(hero, 0);
        InventoryManager.instance.AddItem(hero, 1);
        

        PartyManager.instance.SelectSingleHero(0);
        UIManager.instance.ShowMagicToggles();
    }

    private void WarpPlayers() 
    {
        PartyManager.instance.LoadAllHeroData();
    }

}
