using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {

	bool isOpen;

	public float stayOpenTime;
	public float openTime;

	Animator anim;
	YCordToSPOrderLayer sPOrder;

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
		if(Input.GetKeyDown(KeyCode.E)){
			isOpen = true;


		}
	}
}
