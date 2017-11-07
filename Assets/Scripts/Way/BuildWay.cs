using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildWay : MonoBehaviour {

    public Node root;
    //public Node tail;

	void Start () {
        createEdges(root);
	}

    private void createEdges(Node node)
    {
        if(node == null)
        {
            return;
        }
        for(int i = 0; i < node.Nodes.Length; i++)
        {
            //Debug.Log(node.name);

            //Debug.Log(node.transform.position);
            createEdge(node.transform.position, node.Nodes[i].transform.position);
            createEdges(node.Nodes[i]);
        }
        
    }

    private void createEdge(Vector3 head, Vector3 tail)
    {
        float distance = pointDistance(head, tail);
        Vector3 vector = getVectorTwoPoints(head, tail);
        Vector3 basis = getBasis(vector, distance);

        for (int i = 0; i < distance; i++)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            cube.transform.position = new Vector3(head.x+basis.x * i, head.y + basis.y * i, head.z + basis.z * i);
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

    private Vector3 getBasis(Vector3 vector, float distance)
    {
        return new Vector3(vector.x / distance, vector.y / distance, vector.z / distance);
    }
}
