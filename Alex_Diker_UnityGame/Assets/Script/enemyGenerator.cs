/*
Alex Diker
George Brown College
100746284
COMP3064 


*/

using UnityEngine;
using System.Collections;


public class enemyGenerator : MonoBehaviour {

	public GameObject monster;

	private float timeBetweenEnemies = 2f;  
	private int secondsBeforeFirstEnemyAppears = 1; // Wait X seconds before the first enemy appears after the game starts

	private float timeLastMonster;


	void FixedUpdate () {

		if (Time.realtimeSinceStartup > secondsBeforeFirstEnemyAppears && Time.time >= timeLastMonster) {

		GameObject.Instantiate(monster);
            timeLastMonster = Time.time + timeBetweenEnemies;

		}
	}
}