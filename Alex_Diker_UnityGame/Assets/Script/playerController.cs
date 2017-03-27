/*
Alex Diker
George Brown College
100746284
COMP3064 


*/


using UnityEngine;
using System.Collections;


public class playerController : MonoBehaviour {

	public GameObject playerBullet;
	private float Speed = 6.0f; // force applied when fly button is tapped
    private float pushDownForce = 1.0f; 
	private float playerBulletXOffset = 0.5f;
	private float playerBulletYOffset = 0f;
	private float timeBetweenShots = 0.2f;  // 0.2 = 5 shots per second
    public bool isGameOver = false; // for GUI
    public int playerLives = 3; // for GUI
    private float timestamp;

	void FixedUpdate () {
        Move(); 
	}

    private void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * Speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * Speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Shoot();
        }
    }


	void Shoot () {
// Shoot Bullet
		if (Time.time >= timestamp) {
// Used Quaternion identity instead of creating a new vector3 with Quaternion.Eulor..etc. 
            Instantiate(playerBullet, transform.position+new Vector3 (playerBulletXOffset,playerBulletYOffset,0), Quaternion.identity); 

			timestamp = timeBetweenShots + Time.time;
		}
	}

	void OnCollisionEnter2D(Collision2D thisObject)
	{
		playerDidCollide ();
	}

    void OnTriggerEnter2D(Collider2D thisObject)
    {
        playerDidCollide();
    }

    void playerDidCollide () {

		if (playerLives > 0) {
// lose 1 life
            playerLives = playerLives-1; 

        if (playerLives == 0)
            {
                gameOver();
            }

		}

	}
	void gameOver () {

		isGameOver = true; 
// This is picked by the Update function in GUI.cs
		Time.timeScale = 0.0F; 
// Stop the game -- as the game is over.
	}

	void Update () {

// restart the game with space key 

		if (isGameOver && Input.GetKey(KeyCode.Space)){
			Time.timeScale = 1.0F; // Restart the time
			Application.LoadLevel(Application.loadedLevel); // Reload this level
		}

	}


}