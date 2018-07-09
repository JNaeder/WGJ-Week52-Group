using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    float damage;
    float bulletSpeed;
	float knockbackTime;
	float knockbackPower;
	
	
	void Update () {

        transform.position += transform.up * Time.deltaTime * bulletSpeed;


	}


    public void SetDamage(float newDamage, float newBulletSpeed, float newKnockBackTime, float newKnockBackPower) {
        damage = newDamage;
        bulletSpeed = newBulletSpeed;
		knockbackTime = newKnockBackTime;
		knockbackPower = newKnockBackPower;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
		if(collision.gameObject.tag == "Enemy"){
			Enemy enemy = collision.gameObject.GetComponent<Enemy>();
			enemy.TakeDamage(damage, knockbackTime, knockbackPower);

		}

		if(collision.gameObject.tag == "Camera"){
			CameraBoss cam = collision.gameObject.GetComponent<CameraBoss>();
			cam.TakeDamage(damage);
		}

		if(collision.gameObject.tag == "Player"){
			GuyController guy = collision.gameObject.GetComponent<GuyController>();
			if (guy != null)
			{
				guy.TakeDamage(damage);
			}

		}


        Destroy(gameObject);
        
    }
}
