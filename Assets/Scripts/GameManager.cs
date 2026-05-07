using UnityEngine;

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
        GeneratePlayerHero();
    }

    private void GeneratePlayerHero() 
    {
        int i = Settings.playerPrefabId;

        GameObject heroObj = Instantiate(heroPregabs[i],
            new Vector3(46f, 0, 38f), Quaternion.identity);

        heroObj.tag = "Player";

        Characters hero = heroObj.GetComponent<Characters>();
        PartyManager.instance.Members.Add(hero);
    }
}
