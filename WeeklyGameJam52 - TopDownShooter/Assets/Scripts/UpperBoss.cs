using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class UpperBoss : MonoBehaviour {

	public float distanceToPlayer;
	float angleBetweenBullets;
	bool isAttacking;
	float enemyStartSpeed;

	public float distThreshold = 10f;
	public float attackFreq = 5f;
	public int numberOfBulletsInAttack = 10;
	public int enemyDamage;
	public GameObject bullet;

	Enemy enemy;

	// Use this for initialization
	void Start () {
		enemy = GetComponent<Enemy>();
		enemyStartSpeed = enemy.speed;
		angleBetweenBullets = 360 / numberOfBulletsInAttack;
	}
	
	// Update is called once per frame
	void Update () {
		GetDistance();
		if (enemy.guy != null)
		{
			if (distanceToPlayer <= distThreshold)
			{
				if (!isAttacking)
				{

					StartCoroutine("Attack1");
				}

			}
			else
			{
				enemy.speed = enemyStartSpeed;
			}
		}

	}


	void GetDistance(){
		if (enemy.guy != null)
		{
			distanceToPlayer = Vector3.Distance(enemy.guy.transform.position, transform.position);
		}
	}





	IEnumerator Attack1(){
		isAttacking = true;
		enemy.speed = 0;
		yield return new WaitForSeconds(1);
			for (int i = 0; i < numberOfBulletsInAttack; i++){
			GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, i * angleBetweenBullets)));
			Bullet instBullet = newBullet.GetComponent<Bullet>();
			instBullet.SetDamage(enemyDamage, 5, 0, 0);
				//print("Spawn Bullet at angle " + i * angleBetweenBullets);
			}
		yield return new WaitForSeconds(attackFreq);
		isAttacking = false;
        
	}
}
