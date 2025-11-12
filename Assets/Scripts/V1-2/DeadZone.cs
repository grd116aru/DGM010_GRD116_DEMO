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
            //GameManagerV3.Instance.isGameOver = true;
            gameManager.isGameOver = true;
        }
    }
}
