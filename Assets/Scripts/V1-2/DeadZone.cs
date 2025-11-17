using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public GameManagerV3 gameManager;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerV3>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.isGameOver = true;
            gameManager.deathCount += 1;
        }
    }
}
