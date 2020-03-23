using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManagerGuitarHero : MonoBehaviour
{
    // 21.5 is the ideal range between the node and focus point to accept a successful tap.
    [SerializeField]
    float idealRange = 21.5f, tolerance = 3f, gameSpeed = 1f, nodeCheckFrequency = 0.5f; // ideal range + tolerance range to accept the successful tap.
    float nextNodeCheck = 0f;

    [SerializeField]
    List<Transform> NodeList;

    [SerializeField]
    Transform FocusPoint;    

    Transform nodePool;

    


    
    void Start()
    {
        PrepareTheNodeList();
        print("# of Nodes : " + NodeList.Count);
    }

    void Update()
    {
        moveNodePool();
        NodeCheck();
    }

    void PrepareTheNodeList()
    {
        nodePool = GameObject.Find("NodePool").transform;
        NodeList = new List<Transform>();
        foreach (Transform Node in nodePool)
        {
            NodeList.Add(Node);
        }
    }

    void Tapped()
    {
        if (Input.touchCount > 0) 
        {
            
        }
    }

    void moveNodePool() 
    {
        nodePool.Translate(-gameSpeed * Time.deltaTime, 0, 0);
    }
    int counter = 1;

    void NodeCheck() 
    {
        if (NodeList[0].GetChild(0).position.x - FocusPoint.position.x < -tolerance)
        {
            NodeList.RemoveAt(0);
        }
    }

    // The CPU friendly Version
    /*void NodeCheck() // If player can't touch the node and the node passes away the focus point, then we cancel that missed node here.
    {
        
        if (nextNodeCheck < Time.timeSinceLevelLoad) 
        {
            nextNodeCheck = Time.timeSinceLevelLoad + nodeCheckFrequency;
            
            if (NodeList[0].GetChild(0).position.x - FocusPoint.position.x < - tolerance) 
            {
                NodeList.RemoveAt(0);
            }
        }   
    }*/
}
