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

    public static PartyManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        foreach (Characters c in members) 
        {
            c.CharInit(VFXManager.instance, UIManager.instance);
        }

        SelectSingleHero(0);

        members[0].MagicSkills.Add(new Magic(0, "Power Glow", 10f, 20, 3f, 1f, 2, 2));
        members[0].MagicSkills.Add(new Magic(1, "Fire Ball", 12f, 30, 4f, 1f, 3, 3));
        members[0].MagicSkills.Add(new Magic(2, "Ice Spear", 15f, 25, 5f, 1f, 4, 4));

        members[1].MagicSkills.Add(new Magic(0, "Fire Ball", 10f, 35, 3f, 5f, 0, 1));
        members[1].MagicSkills.Add(new Magic(2, "Thunder", 18f, 45, 6f, 1f, 5, 5));

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
}
