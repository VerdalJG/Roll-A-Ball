using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    public bool bumperUsed; // Used for delay between bumper uses
    Vector3 bumperPos;
    bool movingRight;

    // Start is called before the first frame update
    void Start()
    {
        bumperUsed = false;
        bumperPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (bumperUsed)
        {
            Invoke("EnableBumper", 1);
        }

        if (gameObject.name.StartsWith("BumperL3"))
        {
            if (movingRight)
            {
                bumperPos.x += 0.4f;
                transform.position = bumperPos;
                if (bumperPos.x > 15)
                {
                    movingRight = false;
                }
            }
            
            if (!movingRight)
            {
                bumperPos.x -= 0.4f;
                transform.position = bumperPos;
                if (bumperPos.x < -20)
                {
                    movingRight = true;
                }
            }
        }
    }

    void EnableBumper()
    {
        bumperUsed = false;
    }
}
