using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [field: SerializeField] public static PlayerMovement Instance { get; private set; }
    [SerializeField] private float moveSpeed;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float mouseSensitivity;
    private float xRotation;
    private Vector3 velocity;
    private Transform cameraTransform;
    private CharacterController cc;
    public InputSystem_Actions InputActions { get; private set; }
    [field: SerializeField] public bool isMovementDisabled {get; private set;}

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        InputActions = new InputSystem_Actions();
        Init();
    }
    public void Init()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        InputActions.Player.Enable();

        cc = GetComponent<CharacterController>();

        cameraTransform = GetComponentInChildren<Camera>().transform;
    }

    void Update()
    {
        if (!isMovementDisabled)
        {
            HandleMovement();
            HandleLook();
        }
    }

    private void HandleMovement()
    {
        Vector2 moveInput = InputActions.Player.Move.ReadValue<Vector2>();
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        cc.Move(move * moveSpeed * Time.deltaTime);

        if (cc.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
    }

    private void HandleLook()
    {
        Vector2 lookInput = InputActions.Player.Look.ReadValue<Vector2>();

        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    public void EnablingMovement(bool value)
    {
        isMovementDisabled = !value;
        EnablingCursor(!value);
    }

    private void EnablingCursor(bool value)
    {
        if (value)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }

    void OnDestroy()
    {
        InputActions.Player.Disable();
    }
}
