using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public int enemyHealth;

    public float speed = 2f;
    [SerializeField] private float damageCooldown = 1f;
    [SerializeField] private float lastDamageTime = 0f;
    [SerializeField] private Vector3 moveDirection;

    public Rigidbody rb;
    public GameManagerV3 gameManager;
    public MeshRenderer mr;
    public BoxCollider bc;

    public PlayerControllerV3 playerController;

    public bool isDJEnemy;

    private void Start()
    {
        InitialSetup();

    }

    public void InitialSetup()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerV3>();
        rb = GetComponent<Rigidbody>();
        mr = GetComponent<MeshRenderer>();
        bc = GetComponent<BoxCollider>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerV3>();

        moveDirection = Vector3.left;

        mr.enabled = true;
        bc.enabled = true;

        enemyHealth = 3;
    }

    private void Update()
    {
        transform.position = transform.position + (moveDirection * speed * Time.deltaTime);

        if (enemyHealth <= 0)
        {
            if (isDJEnemy == true)
            {
                playerController.canDoubleJump = true;
            }

            gameObject.SetActive(false);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && Time.time > lastDamageTime + damageCooldown)
        {
            GameManager.Instance.ReduceHP(100);
            lastDamageTime = Time.time;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EndPointLeft"))
        {
            moveDirection = Vector3.right;
        }
        if (other.gameObject.CompareTag("EndPointRight"))
        {
            moveDirection = Vector3.left;
        }

        if (other.gameObject.CompareTag("Pellet"))
        {
            int pelletDamage = other.gameObject.GetComponent<PelletV3>().damage;
            gameManager.currentEnemy = gameObject;
            gameManager.enemyController = gameManager.currentEnemy.GetComponent<Enemy>();
            enemyHealth -= pelletDamage;
            Debug.Log($"Hit!-{enemyHealth}");
            Destroy(other.gameObject);
        }
    }
}
