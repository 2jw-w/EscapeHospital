using UnityEngine;

public class FPSController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 2f;
    public float runSpeed = 5f; // Shift 달리기 속도
    public float gravity = -9.81f;

    [Header("Mouse Look")]
    public float mouseSensitivity = 800f;

    public Transform cameraTransform;

    private CharacterController controller;
    private Vector3 velocity;
    private float xRotation = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Move();
        LookAround();
    }
    public bool IsMoving { get; private set; }
    public bool IsRunning { get; private set; }
    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        IsRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = IsRunning ? runSpeed : walkSpeed;

        Vector3 move = transform.right * x + transform.forward * z;
        IsMoving = move.magnitude > 0.1f && controller.isGrounded;

        controller.Move(move * currentSpeed * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0) velocity.y = -2f;
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void LookAround()
    {
        float mouseX =
            Input.GetAxis("Mouse X")
            * mouseSensitivity
            * Time.deltaTime;

        float mouseY =
            Input.GetAxis("Mouse Y")
            * mouseSensitivity
            * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation =
            Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }
}