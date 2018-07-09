using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBossManager : MonoBehaviour {

	CameraBoss[] cameras;


	// Use this for initialization
	void Start () {
		cameras = GetComponentsInChildren<CameraBoss>();
	}


	public void ActivateCameras(){
		int randNum = Random.Range(0, cameras.Length);
		cameras[randNum].Activate();


	}
}
