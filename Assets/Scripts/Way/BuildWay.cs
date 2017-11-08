using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class BuildWay : MonoBehaviour {

    private static string NODE = "Node";

//    public Node start;
    public Node finish;
    public int countNodes;
    public GameObject relation;
    public GameObject way;
    List<List<float>> graf;

//    private Transform rootOther;
    
    void Start () {

        countNodes = GameObject.FindGameObjectsWithTag("Node").Length;
        initGraf();
//        rootOther = GameObject.FindGameObjectWithTag("Other");
    }

    public void run(Node node)
    {
        node.setWeight(0);
        
        buildShortWay();
        printWay(finish);
    }

    private void createEdge(Vector3 head, Vector3 tail, GameObject obj)
    {
        if (!obj) return;
        float distance = pointDistance(head, tail);
        Vector3 vector = getVectorTwoPoints(head, tail);
        Vector3 basis = getBasis(vector, distance);

        Vector3 dir = tail - head;
        dir = dir.normalized;
        
        for (int i = 0; i < distance; i++)
        {
            GameObject dot = Instantiate(obj, head + basis * i, Quaternion.identity, transform);
            dot.transform.forward = dir;
        }
    }

    private float pointDistance(Vector3 head, Vector3 tail)
    {
        Vector3 dis = head - tail;
        return dis.magnitude;
    }

    private Vector3 getVectorTwoPoints(Vector3 head, Vector3 tail)
    {
        return tail - head;
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

                Node nodeHead = GameObject.Find(i.ToString()).GetComponent<Node>();
                Node nodeTail = GameObject.Find(j.ToString()).GetComponent<Node>();

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
        Node next = gameObject.GetComponent<Node>();

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
