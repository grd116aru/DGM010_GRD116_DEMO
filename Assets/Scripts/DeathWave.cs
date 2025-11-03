using Unity.VisualScripting;
using UnityEngine;

public class DeathWave : MonoBehaviour
{
    public float moveSpeed;

    public GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitialSetup();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.right * moveSpeed * Time.deltaTime);
    }

    private void InitialSetup()
    {
        moveSpeed = 3f;
        player = GameObject.FindWithTag("Player");
    }


}
