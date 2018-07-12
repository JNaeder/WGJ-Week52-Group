using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class BulletPickUp : MonoBehaviour {

	public int ammoIndex;
    public int ammoAmount = 1;
    public bool isRandomBox;

    [FMODUnity.EventRef]
    public string pickUpSound;


	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Player"){
			GuyController guy = collision.gameObject.GetComponent<GuyController>();
            if (!isRandomBox)
            {
                guy.ammoNum[ammoIndex] += ammoAmount;
            }
            else {
                int randNum = Random.Range(0, 5);
                int randAmount = Random.Range(0, ammoAmount);
                guy.ammoNum[randNum] += randAmount;

            }

            FMODUnity.RuntimeManager.PlayOneShot(pickUpSound);
            Destroy(gameObject);
		}
	}
}
