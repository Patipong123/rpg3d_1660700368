using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField]
    private Npc[] npcPerson;
    public Npc[] NPCPerson {  get { return npcPerson; } set { npcPerson = value; } }

    [SerializeField]
    private QuestData[] questData;
    public QuestData[] QuestData { get { return questData; } set { questData = value; } }

    [SerializeField]
    private Npc curNpc;
    public Npc CurNPC { get { return curNpc; } set { curNpc = value; } }

    [SerializeField]
    private Quest curQuest;
    public Quest CurQuest { get { return curQuest; } set { curQuest = value; } }

    public static QuestManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        foreach (Characters npc in npcPerson) 
        {
            npc.CharInit(VFXManager.instance, UIManager.instance,
                InventoryManager.instance, PartyManager.instance);
        }

        AddQuestToNPC(npcPerson[0], questData[0]);
    }

    private void AddQuestToNPC(Npc npc, QuestData questData) 
    {
        Quest quest = new Quest(questData);
        npc.QuestToGive.Add(quest);
    }

    public Quest CheckForQuest(Npc npc, QuestStatus status) 
    {
        curNpc = npc;
        Quest quest = npc.CheckQuestList(status);
        curQuest = quest;

        return quest;
    }

    private bool CheckItemToDelivery() 
    {
        return InventoryManager.instance.CheckPartyForItem(curQuest.QuestItemId);
    }

    public bool CheckIfFinishQuest() 
    {
        bool success = false;
        Debug.Log(curQuest.Type);

        switch (curQuest.Type) 
        {
            case QuestType.Delivery:
                success = CheckItemToDelivery(); 
                break;
        }
        return success;
    }

    public bool CheckLastDialouge(int i) 
    {
        if (i == curQuest.QuestDialogue.Length - 1)
            return true;
        else
            return false;
    }

    public string NextDialouge(int i) 
    {
        if (i < curQuest.QuestDialogue.Length)
            return curQuest.QuestDialogue[i];
        else
            return "";
    }

    public void RejectQuest() 
    {
        curQuest.Status = QuestStatus.New;
        Debug.Log("RejectQuest");
    }

    public void AcceptQuest() 
    {
        curQuest.Status = QuestStatus.InProgress;
        PartyManager.instance.QuestList.Add(curQuest);
        Debug.Log("AcceptQuest");
    }

    public bool DeliverItem() 
    {
        return InventoryManager.instance.RemoveItemFromParty(curQuest.QuestItemId);
    }

    public bool NpcGiveReward() 
    {
        if (PartyManager.instance.SelectChars.Count == 0)
            return false;

        Characters hero = PartyManager.instance.SelectChars[0];

        Item item = new Item(InventoryManager.instance.ItemData[curQuest.RewardItemId]);

        for (int i = 0; i < 16; i++) 
        {
            if (hero.InventoryItem[i] == null) 
            {
                hero.InventoryItem[i] = item;
                curQuest.Status = QuestStatus.Finish;
                return true;
            }
        }
        return false;
    }

   
}
