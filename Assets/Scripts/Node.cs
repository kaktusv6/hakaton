using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

	public Node[] Nodes;
	public Node Parent;
	
	// Use this for initialization
	void Start () {
		foreach (Node node in Nodes)
		{
			Debug.Log(node.gameObject.name);
			node.Parent = this;
		}	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
