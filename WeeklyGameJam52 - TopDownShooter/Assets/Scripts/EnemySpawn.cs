using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {


	public GameObject[] enemies;
	public float[] perc;
	public Transform spawnPoint;


	GameManager gM;

	private void Start()
	{
		gM = FindObjectOfType<GameManager>();
	}
    
	public void SpawnNextEnemy(){
		float randPerc = Random.Range(0f, 100f);
		if(gM.wave == 1){
			Instantiate(enemies[0], spawnPoint.position, spawnPoint.rotation);

		} else if(gM.wave == 2){
			Instantiate(enemies[0], spawnPoint.position, spawnPoint.rotation);

		} else if(gM.wave == 3){
			if(randPerc > 80){
				Instantiate(enemies[1], spawnPoint.position, spawnPoint.rotation);
			} else {
				Instantiate(enemies[0], spawnPoint.position, spawnPoint.rotation);
			}

		} else if(gM.wave == 4){
			if (randPerc > 65)
            {
                Instantiate(enemies[1], spawnPoint.position, spawnPoint.rotation);
            }
            else
            {
                Instantiate(enemies[0], spawnPoint.position, spawnPoint.rotation);
            }
		} else if(gM.wave == 5){
			if (randPerc > 50)
            {
                Instantiate(enemies[1], spawnPoint.position, spawnPoint.rotation);
            }
            else
            {
                Instantiate(enemies[0], spawnPoint.position, spawnPoint.rotation);
            }
		}else if(gM.wave == 6){
			if (randPerc > 40)
            {
                Instantiate(enemies[1], spawnPoint.position, spawnPoint.rotation);
            }
            else
            {
                Instantiate(enemies[0], spawnPoint.position, spawnPoint.rotation);
            }
		} else if(gM.wave >= 7){
			if (randPerc > 20)
            {
                Instantiate(enemies[1], spawnPoint.position, spawnPoint.rotation);
            }
            else
            {
                Instantiate(enemies[0], spawnPoint.position, spawnPoint.rotation);
            }

		}





      

	}
}
