using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


/// <summary>
/// The class uses INHERITANCE example by inheriting GameManager class and override its method by updating the UI part
/// </summary>
public class UIHandler : GameManager
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public Button restartButton;
    public GameObject pauseScreen;

    private bool paused;
    private bool isUpdated = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            CheckForPause();
        }

        // Update score and lives only one time when the game is started
        if (isGameActive && !isUpdated)
        {
            UpdateLives(3);
            UpdateScore(score);
            isUpdated = true;
        }

    } 

    public override void UpdateScore(int scoreToAdd)
    {
        base.UpdateScore(scoreToAdd);
        scoreText.text = "Score : " + score;
    }

    public override void UpdateLives(int livesToChange)
    {        
        base.UpdateLives(livesToChange);

        livesText.text = "Lives : " + lives;

        if (lives <= 0)
        {
            GameOver();
        }
    }

    public override void GameOver()
    {
        base.GameOver();
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);

    }

    private void CheckForPause()
    {
        if (!paused)
        {
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }
}

