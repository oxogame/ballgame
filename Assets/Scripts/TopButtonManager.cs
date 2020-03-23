using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopButtonManager : MonoBehaviour
{
    List<Transform> NodeList;
    Transform nodePool;
    void Start()
    {
        PrepareTheNodeList();
        print("# of Nodes : " + NodeList.Count);
    }


    void Update()
    {
        
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

    public void TopButtonTapped() 
    {
    
    }


}
