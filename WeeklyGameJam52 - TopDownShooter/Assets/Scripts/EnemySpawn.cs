using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {


	public GameObject[] enemies;
	public Transform spawnPoint;




	public void SpawnNextEnemy(){

		int randomNum = Random.Range(0, enemies.Length);

		Instantiate(enemies[randomNum], spawnPoint.position, spawnPoint.rotation);


	}
}
