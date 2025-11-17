using UnityEngine;

public class PelletV3 : MonoBehaviour
{
    public bool isCollectable;
    public PlayerControllerV3 playerController;
    public Rigidbody rb;

    public int damage = 1;

    private char shotDirection;

    public float shootSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerV3>();
        rb = GetComponent<Rigidbody>();

        //if (damage == 0)
        //{
        //    damage = 1;
        //}

        Debug.Log(damage);

        shootSpeed = 15f;

        playerController.shotDirection = shotDirection;
    }

    public void SetShootDirection(char shootDirection)
    {
        shotDirection = shootDirection;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.canShoot == true)
        {
            ShootPellet();
        }
    }

    public void ShootPellet()
    {
        if (shotDirection == 'L')
        {
            transform.position = transform.position + (Vector3.left * shootSpeed * Time.deltaTime);
        }
        else if (shotDirection == 'R')
        {
            transform.position = transform.position + (Vector3.right * shootSpeed * Time.deltaTime);
        }
        else if (shotDirection == 'U')
        {
            transform.position = transform.position + (Vector3.up * shootSpeed * Time.deltaTime);
        }
        else if (shotDirection == 'D')
        {
            transform.position = transform.position + (Vector3.down * shootSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.gameObject.CompareTag("Platform"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("cameraSwitch"))
        {
            Destroy(gameObject);
        }
    }
}
