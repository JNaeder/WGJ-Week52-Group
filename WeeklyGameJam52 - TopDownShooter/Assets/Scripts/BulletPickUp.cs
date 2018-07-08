using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPickUp : MonoBehaviour {

	public int ammoIndex;


	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Player"){
			GuyController guy = collision.gameObject.GetComponent<GuyController>();
			guy.ammoNum[ammoIndex]++;
			Destroy(gameObject);
		}
	}
}
