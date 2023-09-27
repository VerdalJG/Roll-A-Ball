using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public GameObject menuManagerPrefab; // MenuManager Prefab

    public MenuManager menuScript; //MenuManager Script

    public GameObject gameManagerPrefab; //GameManagerPrefab

    private void Start()
    {
        //Create MenuManager and GameManager if they do not exist.
        if (GameObject.FindWithTag("Menu") == null)
        {
            GameObject menuManager = Instantiate(menuManagerPrefab);
            menuScript = menuManager.GetComponent<MenuManager>();
            
        }
        else if (GameObject.FindWithTag("Menu") != null) // Get menumanager script
        {
            menuScript = GameObject.FindGameObjectWithTag("Menu").GetComponent<MenuManager>();
        }

        if (GameObject.FindWithTag("GameManager") == null)
        {
            GameObject gameManager = Instantiate(gameManagerPrefab);
        }
    }

    //Button calls this because menu manager doesn't exist initially in the scene. It communicates what index to use for the menumanager.
    public void MenuChange(int index)
    {
        if (index == 1)
        {
            menuScript.SceneChange(1);
        }
        if (index == 2)
        {
            menuScript.SceneChange(2);
        }
        if (index == 3)
        {
            menuScript.SceneChange(3);
        }
        if (index == 4)
        {
            menuScript.SceneChange(4);
        }
    }
}
