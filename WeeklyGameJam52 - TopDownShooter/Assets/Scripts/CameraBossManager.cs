using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBossManager : MonoBehaviour {

	CameraBoss[] cameras;
	GameManager gM;

	int numOfCamerasOn;


	// Use this for initialization
	void Start () {
		cameras = GetComponentsInChildren<CameraBoss>();
		gM = FindObjectOfType<GameManager>();
	}


	private void Update()
	{
		if (gM.wave > 2)
		{
			numOfCamerasOn = gM.wave - 2;
		}
	}


	public void ActivateCameras(){
		for (int i = 0; i < cameras.Length; i++){
			cameras[i].Deactivate();         
		}

		for (int i = 0; i < numOfCamerasOn; i++){
			int randNum = Random.Range(0, cameras.Length);
            cameras[randNum].Activate();
		}      
	}


	public void DeActivateAllCams(){

		for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].Deactivate();
        }
	}
}
