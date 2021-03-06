﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    float damage;
    float bulletSpeed;
	float knockbackTime;
	float knockbackPower;

	public bool doesRotate;
	public float rotSpeed;


	Vector3 shootDirection;

	private void Start()
	{
		shootDirection = transform.up;


	}


	void Update () {

		transform.position += shootDirection * Time.deltaTime * bulletSpeed;

		if(doesRotate){

			transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * rotSpeed);
		}


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
            if (enemy != null)
            {
                enemy.TakeDamage(damage, knockbackTime, knockbackPower);
            }

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
