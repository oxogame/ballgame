﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchManagerGuitarHero : MonoBehaviour
{
    // 21.5 is the ideal range between the node and focus point to accept a successful tap.
    [SerializeField]
    float idealRange = 21.5f, tolerance = 3f, extraToleranceForLong = 2f, gameSpeed = 1f, nodeCheckFrequency = 0.5f, powerBarStep; // ideal range + tolerance range to accept the successful tap.
    float nextNodeCheck = 0f;

    [SerializeField]
    List<Transform> NodeList;

    [SerializeField]
    Transform FocusPoint;

    [SerializeField]
    Image PowerBar;

    Transform nodePool;

    public bool busyWithLong = false;
    float longTouchBeganError = 0f;
    float longTouchEndError = 0f;

    

    void Start()
    {
        PrepareTheNodeList();
        print("# of Nodes : " + NodeList.Count);
    }

    void Update()
    {
        MoveNodePool();
        NodeCheck();
        Touched();
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

    void Touched()
    {
        if (Input.touchCount > 0) 
        {
            if (NodeList.Count > 0) 
            {
                if (NodeList[0].tag == "Short")
                {
                    TapProcess(Input.GetTouch(0));
                }
                else if (NodeList[0].tag == "Long")
                {
                    LongTouchProcess(Input.GetTouch(0));
                }
            }      
        }
    }

    void MoveNodePool() 
    {
        nodePool.Translate(-gameSpeed * Time.deltaTime, 0, 0);
    }
    int counter = 1;

    void NodeCheck() 
    {
        if (NodeList.Count > 0) 
        {
            if (NodeList[0].tag == "Short")
            {
                NodePassedWithoutTappedProcess();
            }
            else if (NodeList[0].tag == "Long") 
            {

                NodePassedWithoutTappedProcess();

            }
            
        }       
    }

    void NodePassedWithoutTappedProcess() 
    {
        if (busyWithLong)
        {
            if (NodeList[0].GetChild(0).position.x - FocusPoint.position.x < - tolerance + extraToleranceForLong)
            {
                busyWithLong = false;
                NodeList[0].GetComponent<Image>().color = new Color(0f, 0f, 0f);              
                NodeList.RemoveAt(0);
            }
        }
        else 
        {
            if (NodeList[0].GetChild(0).position.x - FocusPoint.position.x < -tolerance)
            {
                NodeList[0].GetComponent<Image>().color = new Color(0f, 0f, 0f);
                NodeList.RemoveAt(0);
            }

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
    void TapProcess(Touch touch) 
    {
        if (touch.phase == TouchPhase.Began)
        {
            if (NodeList[0].GetChild(1).position.x - tolerance < FocusPoint.position.x)
            {
                //NodeList[0].GetComponent<Image>().color = new Color(118f, 212f, 144f);
                SuccessfulMove();
                NodeList.RemoveAt(0);
            }
        }
    }
    void LongTouchProcess(Touch touch) 
    {
        switch(touch.phase)
        {
            case TouchPhase.Began:               
                if (NodeList[0].GetChild(1).position.x - tolerance < FocusPoint.position.x)
                {
                    busyWithLong = true;
                    longTouchBeganError = Mathf.Abs(Mathf.Abs(NodeList[0].GetChild(1).position.x) - Mathf.Abs(FocusPoint.position.x)) - tolerance;                                     
                }
                
                break;

            case TouchPhase.Ended:
                if (busyWithLong) 
                {
                    busyWithLong = false;
                    SuccessfulMove();
                    longTouchEndError = Mathf.Abs(Mathf.Abs(NodeList[0].GetChild(0).position.x) - Mathf.Abs(FocusPoint.position.x)) - tolerance + extraToleranceForLong;
                    Debug.Log(" BEGANERROR : " + longTouchBeganError + " ::: " + " ENDERROR : " + longTouchEndError + " TOTAL ERROR  : " + (longTouchEndError + longTouchBeganError));
                    NodeList.RemoveAt(0);
                }
                
                break;
        }
    }

    void SuccessfulMove() 
    {
        NodeList[0].GetComponent<Image>().color = new Color(118f, 212f, 144f);
        if (PowerBar.rectTransform.anchoredPosition.x < 1) 
        {
            PowerBar.rectTransform.anchoredPosition = new Vector3(PowerBar.rectTransform.anchoredPosition.x + powerBarStep, 0f, 0f);
        }
        
    }
}
