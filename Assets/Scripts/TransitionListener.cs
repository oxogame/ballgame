using Sirenix.OdinInspector;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;


public class TransitionListener : MonoBehaviour
{

    [SerializeField]
    private int prevAnimId, currAnimId;
    public int animCounter = 0;
    private int currAnimInteger;
    private int prevAnimInteger;

    public RV_AnimDataList Data;

    public AnimationManager animationManager;
    public TouchManagerGuitarHero2 touchManager;
    public Animator animator;

    public bool testing;
    float durationOfTransition = 0f;

    [SerializeField]
    RV_AnimDataList animDataList;

    [SerializeField]
    Transform ball, testCube;
    Vector3 midPoint;
    [SerializeField]
    float ballHeightUnity = 1f, toleranceRange = 0.25f, highLightToggleFreq= 0.1f, helperHeightOffset = 0f;
    bool ballHighlightOn = false;
    float nextHighlightToggle = 0f;
    GameObject ballHeighlight;
    GameObject helper;

    float listenStop;
    public float listenStart;

    public bool rightTimeToTap = false;
    bool rightTimeAvailableToChange = true;

    public bool touched = false;
    public bool failed = false;
    
    

    [SerializeField]
    public Text startCounter;

    private void Start()
    {
        
        testing = false;
        animator = this.GetComponent<Animator>();
        animationManager = GameObject.Find("AnimationManager").GetComponent<AnimationManager>();
        StartCoroutine(Counter());
        ballHeighlight = ball.GetChild(0).gameObject;
        helper = GameObject.Find("Helper2");


    }

    private void Update()
    {
        if (Time.time > listenStart)
        {           
            
            if (rightTimeAvailableToChange)
            {
                rightTimeAvailableToChange = false;
                rightTimeToTap = true;
                touched = false;
            }
            /*if (!ballHeighlight.activeSelf) 
            {
                Highlighter(true);
            }*/

        }
        /*else if (Time.time < listenStop)
        {
            if (rightTimeAvailableToChange)
            {                
                rightTimeAvailableToChange = false;
                rightTimeToTap = true;
            }
            /*if (!ballHeighlight.activeSelf)
            {
                Highlighter(true);
            }*/
        //}*/
        else 
        {
            
            if (!rightTimeAvailableToChange)
            {
                rightTimeAvailableToChange = true;
                rightTimeToTap = false;
            }
            if (ballHeighlight.activeSelf)
            {
                Highlighter(false);
            }
        }
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

            //Debug.Log("Animation " + prevAnimId + " --> " + currAnimId);
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
        if (animationManager.animationList.Count > 0)
        {

            if (animCounter > 1 && !touched && rightTimeToTap)
            {
                //touched = true;
                touchManager.FailProcess();
            }
            if (!failed)
            {
                prevAnimInteger = currAnimInteger;
                currAnimInteger = animationManager.animationIntegers[animationManager.animationList[0]];
                prevAnimId = animator.GetInteger("AnimId");
                animator.SetInteger("AnimId", currAnimInteger);
                currAnimId = animator.GetInteger("AnimId");
                helper.transform.position = new Vector3(animDataList.moveFigureList[currAnimInteger.ToString()].BallPosition.x, 
                    animDataList.moveFigureList[currAnimInteger.ToString()].BallPosition.y + helperHeightOffset, 
                    animDataList.moveFigureList[currAnimInteger.ToString()].BallPosition.z);
            }
            else 
            {
                prevAnimInteger = currAnimInteger;
                currAnimInteger = 10;
                prevAnimId = animator.GetInteger("AnimId");
                animator.SetInteger("AnimId", 10);
                currAnimId = animator.GetInteger("AnimId");
            }
                
            animCounter++;

            //setListenTimeForCurrentKick(animDataList.tranList[prevAnimId + "_" + currAnimId].TransitionTime);

            //print(" transition : " + prevAnimId + "_" + currAnimId + " : Duration : " + animDataList.tranList[prevAnimId + "_" + currAnimId].TransitionTime);
            if (animCounter > 1) 
            {
                //print("REMOVING : " + animationManager.animationList[0]);
                animationManager.animationList.RemoveAt(0);
                //print(" POS ERROR : prev : " + prevAnimInteger + " curr : " + currAnimInteger);
                moveTheBall(animDataList.moveFigureList[prevAnimInteger.ToString()].BallPosition, 
                    animDataList.moveFigureList[currAnimInteger.ToString()].BallPosition, 
                    animDataList.tranList[prevAnimId + "_" + currAnimId].TransitionTime * (1 / animator.GetFloat("TimeFactor")));
            }
        }
    }

    void moveTheBall(Vector3 currentPos, Vector3 targetPos, float duration)
    {
        midPoint = midPointFinder(currentPos, targetPos, duration);
        Vector2 travelDurations = travelDurationPortions(currentPos.y, targetPos.y, midPoint.y, duration);
        setListenTimeForCurrentKick(duration);
        //Instantiate(testCube, midPoint, Quaternion.identity);
        //Instantiate(testCube, targetPos, Quaternion.identity);

        GoUp(travelDurations, targetPos);

    }

    private void GoUp(Vector2 travelDurations, Vector3 targetPos)
    {
        Debug.Log("ball goes up");
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
        
        //print(" PORTIONS TOTAL : " + duration + " : riseDuration : " + riseDuration + " . dropDuration : " + dropDuration + " : riseDurationRatio : " + (midY - currentY / (midY - currentY + midY - targetY)));
        return new Vector2(riseDuration, dropDuration);
    }

    void setListenTimeForCurrentKick(float duration) 
    {
        listenStop = Time.time + toleranceRange;
        listenStart = Time.time + duration - toleranceRange;
        touchManager.touchableStartTime = listenStart;
        touchManager.bestTouchTime = Time.time + duration;
        //print(  " STOP :  " + listenStop + " :: Start :  " + listenStart);
    }

    IEnumerator Counter() 
    {
        failed = false;
        animator.SetInteger("AnimId", 0);
        touched = false;
        animCounter = 0;
        ball.position = animDataList.moveFigureList["0"].BallPosition;//new Vector3(1.21f, -2.04f, -0.26f);
        for (int i = 2; i > 0; i--) 
        {
            yield return new WaitForSeconds(1f);
            startCounter.text = i.ToString();           
        }
        yield return new WaitForSeconds(1f);
        startCounter.text = "GO";
        // Build Apk purposes
        
        Play(0); // input doesn't matter
        // Build Apk purposes //
    }

    public void rePlay() 
    {
        print("BUTTON ACTIVE");
        animationManager.GenerateAnimationSerieDirected();
        StartCoroutine(Counter());
    }

    void Highlighter(bool on) 
    {
        ballHeighlight.SetActive(on);
        /*if (nextHighlightToggle < Time.timeSinceLevelLoad) 
        {
            nextHighlightToggle = Time.timeSinceLevelLoad + highLightToggleFreq;
            ballHeighlight.SetActive(!ballHeighlight.activeSelf); 
        }*/

    }

    

}
