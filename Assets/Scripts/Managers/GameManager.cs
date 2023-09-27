using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [Header("Set Dynamically")]
    public static int score = 0;
    public static int scoreRQ1 = 5000; // Required Score for Level 1
    public static int scoreRQ2 = 5000; // Required Score for Level 2
    public static int scoreRQ3 = 5000; // Required Score for Level 3
    public static int highScore1 = 5000; // High Score for Level 1
    public static int highScore2 = 5000; // High Score for Level 2
    public static int highScore3 = 5000; // High Score for Level 3
    public static bool level1Pass = false;
    public static bool level2Pass = false;
    public static bool level3Pass = false;

    public static GameMode mode = GameMode.menu;
    public static int level;
    public MenuManager menuScript;

    void Awake()
    {
        //If the high scores already exists, read it, else - Assign the high scores to their respective levels
        if (PlayerPrefs.HasKey("L1HighScore")) // If the key 'L1HighScore' exists
        {
            highScore1 = PlayerPrefs.GetInt("L1HighScore"); // Read whatever value is stored with this key
        }
        else // If the key doesn't exist
        {
            PlayerPrefs.SetInt("L1HighScore", highScore1); // Create the key and set the value from the variable bestScoreL1
        }

        if (PlayerPrefs.HasKey("L2HighScore"))
        {
            highScore2 = PlayerPrefs.GetInt("L2HighScore");
        }
        else
        {
            PlayerPrefs.SetInt("L2HighScore", highScore2);
        }

        if (PlayerPrefs.HasKey("L3HighScore"))
        {
            highScore3 = PlayerPrefs.GetInt("L3HighScore");
        }
        else
        {
            PlayerPrefs.SetInt("L3HighScore", highScore3);
        }
    }

    void Start()
    {
        DontDestroyOnLoad(this); // Don't destroy on scene load
        menuScript = GameObject.FindGameObjectWithTag("Menu").GetComponent<MenuManager>();
    }

    public static void StartLevel() // Set game state to "playing", and check what level we are in - Button Click
    {
        mode = GameMode.playing;
        level = SceneManager.GetActiveScene().buildIndex;
        score = 0;
    }

    void Update() // Called once per frame  
    {
        if (SceneManager.GetActiveScene().buildIndex != 0 && mode == GameMode.ready) // Freeze game when in ready state
        {
            Time.timeScale = 0; //Freeze
        }

        if (Goal.goalMet && mode == GameMode.playing)
        {
            score += (Mathf.FloorToInt(UIManager.timerGame) * 100);//Bonus score based on time left
            mode = GameMode.levelEnd;
        }

        if (SceneManager.GetActiveScene().buildIndex != 0 && mode == GameMode.levelEnd)
        {
            switch (level)
            {
                case 1:
                    if (score > scoreRQ1)
                    {
                        level1Pass = true; 
                    }
                    if (score > highScore1)
                    {
                        highScore1 = score;
                        PlayerPrefs.SetInt("L1HighScore", highScore1);
                    }
                    Invoke("BackToMenu", 4f);
                    mode = GameMode.menu;
                    break;
                case 2:
                    if (score > scoreRQ2)
                    {
                        level2Pass = true;
                    }
                    if (score > highScore2)
                    {
                        highScore2 = score;
                        PlayerPrefs.SetInt("L2HighScore", highScore2);
                    }
                    Invoke("BackToMenu", 4f);
                    mode = GameMode.menu;
                    break;
                case 3:
                    if (score > scoreRQ3)
                    {
                        level3Pass = true;
                    }
                    if (score > highScore3)
                    {
                        highScore3 = score;
                        PlayerPrefs.SetInt("L3HighScore", highScore3);
                    }
                    Invoke("BackToMenu", 4f);
                    mode = GameMode.menu;
                    break;
            }
        }
        if (UIManager.timerGame <= 0)
        {
            Invoke("BackToMenu", 0 );
            mode = GameMode.menu;
        }
    }

    public static void AddScore(int value) // Function that adds score, called when an object is picked up.
    {
        score += value;
    }

    void BackToMenu()
    {
        menuScript.SceneChange(0);
    }
}
