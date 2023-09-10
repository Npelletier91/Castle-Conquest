using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{

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
        livesText.text = playerLives.ToString();
        scoreText.text = playerScore.ToString();
    }



    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGame();
        }
    }




    public void TakeLife()
    {
        playerLives--;

        UpdateHearts();
        livesText.text = playerLives.ToString();
    }

    public void IncreaseLife()
    {
        playerLives++;

        if(playerLives >= 3)
        {
            playerLives = 3;
        }

        UpdateHearts();

        livesText.text = playerLives.ToString();
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
}


