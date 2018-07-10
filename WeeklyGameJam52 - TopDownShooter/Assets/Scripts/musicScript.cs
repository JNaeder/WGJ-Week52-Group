using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class musicScript : MonoBehaviour {

	[FMODUnity.EventRef]
	public string music;

	FMOD.Studio.EventInstance musicInst;

	public static musicScript musicStatic;

    GameManager gM;

	private void Awake()
	{
		if(musicStatic == null){
			musicStatic = this;

		} else {

			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);

		musicInst = FMODUnity.RuntimeManager.CreateInstance(music);
		musicInst.start();

        gM = FindObjectOfType<GameManager>();

	}
	
	// Update is called once per frame
	void Update () {

        musicInst.setParameterValue("Wave", gM.wave);

	}


    public void SetGameManager(GameManager newGM) {
        gM = newGM;

    }
}
