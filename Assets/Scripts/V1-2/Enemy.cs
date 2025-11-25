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
            gameObject.SetActive(false);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && Time.time > lastDamageTime + damageCooldown)
        {
            GameManager.Instance.ReduceHP(20);
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
            enemyHealth -= pelletDamage;
            Debug.Log($"Hit!-{enemyHealth}");
            Destroy(other.gameObject);
        }
    }
}
