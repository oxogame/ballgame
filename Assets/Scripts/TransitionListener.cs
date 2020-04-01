using Sirenix.OdinInspector;
using UnityEngine;
using DG.Tweening;

public class TransitionListener : MonoBehaviour
{
    private int currAnimId;

    private int prevAnimId;
    private int currAnimInteger;
    private int prevAnimInteger;

    public RV_AnimDataList Data;

    public AnimationManager animationManager;
    Animator animator;

    public bool testing;
    float durationOfTransition = 0f;

    [SerializeField]
    RV_AnimDataList animDataList;

    [SerializeField]
    Transform ball, testCube;
    Vector3 midPoint;
    [SerializeField]
    float ballHeightUnity = 1f;

    private void Start()
    {
        testing = true;
        animator = this.GetComponent<Animator>();
        animationManager = GameObject.Find("AnimationManager").GetComponent<AnimationManager>();
    }

    // Animasyon Eventlerinden Topun vucuda degdigi anda tetikleniyor.
    public void CheckEvent(int status)
    {      
        //currAnimId = this.GetComponent<Animator>().GetInteger("AnimId");   
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
                prevAnimInteger = currAnimInteger;
                currAnimInteger = animationManager.animationIntegers[animationManager.animationList[0]];
                prevAnimId = animator.GetInteger("AnimId");
                animator.SetInteger("AnimId", currAnimInteger);
                currAnimId = animator.GetInteger("AnimId");
                animationManager.animationList.RemoveAt(0);
                print(" transition : " + prevAnimId + "_" + currAnimId);
                print(" transition : " + prevAnimId + "_" + currAnimId + " : Duration : " + animDataList.tranList[prevAnimId + "_" + currAnimId].TransitionTime);
                if (prevAnimId != 0) 
                {
                    moveTheBall(animDataList.moveFigureList[prevAnimInteger.ToString()].BallPosition, animDataList.moveFigureList[currAnimInteger.ToString()].BallPosition, animDataList.tranList[prevAnimId + "_" + currAnimId].TransitionTime);
                }
            }
        }       
    }

    void moveTheBall(Vector3 currentPos, Vector3 targetPos, float duration)
    {
        midPoint = midPointFinder(currentPos, targetPos, duration);
        Vector2 travelDurations = travelDurationPortions(currentPos.y, targetPos.y, midPoint.y, duration);
        //DOTween.Kill(ball);
        //print(" Transition : " + prevAnimId + "_" + currAnimId + " Ball Target Pos : " + targetPos);
        Instantiate(testCube, midPoint, Quaternion.identity);
        Instantiate(testCube, targetPos, Quaternion.identity);

        //ball.DOMove(targetPos, duration).SetEase(Ease.InOutBack);
        print("TARGET POOS : " + targetPos);
        GoUp(travelDurations, targetPos);

        //ball.DOMoveY(midPoint.y, travelDurations.y).SetEase(Ease.OutQuad).SetDelay(travelDurations.x);

    }

    private void GoUp(Vector2 travelDurations, Vector3 targetPos)
    {
        ball.DOMoveY(midPoint.y, travelDurations.x).SetEase(Ease.OutQuad).OnComplete(()=>GoDown(travelDurations, targetPos));
        ball.DOMoveX(midPoint.x, travelDurations.x).SetEase(Ease.Linear);
        ball.DOMoveZ(midPoint.z, travelDurations.x).SetEase(Ease.Linear);
    }
    private void GoDown(Vector2 travelDurations, Vector3 targetPos)
    {
        print("GO DOWN : " + travelDurations.y);
        ball.DOMoveY(targetPos.y, travelDurations.y).SetEase(Ease.InQuad);
        ball.DOMoveX(targetPos.x, travelDurations.y).SetEase(Ease.Linear);
        ball.DOMoveZ(targetPos.z, travelDurations.y).SetEase(Ease.Linear);
    }

    Vector3 midPointFinder(Vector3 currentPos, Vector3 targetPos, float duration) 
    {
        float midX = targetPos.x - ((targetPos.x - currentPos.x) / 2);
        float midZ = targetPos.z - ((targetPos.z - currentPos.z) / 2);
        float midY = Mathf.Max(currentPos.y, targetPos.y) +(duration / 0.1f) * ballHeightUnity;
        return new Vector3(midX, midY, midZ);
    }
    Vector2 travelDurationPortions(float currentY, float targetY, float midY, float duration) 
    {
        float riseDuration = duration*(Mathf.Abs( midY - currentY ) / (Mathf.Abs(midY - currentY) + Mathf.Abs(midY - targetY)));
        float dropDuration = duration - riseDuration;
        print(" PORTIONS TOTAL : " + duration + " : riseDuration : " + riseDuration + " . dropDuration : " + dropDuration + " : riseDurationRatio : " + (midY - currentY / (midY - currentY + midY - targetY)));
        return new Vector2(riseDuration, dropDuration);
    }
}
