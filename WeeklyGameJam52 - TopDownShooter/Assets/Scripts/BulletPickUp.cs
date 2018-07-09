using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class BulletPickUp : MonoBehaviour {

	public int ammoIndex;

    [FMODUnity.EventRef]
    public string pickUpSound;


	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Player"){
			GuyController guy = collision.gameObject.GetComponent<GuyController>();
			guy.ammoNum[ammoIndex]++;
            FMODUnity.RuntimeManager.PlayOneShot(pickUpSound);
			Destroy(gameObject);
		}
	}
}
