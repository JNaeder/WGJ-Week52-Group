using UnityEngine;
using System.Collections.Generic;
using PolyNav;

//example
[RequireComponent(typeof(PolyNavAgent))]
public class FollowTarget : MonoBehaviour{

	public Transform target; 
	
	private PolyNavAgent _agent;
	private PolyNavAgent agent{
		get {return _agent != null? _agent : _agent = GetComponent<PolyNavAgent>();}
	}

	private void Start()
	{
		target = FindObjectOfType<GuyController>().transform;

	}

	void Update() {
		if (target != null){
			agent.SetDestination( target.position );
		}
	}
}