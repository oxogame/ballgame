using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMekanik2TouchManager : MonoBehaviour
{
    [SerializeField]
    GameObject player, helper;
    Animator animator;

    [SerializeField]
    AnimationManager animationManager;

    [SerializeField]
    RV_AnimDataList animDataList;

    int currAnimId = 0;
    int prevAnimId = 0;

    [SerializeField]
    float helperHeightOffset = 0.25f;


    void Start()
    {
        animator = player.GetComponent<Animator>();
    }

    void Update()
    {
        Touched();
    }

    public void Touched()
    {
        if ((Input.touchCount > 0 || Input.GetMouseButtonDown(0)) &&  animator.GetCurrentAnimatorStateInfo(0).IsName("E1") )
        {
            if (Input.GetMouseButtonDown(0))
            {
                TapProcess();
            }
            else if(Input.touchCount > 0) 
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began) 
                {
                    TapProcess();
                }               
            }            
        }
    }

    void TapProcess() 
    {
        if (checkCorrectTap(Time.time)) 
        {
            if (animationManager.animationList.Count > 0)
            {
                currAnimId = animationManager.animationIntegers[animationManager.animationList[0]];
                prevAnimId = animator.GetInteger("AnimId");
                animator.SetInteger("AnimId", currAnimId);
                animationManager.animationList.RemoveAt(0);
            }

            if (animDataList.moveFigureList.ContainsKey((currAnimId+1).ToString())) 
            {
                helper.transform.position = new Vector3(animDataList.moveFigureList[(currAnimId+1).ToString()].BallPosition.x,
                    animDataList.moveFigureList[(currAnimId + 1).ToString()].BallPosition.y + helperHeightOffset,
                    animDataList.moveFigureList[(currAnimId + 1).ToString()].BallPosition.z);
            }
            

        }
    }

    bool checkCorrectTap(float touchTime) 
    {
        return true;
    }
}
