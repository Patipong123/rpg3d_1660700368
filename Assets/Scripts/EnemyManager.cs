using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private List<Enemy> monsters;
    public List<Enemy> Monsters { get { return monsters; } }

    public static EnemyManager instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        foreach (Characters m in monsters) 
        {
            m.CharInit(VFXManager.instance, UIManager.instance, InventoryManager.instance, PartyManager.instance);
        }

        InventoryManager.instance.AddItem(monsters[0], 0);
        InventoryManager.instance.AddItem(monsters[0], 1);
        InventoryManager.instance.AddItem(monsters[0], 2);
    }

    
    void Update()
    {
        
    }
}
