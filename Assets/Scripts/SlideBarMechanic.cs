using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideBarMechanic : MonoBehaviour
{
    [SerializeField]
    public float speed = 1f;

    [SerializeField]
    GameObject SlideBarGameObj;
    Slider SlideBar;

    float currentValue = 0f;

    public bool moveSlideBar = true;
    void Start()
    {
        SlideBar = SlideBarGameObj.GetComponent<Slider>();
    }

    void Update()
    {
        if (moveSlideBar) 
        {
            SlideBar.value = Mathf.PingPong(Time.time * speed, 100);
        }
    }

    public void Touched() 
    {
        moveSlideBar = !moveSlideBar;
        currentValue = SlideBar.value;
        print("CURRENT VALUE : " + currentValue);
    }

    public void tamam_denemem()
    {
        Debug.Log("denemicem");
    }
}
