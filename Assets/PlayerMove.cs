using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.AudioSettings;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float speed = 2;

    Rigidbody rb;

    bool isMobile=false;

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private float swipeThreshold = 50f;

    private float plane=0;

    private float forceX = 0;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_ANDROID
        isMobile=true;
#elif UNITY_IOS
        isMobile=true;
#elif UNITY_STANDALONE_WIN
       isMobile=false;
#endif

        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {

        //MoveToLaneMobile();

        MoveToPlaneWindow();
    }

    private void FixedUpdate()
    {
        Vector3 force = new Vector3(forceX, 0, 1f) * speed;
        rb.AddForce(force);
    }

    private void MoveToLaneMobile()
    {
        if (Input.touchCount > 0)
        {
            Touch touch= Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition=touch.position;
                    break;
                case TouchPhase.Ended:
                    endTouchPosition=touch.position;

                    Vector2 swipeDelta=endTouchPosition - startTouchPosition;
                    
                    if(Mathf.Abs(swipeDelta.x) > swipeThreshold|| Mathf.Abs(swipeDelta.y) > swipeThreshold) 
                    {
                        if(Mathf.Abs(swipeDelta.x)> Mathf.Abs(swipeDelta.y))
                        {
                            if (swipeDelta.x > 0)
                            {
                                MoveRight();
                            }
                            else
                            {
                                MoveLeft();
                            }
                        }
                        else
                        {
                            if(swipeDelta.y > 0)
                            {
                                MoveUp();
                            }
                            else
                            {
                                MoveDown();
                            }
                        }
                    }
                    break;
            }
        }
    }

    private void MoveToPlaneWindow()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (transform.position.x==plane)
        {
            if (moveHorizontal > 0 || moveHorizontal < 0)
            {
                if (moveHorizontal > 0)
                {
                    MoveRight();
                }
                else
                {
                    MoveLeft();
                }
            }
            if (moveVertical > 0 || moveVertical < 0)
            {
                if (moveVertical > 0)
                {
                    MoveUp();
                }
                else
                {
                    MoveDown();
                }
            }
        }
    }

    private void MoveDown()
    {
        return;
    }

    private void MoveUp()
    {
        return;
    }

    private void MoveLeft()
    {
        if (plane > -6)
        {
            plane -=6;
            forceX = -6;
            StartCoroutine(StopForce(0.2f));
        }
    }

    private void MoveRight()
    {
        if (plane < 6)
        {
            plane += 6;
            forceX = 6;
            StartCoroutine(StopForce(0.2f));
        }
    }

    private IEnumerator StopForce(float time)
    {
        yield return new WaitForSeconds(time);
        forceX = 0;
        rb.velocity = Vector3.zero;
    }
}
