using UnityEngine;

public class PlayerControllerV3 : MonoBehaviour
{
    public float moveSpeed;
    public float horizontalInput;
    public float jumpForce;

    public bool isGrounded;

    public Rigidbody rb;
    public GameObject cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitialSetup();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    private void InitialSetup()
    {
        if (moveSpeed == 0)
        {
            moveSpeed = 5f;
        }
        if (jumpForce == 0)
        {
            jumpForce = 6f;
        }
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        if (cam == null)
        {
            cam = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }

    private void PlayerMovement()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector3(horizontalInput * moveSpeed, rb.linearVelocity.y, rb.linearVelocity.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded == true)
            {
                rb.linearVelocity = new Vector3(0f, jumpForce, 0f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("cameraSwitch"))
        {
            cam.transform.position = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, -10f);
        }
    }
}
