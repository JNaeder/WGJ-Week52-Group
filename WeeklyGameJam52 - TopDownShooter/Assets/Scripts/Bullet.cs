using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    float damage;
    float bulletSpeed;
	
	
	void Update () {

        transform.position += transform.up * Time.deltaTime * bulletSpeed;


	}


    public void SetDamage(float newDamage, float newBulletSpeed) {
        damage = newDamage;
        bulletSpeed = newBulletSpeed;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
		if(collision.gameObject.tag == "Enemy"){
			Enemy enemy = collision.gameObject.GetComponent<Enemy>();
			enemy.TakeDamage(damage);

		}


        Destroy(gameObject);
        
    }
}
