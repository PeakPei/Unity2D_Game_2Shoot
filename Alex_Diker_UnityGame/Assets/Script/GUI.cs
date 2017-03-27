/*
Alex Diker
George Brown College
100746284
COMP3064 


*/

using UnityEngine;
using System.Collections;
// added UnityEngine.UI 
using UnityEngine.UI;
// I am using UnityEngine.UI is used in order to handle the text thats on the camera view.

public class GUI : MonoBehaviour {

	public int currentScore = 0; // currentScore is increased from monsterController
	public bool isGameOver; // from playerController.cs >> asset 
	public int playerLives; // from playerController.cs >> asset

	private Text healthText;
	private Text scoreText;
	private Text endGameText;

	void Start () {

		healthText = GameObject.Find("Lives").GetComponent<Text>();
		scoreText = GameObject.Find("Score").GetComponent<Text>();
        endGameText = GameObject.Find("GameOver").GetComponent<Text>(); 

	}

	void Update () {

		playerLives = GameObject.Find("Player").GetComponent<playerController>().playerLives;
		isGameOver = GameObject.Find("Player").GetComponent<playerController>().isGameOver;

 // Update scores and Health of player on every frame
        healthText.text = "Health: "+playerLives;
		scoreText.text = "Score: "+currentScore;

		if (isGameOver) {
            endGameText.enabled = true;
		}
	}
}