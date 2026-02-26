using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField]
    private GameObject doubleRingMarker;
    public GameObject DoubleRingMarker { get { return doubleRingMarker; } }

    [SerializeField]
    private GameObject[] magicVFX;
    private GameObject[] MagicVFX { get { return magicVFX; } }
    public static VFXManager instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        instance = this;
    }

    
    void Update()
    {
        
    }

    public void LoadMagic(int id, Vector3 posA, float time) 
    {
        if (magicVFX[id] == null)
            return;

        posA += Vector3.up * 1.2f;

        GameObject objLoad = Instantiate(magicVFX[id], posA, Quaternion.identity);
        Destroy(objLoad, time);
    }

    public void ShootMagic(int id, Vector3 posA, Vector3 posB, float time) 
    {
        if (magicVFX[id] == null)
            return;

        posA += Vector3.up * 1.2f;
        posB += Vector3.up * 1.2f;

        GameObject objShoot = Instantiate(magicVFX[id], posA, Quaternion.identity);
        objShoot.transform.position = Vector3.LerpUnclamped(posA, posB, time);
        Destroy(objShoot, time);
    }
}
