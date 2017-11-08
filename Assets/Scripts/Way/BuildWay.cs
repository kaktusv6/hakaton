using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildWay : MonoBehaviour {

    private static string NODE= "Node";

    public Node start;
    public Node finish;
    public int countNodes;
    public PrimitiveType relation;
    public PrimitiveType way;
    List<List<float>> graf;

    void Start () {
        start.setWeight(0);

        initGraf();
        buildShortWay();
        printWay(finish);
        
	}

    private void createEdge(Vector3 head, Vector3 tail, PrimitiveType obj)
    {
        float distance = pointDistance(head, tail);
        Vector3 vector = getVectorTwoPoints(head, tail);
        Vector3 basis = getBasis(vector, distance);

        for (int i = 0; i < distance; i++)
        {
            GameObject objImp = GameObject.CreatePrimitive(obj);
            //GameObject objImpl = new GameObject();
            //objImpl = obj;
            objImp.transform.position = new Vector3(head.x+basis.x * i, head.y + basis.y * i, head.z + basis.z * i);
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
                            createEdge(nodeHead.transform.position, nodeTail.transform.position, relation);
                        }
                    }
                   
                }

            }
        }

    }
    

    private void printWay(Node node)
    {
        Debug.Log(node.name);
        GameObject gameObject = GameObject.Find(node.getPrev().ToString());
        if (gameObject == null) return;
        Node next = gameObject.GetComponent("Node") as Node;

        createEdge(next.transform.position, node.transform.position, way);
        printWay(next);
    }


    private void buildShortWay() 
    {
        Node node = new Node();
        bool change = false;

        for (int j = 0; j < countNodes; j++)
        {
            change = false;
            node = new Node();

            for (int i = 0; i < countNodes; i++)
            {
                if (GameObject.Find(i.ToString()) != null)
                {
                    Node bufferNode = GameObject.Find(i.ToString()).GetComponent(NODE) as Node;

                    if ((node.getWeight() > bufferNode.getWeight()) && (bufferNode.getIsOpen() == false))
                    {
                        node = bufferNode;
                        change = true;
                    }
                } 
                
            }

            if (change == true)
            {
                step(node);
                node.setIsOpen(true);

            }

        }
    }

    private void step(Node node)
    {
        for(int i = 0; i < countNodes; i++)
        {
            if (Int32.Parse(node.name) == i) continue;
            if (GameObject.Find(i.ToString()) == null) continue;

            Node bufferNode = GameObject.Find(i.ToString()).GetComponent(NODE) as Node;

            if (node.getIsOpen() == true) continue; //если уже открыта
            if (graf[Int32.Parse(node.name)][i] == -1) continue; //если нет связи
            
            float sum = node.getWeight() + graf[Int32.Parse(node.name)][i];
            if (sum > bufferNode.getWeight()) continue; //если сумма больше чем было

            bufferNode.setWeight(sum); //сохраням новую метку
            bufferNode.setPrev(Int32.Parse(node.name));
            
        }

    }
}
