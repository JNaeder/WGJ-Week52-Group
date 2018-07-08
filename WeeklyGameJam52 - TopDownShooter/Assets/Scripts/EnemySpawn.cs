using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {


	public GameObject[] enemies;
	public Transform spawnPoint;




	public void SpawnNextEnemy(){
		Instantiate(enemies[0], spawnPoint.position, spawnPoint.rotation);


	}
}
