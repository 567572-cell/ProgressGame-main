using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float mouseSensitivity = 2f;
    public float gravity = -20f;
    public float jumpSpeed = 7f;
    public Transform cameraTransform;

    CharacterController controller;
    float cameraAngle;
    float fallSpeed;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // looking
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        transform.Rotate(0f, mouseX, 0f);
        cameraAngle = cameraAngle - mouseY;
        cameraAngle = Mathf.Clamp(cameraAngle, -85f, 85f);
        cameraTransform.localRotation = Quaternion.Euler(cameraAngle, 0f, 0f);

        // walking
        float sideways = Input.GetAxis("Horizontal");
        float forward = Input.GetAxis("Vertical");
        Vector3 direction = transform.right * sideways + transform.forward * forward;
        controller.Move(direction * speed * Time.deltaTime);

        // gravity and jumping
        if (controller.isGrounded && fallSpeed < 0f)
        {
            fallSpeed = -2f;
        }
        if (controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            fallSpeed = jumpSpeed;
        }
        fallSpeed = fallSpeed + gravity * Time.deltaTime;
        controller.Move(new Vector3(0f, fallSpeed, 0f) * Time.deltaTime);
    }
}