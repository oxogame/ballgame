using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMekanik2TransitionListener : MonoBehaviour
{
    Animator animator;
    [SerializeField]
    TestMekanik2TouchManager touchManager;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }
    public void CheckEvent() 
    {
        animator.SetInteger("AnimId", 0);
    }
    
}
