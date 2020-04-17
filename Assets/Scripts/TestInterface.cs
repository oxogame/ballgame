using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Assets.Scripts.Enum;
public class TestInterface : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    TransitionListener transitionListener;
    [SerializeField]
    AnimationManager animationManager;

    [SerializeField]
    string currentTestMode = "NoTest";
    public List<Actions> tempAnimationList = new List<Actions>();


    bool devAnimTestOn, testing, animTestOn;
    
    
    void Start()
    {
        devAnimTestOn = animationManager.devAnimTestOn;
        testing = transitionListener.testing;
        animTestOn = transitionListener.animTestOn;
    }

    [Button]
    public void DevAnimTest()
    {
        animationManager.devAnimTestOn = true;
        transitionListener.testing = false;
        transitionListener.animTestOn = false;
        currentTestMode = "DevAnimTest is ON ";
    }
    [Button]
    public void PushAnimList()
    {
        animationManager.animationList = new List<Actions>();
        foreach (Actions action in tempAnimationList)
        {
            animationManager.animationList.Add(action);
        }

    }

    [Button]
    public void PlayAnimSet()
    {
        animationManager.devAnimTestOn = false;
        transitionListener.testing = false;
        transitionListener.animTestOn = true;
        currentTestMode = "PlayAnimSet is ON ";

        animationManager.GenerateAnimationSerieDirected();
    }
    [Button]
    public void TimeTest()
    {
        animationManager.devAnimTestOn = false;
        transitionListener.testing = true;
        transitionListener.animTestOn = false;
        currentTestMode = "TimeTest is ON ";
    }
    [Button]
    public void PlayGame()
    {
        animationManager.devAnimTestOn = false;
        transitionListener.testing = false;
        transitionListener.animTestOn = false;
        currentTestMode = "PlayGame is ON ";
    }

}
