using Sirenix.OdinInspector;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class TransitionListener : MonoBehaviour
{

    [SerializeField]
    private int prevAnimId, currAnimId;
    public int animCounter = 0;
    //private int currAnimInteger;
    //private int prevAnimInteger;

    public RV_AnimDataList Data;

    public AnimationManager animationManager;
    public TouchManagerGuitarHero2 touchManager;
    public Animator animator;

    public bool testing;
    float durationOfTransition = 0f;

    [SerializeField]
    RV_AnimDataList animDataList;

    [SerializeField]
    Transform ball, testCube, head;
    Vector3 midPoint;
    [SerializeField]
    float ballHeightUnity = 1f, toleranceRange = 0.25f, highLightToggleFreq= 0.1f, helperHeightOffset = 0f, ballHeadHeightDiffUpward = 1f, ballHeadHeightDiffDownward = 1f;
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

    // AnimTEst ON 
    public bool animTestOn = false;
    public bool specialAnim = false;

    Dictionary<string, List<Ease>> easeDict = new Dictionary<string, List<Ease>>() { {"Linear", new List<Ease>() {Ease.Linear, Ease.Linear } }, 
                                                                                     {"Quad", new List<Ease>() { Ease.InQuad, Ease.OutQuad } },
                                                                                     {"Quart", new List<Ease>() {Ease.InQuart, Ease.OutQuart } },
                                                                                     {"Quint", new List<Ease>() {Ease.InQuint, Ease.OutQuint } },};
    List<string> easeList = new List<string>() { "Linear", "Quad", "Quart", "Quint" };
    public string selectedEase = "Quad";
    int easeCounter = 0;

    [SerializeField]
    Text testText, easeText;
    // AnimTest ON //

    public bool headRotOn = false;
    List<int> headRotActions = new List<int>() {1,2,3,4 };
    [SerializeField]
    float headRotSpeed = 1f;

    [SerializeField]
    public Text startCounter;

    private void Start()
    {
        
        //testing = false;
        animator = this.GetComponent<Animator>();
        animationManager = GameObject.Find("AnimationManager").GetComponent<AnimationManager>();
        StartCoroutine(Counter());
        ballHeighlight = ball.GetChild(0).gameObject;
        helper = GameObject.Find("Helper2");

        if (animationManager.devAnimTestOn) 
        {
            AnimTestToggle();
        }


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
        //print(" ballHeadHeightDiffUpward :: " + (head.position.y - ball.position.y) + "ballHeadHeightDiffDownward : "+ (head.position.y - ball.position.y) );
        if (headRotOn && (( ballHeadHeightDiffUpward < head.position.y - ball.position.y) || (ballHeadHeightDiffDownward > head.position.y - ball.position.y)))
        {
            var lookPos = ball.position - head.position;
            var rotation = Quaternion.LookRotation(lookPos);

            head.rotation = Quaternion.Slerp(head.rotation, rotation, Time.deltaTime * headRotSpeed);
            head.localEulerAngles = new Vector3(head.localEulerAngles.x, head.localEulerAngles.y, 0f);
            
        }
        else
        {
            //head.rotation = Quaternion.Slerp(head.rotation, Quaternion.Euler(0f,0f,0f), Time.deltaTime * headRotSpeed);
            head.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

    // Animasyon Eventlerinden Topun vucuda degdigi anda tetikleniyor.
    public void CheckEvent(int status)
    {
        //currAnimId = this.GetComponent<Animator>().GetInteger("AnimId");
        
        if (testing)
        {
            print(Time.time - durationOfTransition);
            
            

            if (!animDataList.tranList.ContainsKey(prevAnimId + "_" + currAnimId))
                animDataList.tranList.Add(prevAnimId + "_" + currAnimId, new Assets.Scripts.TransitionVo());

            animDataList.tranList[prevAnimId + "_" + currAnimId].TransitionTime = (Time.time - durationOfTransition) / (1 / animator.GetFloat("TimeFactor"));
            print("KAydediliyor : " + prevAnimId + "_" + currAnimId + " : " + animDataList.tranList[prevAnimId + "_" + currAnimId].TransitionTime);
            /*float tempTimeFactor = animator.GetFloat("TimeFactor");
            float tempDuration = Time.time - durationOfTransition;

            print(" TIME FACTOR : " + tempTimeFactor + " tempDuration : " + tempDuration + " d/(1/a) : " + tempDuration / (1 / tempTimeFactor));
            animDataList.tranList[prevAnimId + "_" + currAnimId].TransitionTime = tempDuration / (1/tempTimeFactor);
            print("KAydediliyor : " + prevAnimId + "_" + currAnimId + " : " + animDataList.tranList[prevAnimId + "_" + currAnimId].TransitionTime); 
            */

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

    [Button(ButtonSizes.Large)]
    public void PlayKaldırmaSag2() 
    {
        animator.Play("KaldırmaSag");
        animator.speed = 1;
        animator.SetInteger("AnimId", 100);
    }

    [Button(ButtonSizes.Large)]
    public void PlayKaldırmaSol1()
    {
        animator.Play("KaldırmaSol");
        animator.speed = 1;
        animator.SetInteger("AnimId", 100);

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
    
    [Button(ButtonSizes.Gigantic)]
    public void Play5()
    {
        Play(5);
    }

    void playAuto() 
    {
        if (animationManager.animationList.Count > 0)
        {

            if (animCounter > 1 && !touched && rightTimeToTap && !animTestOn)
            {
                //touched = true;
                touchManager.FailProcess();
            }

            if (!failed)
            {
                if (specialAnim)
                {
                    PlaySpecialMoveAnim(); // SpecialAnim'e event atilacak ve o eventte specialAnim bool'u false'a cevirilecek.

                }
                else 
                {
                    SetNextAnim(animationManager.animationIntegers[animationManager.animationList[0]]);
                }
                              
            }
            else 
            {
                SetNextAnim(10);
            }
                
            animCounter++;

            //setListenTimeForCurrentKick(animDataList.tranList[prevAnimId + "_" + currAnimId].TransitionTime);

            //print(" transition : " + prevAnimId + "_" + currAnimId + " : Duration : " + animDataList.tranList[prevAnimId + "_" + currAnimId].TransitionTime);
            if (animCounter > 1) 
            {
                //print("REMOVING : " + animationManager.animationList[0]);
                if (animationManager.devAnimTestOn) // this if statement can be removed when this test is over
                {
                    
                    //string tempAnim = animationManager.animationList[0];
                    animationManager.animationList.Add(animationManager.animationList[0]);
                    //print(" ANIM ADD : " + animationManager.animationList.Count);
                }
                animationManager.animationList.RemoveAt(0);
                //print(" POS ERROR : prev : " + prevAnimId + " curr : " + currAnimId);
                moveTheBall(animDataList.moveFigureList[prevAnimId.ToString()].BallPosition, 
                    animDataList.moveFigureList[currAnimId.ToString()].BallPosition, 
                    animDataList.tranList[prevAnimId + "_" + currAnimId].TransitionTime * (1 / animator.GetFloat("TimeFactor")));
            }
        }
    }

    void headRotCOntroller() 
    {
        if (headRotActions.Contains(animator.GetInteger("AnimId")) && !failed)
        {
            headRotOn = true;

            //head.DOLookAt(ball, duration);
        }
        else
        {
            headRotOn = false;

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
        ball.DORotate(new Vector3(Random.Range(0,360), Random.Range(0, 360), Random.Range(0, 360)), duration, RotateMode.Fast);

    }

    private void GoUp(Vector2 travelDurations, Vector3 targetPos)
    {
        Debug.Log("ball goes up");
        ball.DOKill();
        ball.DOMoveY(midPoint.y, travelDurations.x).SetEase(easeDict[selectedEase][1]).OnComplete(()=>GoDown(travelDurations, targetPos));
        ball.DOMoveX(midPoint.x, travelDurations.x).SetEase(Ease.Linear);
        ball.DOMoveZ(midPoint.z, travelDurations.x).SetEase(Ease.Linear);
    }
    private void GoDown(Vector2 travelDurations, Vector3 targetPos)
    {
        //print("GO DOWN : " + travelDurations.y);
        ball.DOMoveY(targetPos.y, travelDurations.y).SetEase(easeDict[selectedEase][0]);
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

    public void rePlay() 
    {
        print("BUTTON ACTIVE");
        animationManager.GenerateAnimationSerieDirected();
        refreshValuesAtRePlay();
        StartCoroutine(Counter());
    }

    void refreshValuesAtRePlay() 
    {
        animator.SetInteger("AnimId", 0);
        animator.Play("Bosta");
        touched = false;
        animCounter = 0;
        ball.DOKill(false);
        ball.position = animDataList.moveFigureList["0"].BallPosition;//new Vector3(1.21f, -2.04f, -0.26f);
        touchManager.refreshPowerBar();
    }

    IEnumerator Counter() 
    {
        for (int i = 2; i > 0; i--) 
        {
            yield return new WaitForSeconds(1f);
            startCounter.text = i.ToString();           
        }
        yield return new WaitForSeconds(1f);
        failed = false;
        startCounter.text = "GO";
        // Build Apk purposes
        
        Play(0); // input doesn't matter
        // Build Apk purposes //
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

    public void AnimTestToggle() 
    {
        animTestOn = !animTestOn;
        if (animTestOn)
        {
            testText.text = "On";
        }
        else 
        {
            testText.text = "Off";
        }
    }
    public void EaseTestToggle()
    {
        easeCounter++;
        selectedEase = easeList[easeCounter % easeList.Count];
        easeText.text = selectedEase;
    }

    void SetNextAnim(int currentAnimId) 
    {
        prevAnimId = currAnimId;
        currAnimId = currentAnimId;

        headRotCOntroller();

        animator.SetInteger("AnimId", currAnimId);

        helper.transform.position = new Vector3(animDataList.moveFigureList[currAnimId.ToString()].BallPosition.x,
            animDataList.moveFigureList[currAnimId.ToString()].BallPosition.y + helperHeightOffset,
            animDataList.moveFigureList[currAnimId.ToString()].BallPosition.z);
    }

    void PlaySpecialMoveAnim() 
    {
        
    }

}
