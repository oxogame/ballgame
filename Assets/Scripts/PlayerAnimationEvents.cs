using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    [SerializeField]
    GameObject TouchManagerObj, AnimationManagerObj;

    TouchManagerGuitarHero2 touchManager;
    AnimationManager animationManager;

    private void Start()
    {
        touchManager = TouchManagerObj.GetComponent<TouchManagerGuitarHero2>();
        animationManager = AnimationManagerObj.GetComponent<AnimationManager>();
    }
    public void playNextAnimation2() 
    {
        print("PLAYING 11: " );
        animationManager.playNextAnimation();
    }

    public void InputStart() 
    {
        print("PLAYING 112: ");
        // Testing -----------------------
        touchManager.red.SetActive(false);
        touchManager.green.SetActive(true);
        if (touchManager.xx.activeSelf) 
        {
            touchManager.xx.SetActive(false);
        }
        // Testing ----------------------- /
        //touchManager.nextTouchCheck = Time.timeSinceLevelLoad + touchManager.currentLevelToleranceAmount;
        animationManager.playNextAnimation();
    }

    public void InputEnd()
    {
        // Testing -----------------------
        touchManager.red.SetActive(true);
        touchManager.green.SetActive(false);
        if (touchManager.tick.activeSelf) 
        {
            touchManager.tick.SetActive(false);
        }
        if (touchManager.tapOnce)
        {
            touchManager.xx.SetActive(true);
        }
        // Testing ----------------------- /
        print("INPUT END");
        
        touchManager.tapOnce = true;
    }
}
