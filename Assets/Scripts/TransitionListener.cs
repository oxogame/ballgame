using Sirenix.OdinInspector;
using UnityEngine;

public class TransitionListener : MonoBehaviour
{
    private int currAnimId;

    private int prevAnimId;

    public RV_AnimDataList Data;

    public AnimationManager animationManager;
    Animator animator;

    public bool testing;
    float durationOfTransition = 0f;

    [SerializeField]
    RV_AnimDataList animDataList;

    private void Start()
    {
        testing = true;
        animator = this.GetComponent<Animator>();
        animationManager = GameObject.Find("AnimationManager").GetComponent<AnimationManager>();
    }

    // Animasyon Eventlerinden tetikleniyor.
    public void CheckEvent(int status)
    {
        
        currAnimId = this.GetComponent<Animator>().GetInteger("AnimId");

        //Debug.Log(Data.tranlist[0].TransitionTime);
        //Debug.Log("Checking Event----------- ");
        //Debug.Log("Animation " + currAnimId + " --> " + nextAnimId);       
        print("AAAANIM : " + prevAnimId + "_" + currAnimId);
        if (testing)
        {
            print(Time.time - durationOfTransition);
            if (animDataList.tranList.ContainsKey(prevAnimId + "_"+ currAnimId)) 
            {
                print("KAydediliyor : " + prevAnimId + "_" + currAnimId + " : " + (Time.time - durationOfTransition));
                animDataList.tranList[prevAnimId + "_" + currAnimId].TransitionTime = Time.time - durationOfTransition;
            }
            
            this.GetComponent<Animator>().speed = 0;
        }
        else 
        {
            Play(animator.GetInteger("AnimId"));                      
        }
        
    }

    public void Play(int AnimId)
    {
        animator.speed = 1;
        if (testing)
        {
            durationOfTransition = Time.time;
            prevAnimId = animator.GetInteger("AnimId");
            animator.SetInteger("AnimId", AnimId);
            currAnimId = animator.GetInteger("AnimId");
            Debug.Log("Animation " + prevAnimId + " --> " + currAnimId);
        }
        else
        {
            playAuto();
        }
    }

    [Button(ButtonSizes.Gigantic)]
    public void PlayAnim() 
    {
        Play(animator.GetInteger("AnimId"));
    }   

    [Button(ButtonSizes.Gigantic)]
    public void Play1()
    {       
        Play(1);
    }

    [Button(ButtonSizes.Gigantic)]
    public void Play2()
    {
        Play(2);
    }

    [Button(ButtonSizes.Gigantic)]
    public void Play3()
    {
        Play(3);
    }

    [Button(ButtonSizes.Gigantic)]
    public void Play4()
    {
        Play(4);
    }

    void playAuto() 
    {
        if (!testing) 
        {           
            if (animationManager.animationList.Count > 0)
            {
                animator.SetInteger("AnimId", animationManager.animationIntegers[animationManager.animationList[0]]);
                animationManager.animationList.RemoveAt(0);
            }
        }       
    }

}
