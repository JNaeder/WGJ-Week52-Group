using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float health;
	public float speed;
	public float damageToPlayer;
	public int points;
	public Transform healthBarGreen;
	public GameObject[] drops;
    public float dropRange;
    public int minDropNum, maxDropNum;


	float healthPerc, startHealthNum;

	Transform target;
    GuyController guy;

    // Use this for initialization
    void Start () {
		startHealthNum = health;
        if (FindObjectOfType<GuyController>() != null)
        {
            target = FindObjectOfType<GuyController>().transform;
        }

		GameManager.numberOfEnemiesLeft++;
        guy = FindObjectOfType<GuyController>();
    }
	
	// Update is called once per frame
	void Update () {
		SetUpHealth();
		FollowPlayer();

	}


	void SetUpHealth(){
		healthPerc = health / startHealthNum;

		Vector3 healthBarScale = healthBarGreen.localScale;
		healthBarScale.x = healthPerc;
		healthBarGreen.localScale = healthBarScale;
	}


	public void TakeDamage(float damage){
		health -= damage;
		if(health <= 0){
			Death();

		}
	}


	void Death()
	{
		GameManager.numberOfEnemiesLeft--;
		GameManager.score += points;
        DropItems();
        Destroy(gameObject);
        if (Mathf.Round(guy.specialCap) < guy.specialStartNum && !guy.isSpecialTime)
        {
            guy.specialCap++;
        }
		
	}

	void DropItems(){
		int randNum = Random.Range(minDropNum, maxDropNum);
		for (int i = 0; i < randNum; i++){
            Vector3 randomDir = new Vector3(Random.Range(-dropRange, dropRange), Random.Range(-dropRange, dropRange), 0);
           // float randomSpeed = Random.Range(1f, 3f);
            int randomDropNum = Random.Range(0, drops.Length);
            Instantiate(drops[randomDropNum], randomDir + transform.position, Quaternion.identity);

        }

		

	}

	void FollowPlayer(){
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }


	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Player"){
			
			guy.TakeDamage(damageToPlayer);
		}
	}
}
