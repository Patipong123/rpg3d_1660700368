using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "HeroData", menuName = "Scriptable Objects/HeroData")]
public class HeroData : ScriptableObject
{
    public int prefabId;
    public int curHp;
    public List<int> magicIds = new List<int>();
    public int[] inventoryItemId = new int[16];

    public int attackDamage;
    public int defensePower;

    public int exp;
    public int level;
    public int nextExp;
}
