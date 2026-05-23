using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera cam;

    [Header("Move")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform corner1;
    [SerializeField] private Transform corner2;
    [SerializeField] private float xInput;
    [SerializeField] private float zInput;

    public static CameraController instance;

    [Header("Zoom")]
    [SerializeField] private float zoomModifier;

    private InputSystem_Actions inputActions;

    void Awake()
    {
        instance = this;
        cam = Camera.main;
        inputActions = new InputSystem_Actions();
    }

    void Start()
    {
        moveSpeed = 50;
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Zoom.performed += OnZoom;
    }

    private void OnDisable()
    {
        inputActions.Player.Zoom.performed -= OnZoom;
        inputActions.Disable();
    }

    private void OnZoom(InputAction.CallbackContext ctx)
    {
        float scroll = ctx.ReadValue<Vector2>().y;
        cam.orthographicSize -= scroll * 0.01f;
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, 4, 10);
    }

    void Update()
    {
        MoveByKB();
    }

    private void MoveByKB()
    {
        Vector2 moveInput = inputActions.Player.Move.ReadValue<Vector2>();
        xInput = moveInput.x;
        zInput = moveInput.y;

        Vector3 dir = (transform.forward * zInput) + (transform.right * xInput);
        transform.position += dir * moveSpeed * Time.deltaTime;
        transform.position = Clamp(corner1.position, corner2.position);
    }

    private Vector3 Clamp(Vector3 lowerLeft, Vector3 topRight)
    {
        return new Vector3(
            Mathf.Clamp(transform.position.x, lowerLeft.x, topRight.x),
            transform.position.y,
            Mathf.Clamp(transform.position.z, lowerLeft.z, topRight.z));
    }
}
