using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVisual : MonoBehaviour {
    public float riseSpeed;
    public float fadeTime;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, fadeTime);
	}
	
	// Update is called once per frame
	void Update () {

        transform.position += Vector3.up * Time.deltaTime * riseSpeed;


	}
}
