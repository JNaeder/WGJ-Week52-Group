using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Guns")]
public class Gun : ScriptableObject  {

    public string gunName;
    public float  fireRate;
    public float damage;
    public float bulletSpeed;
    public GameObject gunObject;
    public GameObject bullet;



    public bool isAutomatic;

	
}
