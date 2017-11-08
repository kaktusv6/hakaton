using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixShake : MonoBehaviour
{

	public GameObject ArCame;
//	public Transform TargetTransform;
	
	private float x;
	private float y;
	private float z;
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 pos = ArCame.transform.position;
		x += (pos.x - x) / 8;
		y += (pos.y - y) / 8;
		z += (pos.z - z) / 8;
		Vector3 can = new Vector3(x, y, z);
		transform.position = can;
		
//		transform.LookAt(TargetTransform);
//		can = transform.eulerAngles;
//		pos = ArCame.transform.eulerAngles;
//		can.z = pos.z;
//		transform.eulerAngles = can;
	}
}
