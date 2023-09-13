using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    private float startTime;
    public Text timerText;
    private bool isTimerRunning = true;

    [SerializeField] int playerLives = 3;
    [SerializeField] int playerScore = 0;

    [SerializeField] Image[] Hearts;

    [SerializeField] Text scoreText, livesText;

    private void Awake()
    {
        int numberOfSessions = FindObjectsOfType<GameSession>().Length;

        if (numberOfSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        scoreText.text = playerScore.ToString();

        startTime = Time.time;
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
            SceneManager.LoadScene(5);
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
}


