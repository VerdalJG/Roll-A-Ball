using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    [Header("Set Dynamically")]
    private Vector3 offset;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        offset = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            transform.position = player.transform.position + offset;
        }
    }
}
