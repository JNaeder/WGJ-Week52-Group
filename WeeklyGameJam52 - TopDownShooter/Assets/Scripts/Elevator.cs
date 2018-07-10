using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class Elevator : MonoBehaviour {

	bool isOpen;

	public float stayOpenTime;
	public float openTime;

	Animator anim;
	YCordToSPOrderLayer sPOrder;

    [FMODUnity.EventRef]
    public string elevatorSound;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		openTime = stayOpenTime;
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetBool("isOpen", isOpen);



		if(isOpen){
			openTime -= Time.deltaTime;
			if(openTime < 0){
				isOpen = false;
				openTime = stayOpenTime;
			}

		}
	}



	private void OnTriggerStay2D(Collider2D collision)
	{
        if (collision.gameObject.tag == "Player") {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isOpen = true;
            }


		}
	}



    public void PlayEvelatorSound() {
        FMODUnity.RuntimeManager.PlayOneShot(elevatorSound);


    }
}
