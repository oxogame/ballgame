﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchManagerGuitarHero2 : MonoBehaviour
{
    // 21.5 is the ideal range between the node and focus point to accept a successful tap.
    [SerializeField]
    float idealRange = 21.5f, tolerance = 3f, extraToleranceForLong = 2f, gameSpeed = 1f, nodeCheckFrequency = 0.5f, powerBarStep; // ideal range + tolerance range to accept the successful tap.
    float nextNodeCheck = 0f;

    [SerializeField]
    List<Transform> NodeList;

    [SerializeField]
    Transform focusPoint,finishpoint, player;

    [SerializeField]
    Image powerBar;

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
        MoveFocusPoint();
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

    void MoveFocusPoint()
    {
        if (focusPoint.position.x < finishpoint.position.x) 
        {
            focusPoint.Translate(gameSpeed * Time.deltaTime, 0, 0);
        }
        
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
            if (NodeList[0].GetChild(0).position.x - focusPoint.position.x < -tolerance + extraToleranceForLong)
            {
                busyWithLong = false;
                NodeList[0].GetComponent<Image>().color = new Color(0f, 0f, 0f);
                NodeList.RemoveAt(0);
            }
        }
        else
        {
            if (NodeList[0].GetChild(0).position.x - focusPoint.position.x < -tolerance)
            {
                NodeList[0].GetComponent<Image>().color = new Color(0f, 0f, 0f);
                NodeList.RemoveAt(0);
            }

        }
    }

    void TapProcess(Touch touch)
    {
        if (touch.phase == TouchPhase.Began)
        {
            if (NodeList[0].GetChild(1).position.x - tolerance < focusPoint.position.x)
            {
                //NodeList[0].GetComponent<Image>().color = new Color(118f, 212f, 144f);
                SuccessfulMove();
                NodeList.RemoveAt(0);
            }
        }
    }
    void LongTouchProcess(Touch touch)
    {
        switch (touch.phase)
        {
            case TouchPhase.Began:
                if (NodeList[0].GetChild(1).position.x - tolerance < focusPoint.position.x)
                {
                    busyWithLong = true;
                    longTouchBeganError = Mathf.Abs(Mathf.Abs(NodeList[0].GetChild(1).position.x) - Mathf.Abs(focusPoint.position.x)) - tolerance;
                }

                break;

            case TouchPhase.Ended:
                if (busyWithLong)
                {
                    busyWithLong = false;
                    SuccessfulMove();
                    longTouchEndError = Mathf.Abs(Mathf.Abs(NodeList[0].GetChild(0).position.x) - Mathf.Abs(focusPoint.position.x)) - tolerance + extraToleranceForLong;
                    Debug.Log(" BEGANERROR : " + longTouchBeganError + " ::: " + " ENDERROR : " + longTouchEndError + " TOTAL ERROR  : " + (longTouchEndError + longTouchBeganError));
                    NodeList.RemoveAt(0);
                }

                break;
        }
    }

    void SuccessfulMove()
    {
        NodeList[0].GetComponent<Image>().color = new Color(118f, 212f, 144f);
        if (powerBar.rectTransform.anchoredPosition.x < 1)
        {
            powerBar.rectTransform.anchoredPosition = new Vector3(powerBar.rectTransform.anchoredPosition.x + powerBarStep, 0f, 0f);
        }

    }

    

}