using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public static bool goalMet = false;

    public void Start()
    {
        Goal.goalMet = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Check to see if it is 'the player
        if (other.gameObject.tag == "Player")
        {
            goalMet = true;
        }
    }
}
