using UnityEngine;

public class WarpPoint : MonoBehaviour
{
    [SerializeField]
    private string toMapName;

    [SerializeField]
    private int enterPointId;

    private bool isWarping = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isWarping) return;
        if (Time.time - Settings.lastWarpTime < 2f) return;

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player enters Warp");
            isWarping = true;
            MapManager.instance.GoToMap(toMapName, enterPointId);
        }
    }
}
