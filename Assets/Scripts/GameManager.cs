using System;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public int gameScore;
    public int gameHealth;

    public bool isGameOver = false;
    public bool hasKey;
    public bool atFinish;

    public Vector3 spawnPoint;

    public GameObject player;
    public static GameManager Instance;
    public PlayerController playerController;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI jumpText;
    public TextMeshProUGUI keyText;

    private void Awake()
    {
        // Enforce Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Keep alive across scenes
    }

    private void Start()
    {
        InitialSetup();
    }

    private void InitialSetup()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();

        gameScore = 0;
        gameHealth = 100;
        hasKey = false;
        atFinish = false;

        GameObject hp = GameObject.FindGameObjectWithTag("hpText");
        GameObject score = GameObject.FindGameObjectWithTag("scoreText");
        GameObject jump = GameObject.FindGameObjectWithTag("jumpText");
        GameObject keytext = GameObject.FindGameObjectWithTag("keyText");

        scoreText = score.GetComponent<TextMeshProUGUI>();
        hpText = hp.GetComponent<TextMeshProUGUI>();
        jumpText = jump.GetComponent<TextMeshProUGUI>();
        keyText = keytext.GetComponent<TextMeshProUGUI>();

        UpdateScoreText();
        UpdateHPText();
        UpdateJumpText();
        UpdateKeyText();
    }


    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            EndGame();
        }

        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
        if (playerController == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerController = player.GetComponent<PlayerController>();
        }

        if (scoreText == null)
        {
            GameObject score = GameObject.FindGameObjectWithTag("scoreText");
            scoreText = score.GetComponent<TextMeshProUGUI>();
            UpdateScoreText();
        }
        if (hpText == null)
        {
            GameObject hp = GameObject.FindGameObjectWithTag("hpText");
            hpText = hp.GetComponent<TextMeshProUGUI>();
            UpdateHPText();
        }
        if (jumpText == null)
        {
            GameObject jump = GameObject.FindGameObjectWithTag("jumpText");
            jumpText = jump.GetComponent<TextMeshProUGUI>();
            UpdateJumpText();
        }
        if (keyText == null)
        {
            GameObject keytext = GameObject.FindGameObjectWithTag("keyText");
            keyText = keytext.GetComponent<TextMeshProUGUI>();
            UpdateKeyText();
        }

        if (gameHealth <= 0)
        {
            isGameOver = true;
        }
    }

    public void EndGame()
    {
        isGameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        InitialSetup();
        playerController.InitialSetup();
    }

    public void IncreaseScore(int value)
    {
        gameScore += value;
        UpdateScoreText();
        UpdateHPText();
    }
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + gameScore.ToString();
    }

    private void UpdateHPText()
    {
        hpText.text = "Health: " + gameHealth.ToString();
        if (gameHealth < 60)
        {
            hpText.color = new Color(255, 128, 0);
        }
        else
        {
            hpText.color = new Color(0, 255, 0);
        }
    }

    public void UpdateJumpText()
    {
        if (playerController.doubleJump == false)
        {
            jumpText.text = "_";
            jumpText.color = new Color(255f, 255f, 255f, 128f);
        }

        if (playerController.doubleJump == true)
        {
            jumpText.text = "Double Jump";
            jumpText.color = new Color(255f, 255f, 255f, 255f);
        }
    }

    public void UpdateKeyText()
    {
        if (hasKey == false)
        {
            keyText.text = "_";
            keyText.color = new Color(0f, 255f, 255f, 128f);

        }

        if (hasKey == true)
        {
            keyText.text = "Has Key";
            keyText.color = new Color(0f, 255f, 255f, 255f);
        }
    }

    public void ReduceHP(int value)
    {
        gameHealth -= value;
        UpdateHPText();
        if (gameHealth <= 0)
        {
            isGameOver = true;
        }
    }

    //private void OnLevelWasLoaded(int level = 0)
    //{
    //    isGameOver = false;
    //    score = 0;
    //    player = GameObject.FindWithTag("Player");
    //}
}

