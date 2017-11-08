using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private static int INF = 1000000;
    public Transform transform;
	public Node[] Nodes;
	private Node Parent;
    private float weight;
    private int prev;
    private bool isOpen;
	
	// Use this for initialization
	void Start () {
        weight = INF;
        prev = -1;
        isOpen = false;
		foreach (Node node in Nodes)
		{
			//Debug.Log(node.gameObject.name);
			node.Parent = this;
		}	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public float getWeight()
    {
        return this.weight;
    }
    public void setWeight(float weight)
    {
        this.weight = weight;
    }

    public int getPrev()
    {
        return this.prev;
    }
    public void setPrev(int prev)
    {
        this.prev = prev;
    }
    public bool getIsOpen()
    {
        return this.isOpen;
    }
    public void setIsOpen(bool isOpen)
    {
        this.isOpen = isOpen;
    }
}
