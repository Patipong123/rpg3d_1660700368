using UnityEngine;
using UnityEditor;

public class FindMissingScripts : EditorWindow
{
    [MenuItem("Tools/Find Missing Scripts")]
    public static void FindAll()
    {
        int count = 0;
        foreach (GameObject go in Resources.FindObjectsOfTypeAll<GameObject>())
        {
            foreach (Component c in go.GetComponents<Component>())
            {
                if (c == null)
                {
                    Debug.LogWarning($"Missing script on: {GetFullPath(go)}", go);
                    count++;
                }
            }
        }

        if (count == 0)
            Debug.Log("No missing scripts found.");
        else
            Debug.LogWarning($"Found {count} missing script(s). Click the warnings above to highlight each object.");
    }

    private static string GetFullPath(GameObject go)
    {
        string path = go.name;
        Transform t = go.transform.parent;
        while (t != null)
        {
            path = t.name + "/" + path;
            t = t.parent;
        }
        return go.scene.name + ": " + path;
    }
}
