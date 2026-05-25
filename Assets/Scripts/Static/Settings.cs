using UnityEngine;
using System.Collections.Generic;

public static class Settings
{
    public static int playerPrefabId;
    public static bool isNewGame = false;
    public static bool isWarping = false;

    public static int enterPointId;
    public static int PartyCount;

    public static float lastWarpTime = -999f;

    public static HashSet<int> recruitedHeroPrefabIds = new HashSet<int>();
}
