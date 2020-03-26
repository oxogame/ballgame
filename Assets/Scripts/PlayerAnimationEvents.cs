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

}
