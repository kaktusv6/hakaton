using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildWay : MonoBehaviour {

    private static string NODE= "Node";

    public Node start;
    public Node finish;
    public int countNodes;
    //private float[][] graf;
    List<List<float>> graf;
    List<bool> mark;

    void Start () {
        //mark = new List<bool>();
        /*for (int i = 0; i < this.countNodes; i++)
        {
            mark.Add(false);
        }*/
        start.setWeight(0);

        initGraf();
        //testSecond(start);
        //printWay(finish);
        
        //Debug.Log(shortWay());
        //createEdges(root);
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


    private void initGraf()
    {
        graf = new List<List<float>>();
        List<float> row;

        for (int i = 0; i < this.countNodes; i++)
        {
            row = new List<float>();
            for (int j = 0; j < this.countNodes; j++)
            {
                row.Add(-1);
            }
            graf.Add(row);
        }

        for(int i = 0; i < this.countNodes; i++)
        {
            for(int j = 0; j < this.countNodes; j++)
            {
                if (i == j)
                {
                   continue;
                }

                if ((GameObject.Find(i.ToString()) == null) || (GameObject.Find(j.ToString()) == null)){
                    continue;
                }

                Node nodeHead = GameObject.Find(i.ToString()).GetComponent("Node") as Node;
                Node nodeTail = GameObject.Find(j.ToString()).GetComponent("Node") as Node;

                if (graf[i][j] == -1)
                {
                    for(int z = 0; z < nodeHead.Nodes.Length; z++)
                    {
                        if(nodeHead.Nodes[z] == nodeTail)
                        {
                            graf[i][j] = pointDistance(nodeHead.transform.position, nodeTail.transform.position);
                            graf[j][i] = graf[i][j];
                            createEdge(nodeHead.transform.position, nodeTail.transform.position);
                        }
                    }
                   
                }

            }
        }

    }
    
    
    private void test(Node node)
    {
        //Debug.Log(node.name);
        //Debug.Log("после");
        mark[Int32.Parse(node.name)] = true;
        for(int i = 0; i < countNodes; i++)
        {
            Node minWay = getMinNodeWay(node);
            if (minWay == null) continue;
            if (mark[Int32.Parse(minWay.name)] == true) continue;
            test(minWay);
        }

        
    }

    private Node getMinNodeWay(Node node)
    {
        float buffer = 0;
        int nodeCount = -1;


        for(int i = 0; i < countNodes; i++)
        {
           
            GameObject gameObject = GameObject.Find(i.ToString());
            if (gameObject == null) continue;
       
            Node nodeBuf = gameObject.GetComponent("Node") as Node;

            if((nodeBuf.getWeight() > (node.getWeight()+ graf[Int32.Parse(node.name)][i])) && ((graf[Int32.Parse(node.name)][i]) != -1))
            {
                nodeBuf.setWeight(node.getWeight() + graf[Int32.Parse(node.name)][i]);
                nodeBuf.setPrev(Int32.Parse(node.name));

                if (((graf[Int32.Parse(node.name)][i] < buffer) || (buffer == 0)) && (mark[i] == false))
                {
                    buffer = graf[Int32.Parse(node.name)][i];
                    nodeCount = i;
                }
            }
            
        }

        if (nodeCount == -1)
        {
            return null;
        }
        Node newNode = GameObject.Find(nodeCount.ToString()).GetComponent("Node") as Node;
        return newNode;
    }

    private void printWay(Node node)
    {
        Debug.Log(node.name);
        GameObject gameObject = GameObject.Find(node.getPrev().ToString());
        if (gameObject == null)
        {
            Debug.Log("ошибка");
            return;
        }
        Node next = gameObject.GetComponent("Node") as Node;

        createEdge(next.transform.position, node.transform.position);
        printWay(next);
    }


    private void testSecond(Node start) 
    {
        Node node = start;

        for (int i = 0; i < countNodes; i++)
        {
            GameObject gameObject = GameObject.Find(i.ToString());
            if (gameObject == null) continue;
            Node bufferNode = gameObject.GetComponent(NODE) as Node;

            if((node.getWeight() > bufferNode.getWeight()) && (node.getIsOpen() == false ))
            {
                node = bufferNode;
            }
        }

        step(node);
    }

    private void step(Node node)
    {
        node.setIsOpen(true);
        for(int i = 0; i < countNodes; i++)
        {
            GameObject gameObject = GameObject.Find(i.ToString());
            if (gameObject == null) continue;
            Node bufferNode = gameObject.GetComponent(NODE) as Node;

            //float sum = 
            //if()
        }
    }
}
