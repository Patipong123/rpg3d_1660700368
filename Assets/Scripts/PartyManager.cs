using UnityEngine;
using System.Collections.Generic;

public class PartyManager : MonoBehaviour
{

    [SerializeField]
    private List<Characters> selectChars = new List<Characters>();
    public List<Characters> SelectChars { get { return selectChars; } }

    public static PartyManager instance;

    private void Awake()
    {
        instance = this;
    }
}
