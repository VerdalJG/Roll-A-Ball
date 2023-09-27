using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject button2;
    public Button level2Button;

    public GameObject button3;
    public Button level3Button;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {


        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            button2 = GameObject.Find("Button_Level2");
            button3 = GameObject.Find("Button_Level3");

            level2Button = button2.GetComponent<Button>();
            level3Button = button3.GetComponent<Button>();
        }

    }

    public void SceneChange(int index)
    {
        SceneManager.LoadScene(index);
        GameManager.level = index;
    }

    public void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (button2 == null)
            {
                button2 = GameObject.Find("Button_Level2");
            }
            if (button3 == null)
            {
                button3 = GameObject.Find("Button_Level3");
            }

            level2Button = button2.GetComponent<Button>();
            level3Button = button3.GetComponent<Button>();

            if (GameManager.level1Pass && !level2Button.interactable)
            {
                level2Button.interactable = true;
            }
            if (GameManager.level2Pass && !level3Button.interactable)
            {
                level3Button.interactable = true;
            }
        }
    }
}
