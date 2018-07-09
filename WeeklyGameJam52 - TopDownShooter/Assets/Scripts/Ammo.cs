﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Ammo", menuName = "Ammo")]
public class Ammo : ScriptableObject{

	public string ammoName;
	public float damage;
	public float knockbackTime;
	public float knockbackPower;
	public float bulletSpeed;
	public GameObject bulletToSpawn;
	public Sprite UIIcon;
}
