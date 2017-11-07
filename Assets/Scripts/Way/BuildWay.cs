using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildWay : MonoBehaviour {

    public GameObject head;
    public GameObject tail;
    public GameObject dotWay;

	void Start () {

        for (float x = head.transform.position.x; x <= tail.transform.position.x; x++)
        {
            for (float y = head.transform.position.y; y <= tail.transform.position.y; y++)
            {
                for (float z = head.transform.position.z; z <= tail.transform.position.z; z++)
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    //cube.AddComponent<Rigidbody>();
                    cube.transform.position = new Vector3(x, y, z);
                }
            }
        }
		
	}
	
	void Update () {
		
	}
}
