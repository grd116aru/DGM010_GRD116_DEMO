using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerV3 : MonoBehaviour
{
    //integers
    public int gameScore;
    public int gameHealth;
    public int deathCount;

    //bools
    public bool isGameOver = false;
    public bool hasKey;
    public bool atFinish;

    //objects and components
    public static GameManagerV3 Instance;
    public GameObject player;
    public PlayerControllerV3 playerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitialSetup();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver == true)
        {
            EndGame();
        }
    }

    private void InitialSetup()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerControllerV3>();

        gameScore = 0;
        gameHealth = 100;
        deathCount = 0;
        hasKey = false;
        atFinish = false;
    }

    public void IncreaseScore(int value)
    {
        gameScore = gameScore + value;
    }
    
    public void EndGame()
    {
        isGameOver = false;
        player.transform.position = playerController.spawnPoint;
        gameHealth = 100;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //InitialSetup();
        playerController.InitialSetup();
    }
}
