using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSpawner : MonoBehaviour {

	public GameObject[] ammo;
	public int minDropNum, maxDropNum;
	public float dropRange;
	public float waitTime = 5f;

	float timeTime;
	bool isVisible;
	bool hasSpawned;



	SpriteRenderer sP;
    
	// Use this for initialization
	void Start () {
		sP = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

		//Debug.Log("isvisible " + isVisible + " and spawned is " + hasSpawned);

		if(sP.isVisible){
			isVisible = true;         
		} else {
			isVisible = false;       
		}



		if(!isVisible){
			if(!hasSpawned){
				
					DropItems();
				
			} 
		} 
	}

	void DropItems()
    {
        int randNum = Random.Range(minDropNum, maxDropNum);
        for (int i = 0; i < randNum; i++)
        {
			timeTime = waitTime;
			hasSpawned = true;
            Vector3 randomDir = new Vector3(Random.Range(-dropRange, dropRange), Random.Range(-dropRange, dropRange), 0);
            // float randomSpeed = Random.Range(1f, 3f);
			int randomDropNum = Random.Range(0, ammo.Length);
            Instantiate(ammo[randomDropNum], randomDir + transform.position, Quaternion.identity);

        }



    }


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Player"){
			hasSpawned = false;
		}
	}



	void SpawnAmmo(){
		hasSpawned = true;

		int randNum = Random.Range(0, ammo.Length);
		Instantiate(ammo[randNum], transform.position, Quaternion.identity);

	}
}
