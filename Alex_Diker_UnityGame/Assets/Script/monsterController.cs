/*
Alex Diker
George Brown College
100746284
COMP3064 


*/


using UnityEngine;
using System.Collections;

public class monsterController : MonoBehaviour {


	public float enemySpeed = 0.5f; 
 // so we set a speed variable here for enemy
	public GameObject monsterBullet;

// varaibles for bullet physics and math
    private bool canShoot = false;
    private float timeBetweenBullets = 0.5f;  // time between single bullets 0.5
    private float timeLastShot;
    private float monsterBulletYOffset = -0.02f;
    private float monsterBulletXOffset = -0.1f;
	private int bulletCounter = 0; //init bullet counter (to avoid continuous shooting)
	private int bulletsPerSeries = 3; // how many bullets are shot before a short break
	private float timeBetweenShotsSeries;  // seconds between a bullet discharge and another
	private float timingShotSeries;
	


	void Start () {
// enemy is invisable until player enters camera view 
		GetComponent<Collider2D>().enabled = false;

// shooting time ( break and shoot ) 
		timeBetweenShotsSeries = timeBetweenBullets* bulletsPerSeries + 3;

//monster Starting Position X & Y ranges depend on the scene scale (they must be chosen according to the scene)
		transform.position = new Vector3(Random.Range(2.45f, 4f), Random.Range(-1.70f, 0.44f), transform.position.z);

	}
	void FixedUpdate () {

// Move the monster from right to left
		GetComponent<Rigidbody2D>().AddForce(new Vector2(-0.1f*enemySpeed, 0), ForceMode2D.Impulse);

//Simple code to actually allow bullets to move 
		if (canShoot && Time.time >= timeLastShot && bulletCounter < bulletsPerSeries) {
			Instantiate(monsterBullet, transform.position+new Vector3 (monsterBulletXOffset, monsterBulletYOffset, 0), Quaternion.identity);
			timeLastShot = Time.time + timeBetweenBullets;
			bulletCounter++;
		} 

// Restart shooting after the break
		if (Time.time >= timingShotSeries) {
			bulletCounter=0;
			timingShotSeries = Time.time + timeBetweenShotsSeries;
		} 

	}

	void OnBecameVisible() {  

// monster might not have the capability to shoot bullets \\ chance 
		if (monsterBullet != null) {
			canShoot = true;
		}

// player can now damage enemy 
        GetComponent<Collider2D>().enabled = true;
	}

//called when the object goes out of the screen
	void OnBecameInvisible() {  
		Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D thisObject)
	{
//Increase score as needed as GUI
		Camera.main.GetComponent<GUI>().currentScore++;
 
// Make the monster quickly flash, then delete/destroy
		StartCoroutine(blinkUponCollisionAndDestroy(gameObject));

	}
	IEnumerator blinkUponCollisionAndDestroy(GameObject thisPlayer)
	{
		thisPlayer.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
		yield return new WaitForSeconds(0.2f);
		Destroy(gameObject);
	}

}