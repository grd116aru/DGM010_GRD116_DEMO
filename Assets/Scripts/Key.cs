using UnityEngine;

public class Key : MonoBehaviour
{
    //program so that when the player is collided with, the key is destroyed and "hasKey" is gameManager is set to true.

    public GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitialSetup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitialSetup()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            gameManager.hasKey = true;
            gameManager.UpdateKeyText();
        }
    }
}
