using UnityEngine;

public class MoveMarker : MonoBehaviour
{
    [SerializeField]
    private float liftTime = 1f;

    
    void Start()
    {
        Destroy(gameObject, liftTime);
    }

    
    void Update()
    {
        
    }
}
