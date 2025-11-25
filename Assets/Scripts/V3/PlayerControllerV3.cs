using UnityEngine;

public class PlayerControllerV3 : MonoBehaviour
{
    public float moveSpeed;
    public float horizontalInput;
    public float jumpForce;

    public char shotDirection;

    public bool isGrounded;
    public bool hasGrounded;
    public bool canShoot;

    public Vector3 spawnPoint;

    public Rigidbody rb;
    public GameObject cam;
    public GameObject pellet;
    public GameManagerV3 gameManager;
    public DoorController doorController;
    public GameObject pauseMenuUI;

    public Enemy currentEnemy;

    public bool canDoubleJump;

    public Material baseMaterial;
    public Material jumpMaterial;

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

        if (canDoubleJump == true)
        {
            gameObject.GetComponent<Renderer>().material = jumpMaterial;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material = baseMaterial;
        }

    }

    public void InitialSetup()
    {
        hasGrounded = false;
        canShoot = false;
        canDoubleJump = false;

        moveSpeed = 5f;
        jumpForce = 6f;

        rb = GetComponent<Rigidbody>();
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerV3>();

        if (gameManager.deathCount == 0)
        {
            spawnPoint = new Vector3(-7.5f, 10f, 0f);
        }

        //gameObject.transform.position = spawnPoint;
    }

    private void PlayerMovement()
    {
        //horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.A))
        {
            horizontalInput = horizontalInput - 1f;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            horizontalInput = horizontalInput + 1f;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            horizontalInput = horizontalInput + 1f;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            horizontalInput = horizontalInput - 1f;
        }

        rb.linearVelocity = new Vector3(horizontalInput * moveSpeed, rb.linearVelocity.y, rb.linearVelocity.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded == true)
            {
                rb.linearVelocity = new Vector3(0f, jumpForce, 0f);
            }
            else if (canDoubleJump == true)
            {
                rb.linearVelocity = new Vector3(0f, jumpForce, 0f);
                canDoubleJump = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (gameManager.atFinish == true && gameManager.hasKey)
            {
                //send player to next level
                Debug.Log("Next Level!");
                doorController.EnterDoor();
                gameManager.hasKey = false;
            }
        }

        if (canShoot == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                //Debug.Log("LEFT!");
                ShootPellet('L');
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                //Debug.Log("RIGHT!");
                ShootPellet('R');
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                //Debug.Log("UP!");
                ShootPellet('U');
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                //Debug.Log("DOWN!");
                ShootPellet('D');
            }
        }
    }

    private void ShootPellet(char direction)
    {
        shotDirection = direction;
        GameObject pelletInstance = Instantiate(pellet, transform.position, transform.rotation);
        pelletInstance.GetComponent<PelletV3>().SetShootDirection(shotDirection);
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

        if (other.gameObject.CompareTag("Pellet"))
        {
            if (other.gameObject.GetComponent<PelletV3>().isCollectable == true)
            {
                Destroy(other.gameObject);
                canShoot = true;
            }
        }
    }
}
