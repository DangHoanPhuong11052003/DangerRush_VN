using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.AudioSettings;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float speed = 2;
    [SerializeField] private float gravityScale = 1f;
    [SerializeField] private float jumpForce = 10;
    [SerializeField]  int maxX = 6;
    [SerializeField] int speedSwap = 2;

    Rigidbody rb;

    bool isMobile = false;

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private float swipeThreshold = 50f;

    private float plane = 0;

    bool canJump = true;

    Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_ANDROID
        isMobile = true;
#elif UNITY_IOS
        isMobile=true;
#elif UNITY_STANDALONE_WIN
       isMobile=false;
#endif

        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        moveDirection = new Vector3(0, 0, 60) * speed*Time.deltaTime;
        rb.AddForce(moveDirection);

        if (transform.position.x != plane)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(plane, transform.position.y, transform.position.z), speedSwap * Time.deltaTime);
        }

        //MoveToLaneMobile();


        MoveToPlaneWindow();

        if (!canJump)
        {
            rb.AddForce(Physics.gravity * gravityScale, ForceMode.Acceleration);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }

    private void MoveToLaneMobile()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = touch.position;
                    break;
                case TouchPhase.Ended:
                    endTouchPosition = touch.position;

                    Vector2 swipeDelta = endTouchPosition - startTouchPosition;

                    if (Mathf.Abs(swipeDelta.x) > swipeThreshold || Mathf.Abs(swipeDelta.y) > swipeThreshold)
                    {
                        if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
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
                            if (swipeDelta.y > 0)
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
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveUp();
        } else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            MoveDown();
        } else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            MoveLeft();
        } else if (Input.GetKeyDown(KeyCode.D)|| Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
    }

    private void MoveDown()
    {
        return;
    }

    private void MoveUp()
    {
        if(canJump)
        { 
            canJump = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void MoveLeft()
    {
        if (plane > -maxX)
        {
            plane -= maxX;
        }
    }

    private void MoveRight()
    {
        if (plane < maxX)
        {
            plane += maxX;
        }
    }
}
