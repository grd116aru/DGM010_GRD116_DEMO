using UnityEngine;

public class KeyV3 : MonoBehaviour
{
    //program so that when the player is collided with, the key is destroyed and "hasKey" is gameManager is set to true.

    public GameManagerV3 gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitialSetup();
    }

    private void InitialSetup()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerV3>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            gameManager.hasKey = true;
        }
    }
}
