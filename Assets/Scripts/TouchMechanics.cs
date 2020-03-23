using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMechanics : MonoBehaviour
{
    SlideBarMechanic slideBarMechanic;
    bool changed = false;
    void Start()
    {
        slideBarMechanic = GameObject.Find("SlideBar").GetComponent<SlideBarMechanic>();
    }


    void Update()
    {
        TouchCheck();
    }

    void TouchCheck() 
    {
        if (Input.touchCount > 0)
        {
            if (!changed)
            {
                changed = true;
                slideBarMechanic.Touched();
            }
        }
        else if (Input.touchCount == 0) 
        {
            changed = false;
        }
    }

}
