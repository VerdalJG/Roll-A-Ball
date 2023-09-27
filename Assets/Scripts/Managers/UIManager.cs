using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameMode
{
    menu, // Menu scene
    ready, // Level ready
    playing, // Level playing
    levelEnd // Level finish
}

public class UIManager : MonoBehaviour
{
    [Header("Level Times")]
    public int timeL1 = 90; //90 Seconds
    public int timeL2 = 60; // 60 Seconds
    public int timeL3 = 60; // 60 Seconds

    [Header("Text")]
    public Text uitScoreText; // Reference to score text
    public Text uitTimerText; // Reference to timer text
    public Text uitStartHighScoreText; // Reference to Start panel score text
    public Text uitStartScoreReqText; // Reference to Start panel required score text
    public Text uitGoalScoreText; // Reference to Goal panel score text
    public Text uitGoalScoreReqText; // Reference to Goal panel required score text

    [Header("Panels")]
    public GameObject uipGoal; // Reference to Goal panel
    public GameObject uipStartLevel; // Reference to Start Panel

    [Header("Set Dynamically")]
    private GameMode mode = GameMode.menu; // Enum variable
    public static float timerGame; // Timer variable
    private static int maxTimeLevel; // Max time per level used to set color of timer

    void Update()
    {
        UpdateMode(); // Always check what state the gamemode is
        uitScoreText.text = "Score: " + GameManager.score; // Update Score Text

        if (SceneManager.GetActiveScene().buildIndex != 0 && mode == GameMode.ready) // Game Start
        {
            Time.timeScale = 0; // Freeze Game

            switch (SceneManager.GetActiveScene().buildIndex) // Set timer based on level
            {
                case 1:
                    timerGame = timeL1; // 90
                    maxTimeLevel = timeL1;
                    break;

                case 2:
                    timerGame = timeL2; // 120
                    maxTimeLevel = timeL2;
                    break;

                case 3:
                    timerGame = timeL3; // 60
                    maxTimeLevel = timeL3;
                    break;
            }
        }

        else if (mode == GameMode.playing) // Start timer when in "playing" state
        {
            Time.timeScale = 1; // Unfreeze time
            timerGame -= Time.deltaTime; // Timer functionality
            uitTimerText.text = "" + Mathf.FloorToInt(timerGame); // Set text to time left as an integer
            ColorTimer();
        }

        else if (mode == GameMode.levelEnd)
        {
            uipGoal.SetActive(true); // Panel active and modify text
            uitGoalScoreText.text = "Level " + GameManager.level + " Score: " + GameManager.score;
            uitGoalScoreReqText.text = "Level " + GameManager.level + " Required Score: " + GameManager.scoreRQ1;
            ResetMode(); // Reset game mode to menu
        }
        if (timerGame <= 0) // Game over, time out
        {
            mode = GameMode.menu;
        }
    }


    void UpdateMode() // Called when changing game states
    {
        if (SceneManager.GetActiveScene().buildIndex != 0 && mode == GameMode.menu) // Level accessed
        {
            mode = GameMode.ready; // Start panel and show score required and highscore
            uipStartLevel.SetActive(true);
            switch (GameManager.level)
            {
                case 1:
                    uitStartHighScoreText.text = "Level " + GameManager.level + " High Score: " + PlayerPrefs.GetInt("L1HighScore");
                    uitStartScoreReqText.text = "Level " + GameManager.level + " Required Score: " + GameManager.scoreRQ1;
                    break;
                case 2:
                    uitStartHighScoreText.text = "Level " + GameManager.level + " High Score: " + PlayerPrefs.GetInt("L2HighScore");
                    uitStartScoreReqText.text = "Level " + GameManager.level + " Required Score: " + GameManager.scoreRQ2;
                    break;
                case 3:
                    uitStartHighScoreText.text = "Level " + GameManager.level + " High Score: " + PlayerPrefs.GetInt("L3HighScore");
                    uitStartScoreReqText.text = "Level " + GameManager.level + " Required Score: " + GameManager.scoreRQ3;
                    break;
            }
            
        }
        
        else if (mode == GameMode.playing)
        {
            uipStartLevel.SetActive(false);
        }
    }

    public void StartLevel() // Method called by pressing start button
    {
        mode = GameMode.playing;
        GameManager.StartLevel();
    }
    
    void ColorTimer()
    {
        if (timerGame == maxTimeLevel) // Text Color turns white at lots of time left
        {
            uitTimerText.color = Color.white;
        }

        if (timerGame / maxTimeLevel <= 0.5 && timerGame > 10) // Text Color turns yellow at half time left
        {
            uitTimerText.color = Color.yellow;
        }

        else if (timerGame <= 10 && mode == GameMode.playing) // Text Color turns red at 10 seconds left
        {
            uitTimerText.color = Color.red;
        }

    }

    private void LateUpdate()
    {
        if (Goal.goalMet) // When the goal is met
        {
            mode = GameMode.levelEnd;
        }
    }

    void ResetMode() // Reset enum variable to menu
    {
        mode = GameMode.menu;
    }
}
