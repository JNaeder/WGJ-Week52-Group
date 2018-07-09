using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YCordToSPOrderLayer : MonoBehaviour {

    SpriteRenderer sP;

    public bool liveUpdate;
    public int offset;

	// Use this for initialization
	void Start () {
        sP = GetComponent<SpriteRenderer>();

        ConvertYCordToOrderLayer(transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {
        if (liveUpdate) {
            ConvertYCordToOrderLayer(transform.position.y);
        }
	}


    void ConvertYCordToOrderLayer(float yCord) {
        int newYCord = -(Mathf.RoundToInt(yCord));
        sP.sortingOrder = newYCord + offset;


    }
}
