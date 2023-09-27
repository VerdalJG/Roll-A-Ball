using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 10;
    private float moveVertical;
    private float moveHorizontal;
    public GameMode mode = GameMode.ready;
    private int bumperImpulse = 15;
    public Vector3 playerStartPos;
    public MenuManager menuScript;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.position = GameObject.FindGameObjectWithTag("StartPos").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (mode == GameMode.playing) // Controls only work in playing state.
        {
            moveHorizontal = Input.GetAxisRaw("Horizontal");
            moveVertical = Input.GetAxisRaw("Vertical");
        }

        if (speed > 10)
        {
            speed -= 5f;
        }

        if (speed < 10)
        {
            speed = 10f;
        }

        if (transform.position.y <= -5)
        {
            menuScript = GameObject.FindGameObjectWithTag("Menu").GetComponent<MenuManager>();
            menuScript.SceneChange(0);
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PickUp") // Check if object is a collectible
        {
            other.gameObject.SetActive(false);
            string name = other.gameObject.name;
            
            if (name.StartsWith("PickUp1"))
            {
                GameManager.AddScore(200);
            }

            else if (name.StartsWith("PickUp2"))
            {
                GameManager.AddScore(400);
            }

            else if (name.StartsWith("PickUpN"))
            {
                GameManager.AddScore(-300);
            }
        }

        if (other.gameObject.tag == "SpeedBoost") // Speedboost Panel
        {
            speed = 100;
        }

        if (other.gameObject.tag == "Goal")
        {
            mode = GameMode.levelEnd;
            rb.velocity = Vector3.zero; //Stop moving
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        Bumper collidedWith = collision.gameObject.GetComponent<Bumper>();
        if (collision.gameObject.tag == "Bumper" && !collidedWith.bumperUsed)
        {
            collidedWith.bumperUsed = true;
            Vector3 diff = transform.position - collision.transform.position;
            rb.velocity = diff * bumperImpulse;
        }
    }

    public void PlayerStart() // Set game state to "playing" - Button Activation
        {
            mode = GameMode.playing;
        }
}
