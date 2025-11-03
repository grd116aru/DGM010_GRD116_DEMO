using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private float horizontalInput;
    public float jumpForce;

    public bool isGrounded;
    public bool doubleJump;

    public Rigidbody rb;
    public GameManager gameManager;
    public GameObject pellet;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitialSetup();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
    }

    public void InitialSetup()
    {
        if (moveSpeed == 0)
        {
            moveSpeed = 5f;
        }
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        if (jumpForce == 0)
        {
            jumpForce = 6f;
        }
        if (gameManager == null)
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }
    }

    private void MoveCharacter()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector3(horizontalInput * moveSpeed, rb.linearVelocity.y, rb.linearVelocity.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded == true)
            {
                rb.linearVelocity = new Vector3(0f, jumpForce, 0f);
            }
            else if (doubleJump == true)
            {
                rb.linearVelocity = new Vector3(0f, jumpForce, 0f);
                doubleJump = false;
                gameManager.UpdateJumpText();
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ShootPelletDown();
        }
    }

    private void ShootPelletDown()
    {
        Instantiate(pellet, transform.position, transform.rotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FinishLine"))
        {
            gameManager.atFinish = true;
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
