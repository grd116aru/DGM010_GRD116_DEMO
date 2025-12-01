using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerV3 : MonoBehaviour
{
    //integers
    public int gameScore;
    public int gameHealth;
    public int deathCount;
    public int killCount;

    //bools
    public bool isGameOver = false;
    public bool hasKey;
    public bool atFinish;

    //objects and components
    public static GameManagerV3 Instance;
    public GameObject player;
    public GameObject cam;
    public PlayerControllerV3 playerController;
    public GameObject currentEnemy;
    public Enemy enemyController;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI deathText;
    public TextMeshProUGUI killsText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitialSetup();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameHealth == 0)
        {
            deathCount += 1;
            UpdateTexts();
            isGameOver = true;
        }

        if (isGameOver == true)
        {
            EndGame();
            if (enemyController != null && currentEnemy != null)
            {
                enemyController.InitialSetup();
                currentEnemy.SetActive(true);
            }
        }
    }

    private void InitialSetup()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerControllerV3>();
        cam = GameObject.FindGameObjectWithTag("MainCamera");

        gameScore = 0;
        gameHealth = 100;
        deathCount = 0;
        killCount = 0;
        hasKey = false;
        atFinish = false;

        GameObject ScoreText = GameObject.FindGameObjectWithTag("scoreText");
        scoreText = ScoreText.GetComponent<TextMeshProUGUI>();
        GameObject DeathCountText = GameObject.FindGameObjectWithTag("hpText");
        deathText = DeathCountText.GetComponent<TextMeshProUGUI>();
        GameObject KillsText = GameObject.FindGameObjectWithTag("jumpText");
        killsText = KillsText.GetComponent<TextMeshProUGUI>();

        cam.transform.position = new UnityEngine.Vector3(0f, 6f, -10f);

        UpdateTexts();
    }

    public void IncreaseScore(int value)
    {
        gameScore = gameScore + value;
        UpdateTexts();
    }

    public void UpdateTexts()
    {
        scoreText.text = "Score: " + gameScore.ToString();
        deathText.text = "Deaths: " + deathCount.ToString();
        killsText.text = "Kills: " + killCount.ToString();
    }

    public void ReduceHP(int value)
    {
        gameHealth -= value;
    }
    
    public void EndGame()
    {
        isGameOver = false;
        player.transform.position = playerController.spawnPoint;
        gameHealth = 100;
        //currentEnemy.SetActive(true);
        //enemyController.enemyHealth = 3;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
