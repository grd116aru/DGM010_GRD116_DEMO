using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    [SerializeField] private float damageCooldown = 1f;
    [SerializeField] private float lastDamageTime = 0f;
    [SerializeField] private Vector3 moveDirection;

    public Rigidbody rb;
    public GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody>();
        moveDirection = Vector3.left;
    }

    private void Update()
    {
        transform.position = transform.position + (moveDirection * speed * Time.deltaTime);
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
    }
}
