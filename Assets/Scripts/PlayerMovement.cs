using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform playerCamera;

    public float speed = 6f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;

    public float bobFrequency = 16f;   // Faster bobbing
    public float bobAmplitude = 0.1f;  // Stronger bobbing
    public float bobSharpness = 10f;   // How quickly camera snaps

    private Vector3 velocity;
    private float originalCameraY;
    private float bobTimer = 0f;

    void Start()
    {
        if (playerCamera != null)
        {
            originalCameraY = playerCamera.localPosition.y;
        }
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Reset gravity if grounded
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Rockier Camera Bobbing
        if (move.magnitude > 0.1f && controller.isGrounded)
        {
            bobTimer += Time.deltaTime * bobFrequency;
            float bobOffset = Mathf.Abs(Mathf.Sin(bobTimer)) * bobAmplitude; // <-- ABS makes it snappier (sharp up/down)

            Vector3 targetPos = new Vector3(
                playerCamera.localPosition.x,
                originalCameraY + bobOffset,
                playerCamera.localPosition.z
            );

            playerCamera.localPosition = Vector3.Lerp(playerCamera.localPosition, targetPos, Time.deltaTime * bobSharpness);
        }
        else
        {
            // Smoothly reset camera position when not moving
            Vector3 camPos = playerCamera.localPosition;
            camPos.y = Mathf.Lerp(camPos.y, originalCameraY, Time.deltaTime * bobSharpness);
            playerCamera.localPosition = camPos;
            bobTimer = 0f;
        }
    }
}
