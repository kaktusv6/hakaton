using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildWay : MonoBehaviour {

    public GameObject head;
    public GameObject tail;

	void Start () {

        float distance = pointDistance(head.transform.position, tail.transform.position);
        Vector3 vector = getVectorTwoPoints(head.transform.position, tail.transform.position);
        Vector3 basis = new Vector3(vector.x / distance, vector.y / distance, vector.z / distance);
        Debug.Log(distance);
        Debug.Log(vector);
        Debug.Log(basis);

        for(int i = 0; i < distance; i++)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            cube.transform.position = new Vector3(basis.x*i, basis.y*i, basis.z*i);
        }

		
	}

    private float pointDistance(Vector3 head, Vector3 tail)
    {
        return Mathf.Sqrt(Mathf.Pow(head.x - tail.x, 2) + Mathf.Pow(head.y - tail.y, 2) + Mathf.Pow(head.z - tail.z, 2));
    }

    private Vector3 getVectorTwoPoints(Vector3 head, Vector3 tail)
    {
        return new Vector3(tail.x - head.x, tail.y - head.y, tail.z - head.z);
    }

}
