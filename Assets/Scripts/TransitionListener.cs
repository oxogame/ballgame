using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionListener : MonoBehaviour
{
    private int currAnimId;

    private int nextAnimId;


    public void CheckEvent(int status)
    {
        currAnimId = status;
        nextAnimId = this.GetComponent<Animator>().GetInteger("AnimId");

        Debug.Log("Checking Event----------- ");
        Debug.Log("Animation " + currAnimId + " --> " + nextAnimId);
    }

    [Button(ButtonSizes.Gigantic)]
    public void Play1()
    {
        this.GetComponent<Animator>().SetInteger("AnimId", 1);
    }

    [Button(ButtonSizes.Gigantic)]
    public void Play2()
    {
        this.GetComponent<Animator>().SetInteger("AnimId", 2);
    }

    [Button(ButtonSizes.Gigantic)]
    public void Play3()
    {
        this.GetComponent<Animator>().SetInteger("AnimId", 3);
    }

    [Button(ButtonSizes.Gigantic)]
    public void Play4()
    {
        this.GetComponent<Animator>().SetInteger("AnimId", 4);
    }
}
