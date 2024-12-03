using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    private float startTime;
    public Text timerText;
    public bool isTimerRunning = false;

    public bool isClimbingUp = false;  // Track if the "Climb Up" button is pressed
    public bool isClimbingDown = false; // Track if the "Climb Down" button is pressed
    public bool isMovingLeft = false;
    public bool isJumpingButton = false; // To track the jump button press
    public bool isAttackingButton = false;
    public bool isMovingRight = false;


    public static GameSession instance;

    [SerializeField] int playerLives = 3;
    [SerializeField] int playerScore = 0;

    [SerializeField] Image[] Hearts;

    [SerializeField] Text scoreText, livesText;

    private void Awake()
    {

        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        scoreText.text = playerScore.ToString();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to sceneLoaded event
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe from sceneLoaded event
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the loaded scene is "Level 1"
        if (scene.name == "Level 1")
        {
            StartTimer(); // Start the timer
        }
        else
        {
            StopTimer(); // Stop the timer in other scenes, if necessary
        }
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            float elapsedTime = Time.time - startTime;
            UpdateTimerText(elapsedTime);
        }

    }


    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            TakeLife();
            StopTimer();
            SceneManager.LoadScene("Game Over");
        }
    }




    public void TakeLife()
    {
        playerLives--;

        UpdateHearts();
    }

    public void IncreaseLife()
    {
        playerLives++;

        if(playerLives >= 3)
        {
            playerLives = 3;
        }

        UpdateHearts();

    }

    public void UpdateHearts()
    {
        for (int i = 0; i < Hearts.Length; i++)
        {
            if(i < playerLives)
            {
                Hearts[i].enabled = true;
            }
            else
            {
                Hearts[i].enabled = false;
            }
        }
    }

    public void IncreaseScore(int value)
    {
        playerScore += value;
        scoreText.text = playerScore.ToString();

    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    private void UpdateTimerText(float time)
    {
        string minutes = Mathf.Floor(time / 60).ToString("00");
        string seconds = Mathf.Floor(time % 60).ToString("00");
        string milliseconds = Mathf.Floor((time * 1000) % 1000).ToString("000");

        timerText.text = minutes + ":" + seconds + ":" + milliseconds;
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }

    public void StartTimer()
    {
        isTimerRunning = true;
        startTime = Time.time;
    }




    // UI Button Methods
    public void OnClimbUpButtonDown()
    {
        isClimbingUp = true;
    }

    public void OnClimbUpButtonUp()
    {
        isClimbingUp = false;
    }

    public void OnClimbDownButtonDown()
    {
        isClimbingDown = true;
    }

    public void OnClimbDownButtonUp()
    {
        isClimbingDown = false;
    }

    public void OnRightButtonDown()
    {
        isMovingRight = true;
    }

    public void OnRightButtonUp()
    {
        isMovingRight = false;
    }

    public void OnLeftButtonDown()
    {
        isMovingLeft = true;
    }

    public void OnLeftButtonUp()
    {
        isMovingLeft = false;


    }

    public void OnJumpButtonDown()
    {
        isJumpingButton = true;
    }


    public void OnAttackDown()
    {
        isAttackingButton = true;
    }



}


