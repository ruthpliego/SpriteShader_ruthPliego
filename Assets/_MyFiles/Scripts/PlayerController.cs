using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 5f; 
    private float jumpForce = 5f;
    [SerializeField] private Transform cameraTransform; 
    private Animator animator; 
    private Rigidbody rb;

    private bool isGrounded; 

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        Cursor.visible = false; 
    }

    void Update()
    {
        Move();
        Jump();
        Dance();
        QuitGame(); // Check for quit game input
    }

    void Move()
    {
        // Get input from WASD or Arrow keys
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        // Rotate the player towards the camera direction
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f);
            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            rb.MovePosition(rb.position + moveDir.normalized * speed * Time.deltaTime);
        }

        // Update animator's movement
        animator.SetFloat("Speed", direction.magnitude);
    }

    void Jump()
    {
        // Check if the player is grounded
        isGrounded = Physics.CheckSphere(transform.position, 0.1f, LayerMask.GetMask("Ground"));

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
        }
    }

    void Dance()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("Dance");
        }
    }

    void QuitGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit(); // Quit the application
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in the editor
#endif
        }
    }
}