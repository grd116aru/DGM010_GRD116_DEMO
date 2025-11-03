using UnityEngine;

public class Pellet : MonoBehaviour
{
    public GameObject pellet;
    public float moveSpeed;
    public Rigidbody rb;
    public PlayerController playerController;
    public GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitialSetup();
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = transform.position + (Vector3.down * moveSpeed * Time.deltaTime);

    }

    private void InitialSetup()
    {
        if (playerController == null)
        {
            playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        }
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();

        }
        rb.freezeRotation = true;

        moveSpeed = 20f;
        
        if (gameManager == null)
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DeadZone"))
        {
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Platform"))
        {
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            playerController.doubleJump = true;
            gameManager.UpdateJumpText();
        }
    }
}
