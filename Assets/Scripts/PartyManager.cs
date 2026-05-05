using UnityEngine;
using System.Collections.Generic;

public class PartyManager : MonoBehaviour
{
    [SerializeField]
    private List<Characters> members = new List<Characters>();
    public List<Characters> Members { get { return members; } }

    [SerializeField]
    private List<Characters> selectChars = new List<Characters>();
    public List<Characters> SelectChars { get { return selectChars; } }

    [SerializeField]
    private List<Quest> questList = new List<Quest>();
    public List<Quest> QuestList { get { return questList; } }

    public static PartyManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        foreach (Characters c in members) 
        {
            c.CharInit(VFXManager.instance,
                UIManager.instance, InventoryManager.instance);
        }

        SelectSingleHero(0);

        members[0].MagicSkills.Add(new Magic(VFXManager.instance.MagicData[0]));
        members[1].MagicSkills.Add(new Magic(VFXManager.instance.MagicData[1]));

        InventoryManager.instance.AddItem(members[0], 0);
        InventoryManager.instance.AddItem(members[0], 2);
        InventoryManager.instance.AddItem(members[0], 3);
        InventoryManager.instance.AddItem(members[0], 4);



        InventoryManager.instance.AddItem(members[1], 0);
        InventoryManager.instance.AddItem(members[1], 2);
        InventoryManager.instance.AddItem(members[1], 3);


        UIManager.instance.ShowMagicToggles();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) 
        {
            if (selectChars.Count > 0) 
            {
                selectChars[0].IsMagicMode = true;
                selectChars[0].CurMagicCast = selectChars[0].MagicSkills[0];
            }
        }
    }

    public void SelectSingleHero(int i) 
    {
        foreach (Characters c in selectChars)
            c.ToggleRingSelection(false);

        selectChars.Clear();

        selectChars.Add(members[i]);
        selectChars[0].ToggleRingSelection(true);
    }

    public void HeroSelectMagicSkill(int i)
    {
        if (selectChars.Count <= 0)
            return;

        selectChars[0].IsMagicMode = true;
        selectChars[0].CurMagicCast = selectChars[0].MagicSkills[i];
    }

    public int FindIndexFromClass(Characters hero) 
    {
        for (int i = 0; i < members.Count; i++) 
        {
            if (members[i] == hero)
                return i;
        }
        return 0;
    }

    public void SelectSingleHeroByToggle(int i) 
    {
        if (selectChars.Contains(members[i])) 
        {
            members[i].ToggleRingSelection(true);
            UIManager.instance.ShowMagicToggles();
        }
        else 
        {
            selectChars.Add(members[i]);
            members[i].ToggleRingSelection(true);
            UIManager.instance.ShowMagicToggles();
        }
    }

    public void UnSelectSingleHeroByToggle(int i) 
    {
        if (selectChars.Count <= 1) 
        {
            UIManager.instance.ToggleAvatar[i].isOn = true;
            return;
        }

        if (selectChars.Contains(members[i])) 
        {
            selectChars.Remove(members[i]);
            members[i].ToggleRingSelection(false);
        }
    }
}
