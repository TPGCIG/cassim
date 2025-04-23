using UnityEngine;

public class BeanMover : MonoBehaviour
{
    public float speed = 5f;
    public float sprintMultiplier = 2f; // Multiplier for sprinting speed
    public float jumpForce = 2.5f; // Force applied for jumping
    private Rigidbody rb;
    private bool isGrounded = true; // To check if the player is on the ground

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Freeze rotation on all axes
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Get the forward and right directions based on current rotation
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        // Calculate movement direction
        Vector3 movement = (forward * moveZ + right * moveX).normalized;

        // Check if the Shift key is held for sprinting
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? speed * sprintMultiplier : speed;

        // Apply movement
        Vector3 move = movement * currentSpeed * Time.deltaTime;
        transform.Translate(move, Space.World);

        // Jump logic
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; // Prevent double jumping
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player is grounded
        if (collision.contacts.Length > 0 && collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }
}
