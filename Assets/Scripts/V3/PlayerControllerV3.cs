using UnityEngine;

public class PlayerControllerV3 : MonoBehaviour
{
    public float moveSpeed;
    public float horizontalInput;
    public float jumpForce;

    public bool isGrounded;
    public bool hasGrounded;

    public Vector3 spawnPoint;

    public Rigidbody rb;
    public GameObject cam;
    public GameManagerV3 gameManager;
    public DoorController doorController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitialSetup();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasGrounded == true)
        {
            PlayerMovement();
        }
    }

    public void InitialSetup()
    {
        hasGrounded = false;
        
        moveSpeed = 5f;
        jumpForce = 6f;

        rb = GetComponent<Rigidbody>();
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerV3>();

        if (gameManager.deathCount == 0)
        {
            spawnPoint = new Vector3(-7.5f, 10f, 0f);
        }

        gameObject.transform.position = spawnPoint;
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

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (gameManager.atFinish == true && gameManager.hasKey)
            {
                //send player to next level
                Debug.Log("Next Level!");
                doorController.EnterDoor();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("cameraSwitch"))
        {
            cam.transform.position = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, -10f);
        }

        if (other.gameObject.CompareTag("FinishLine"))
        {
            gameManager.atFinish = true;
        }

        if (other.gameObject.CompareTag("Checkpoint"))
        {
            spawnPoint = new Vector3(other.transform.position.x, other.transform.position.y, 0f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("FinishLine"))
        {
            gameManager.atFinish = false;
        }
    }
}
