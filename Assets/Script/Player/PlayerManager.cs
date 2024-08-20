using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.AudioSettings;

public class PlayerManager: MonoBehaviour
{
    [SerializeField] float minSpeed = 2;
    [SerializeField] float maxSpeed = 2;
    [SerializeField]  float maxX = 6;
    [SerializeField] private float maxY = 0.77f;
    [SerializeField] int speedSwap = 2;
    [SerializeField] float speedJump = 2;
    [SerializeField] private float timeDash = 1f;
    [SerializeField] Vector3 VectorNormal;
    [SerializeField] Vector3 VectorDash;
    [SerializeField] Vector3 SizeNormal;
    [SerializeField] Vector3 SizeDash;
    [SerializeField] GameObject Character;
    [SerializeField] GameObject ColliderCharacter;
    [SerializeField] public int quantityLife = 3;

    private Animator animator;
    public float currentSpeed = 0;
    private float currentDisJump = 0;

    private bool isMobile = false;

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private float swipeThreshold = 50f;

    private float disSwap = 0;

    bool isJump = false;
    public bool canMove = false;

    private BoxCollider coll;

    private bool isDash=false;

    private int coin=0;
    public bool isDoubleCoin=false;

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
        coll= ColliderCharacter.GetComponentInChildren<BoxCollider>();
        animator=Character.GetComponentInChildren<Animator>();


        currentSpeed = maxSpeed;
        animator.SetTrigger("Moving");
        StartCoroutine(ReadyStart());
    }

    private void Update()
    {
        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + 1), currentSpeed * Time.deltaTime);
            if (transform.position.x != disSwap)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(disSwap, transform.position.y, transform.position.z), speedSwap * Time.deltaTime);
            }


            if (isJump && isDash)
            {
                ColliderCharacter.transform.position = Vector3.MoveTowards(ColliderCharacter.transform.position, new Vector3(ColliderCharacter.transform.position.x, currentDisJump*2, ColliderCharacter.transform.position.z), speedJump * 2 * Time.deltaTime);
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, currentDisJump, transform.position.z), speedJump*2 * Time.deltaTime);
            }
            else if (isJump && ColliderCharacter.transform.position.y != currentDisJump)
            {
                ColliderCharacter.transform.position = Vector3.MoveTowards(ColliderCharacter.transform.position, new Vector3(ColliderCharacter.transform.position.x, currentDisJump * 2, ColliderCharacter.transform.position.z), speedJump * Time.deltaTime);
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, currentDisJump, transform.position.z), speedJump * Time.deltaTime);
                if (ColliderCharacter.transform.position.y == maxY*2)
                {
                    currentDisJump = 0;
                }
            }


            //MoveToLaneMobile();
            MoveToPlaneWindow();

            if (currentSpeed < maxSpeed)
            {
                currentSpeed += 0.01f;
            }
        }
    }

    public void CollisionEnter(Collision collider)
    {
        if (collider.gameObject.CompareTag("Ground")&& isJump)
        {
            isJump = false;
            animator.SetBool("Jumping", false);
            if (isDash)
            {
                coll.center = VectorDash;
                coll.size = SizeDash;
                animator.SetBool("Sliding", true);
                StartCoroutine(StopDash(timeDash));
            }
        }
        else if (collider.gameObject.CompareTag("Obstacle"))
        {
            --quantityLife;
            if (quantityLife <= 0)
            {
                canMove = false;
                animator.SetBool("Dead", true);
                animator.SetTrigger("Hit");
                animator.SetBool("Sliding", false);
                animator.SetBool("Jumping", false);

                if (coin > 0)
                {
                    GameData gameData = LocalData.instance.GetGameData();
                    gameData.coin += coin;
                    LocalData.instance.SetData(gameData);
                }
                return;
            }
            StartCoroutine(Stun());
            animator.SetTrigger("Hit");
            animator.SetBool("Sliding", false);
            animator.SetBool("Jumping", false);
        }
        else if (collider.gameObject.CompareTag("Coin"))
        {
            coin+=isDoubleCoin ?2:1;
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        canJump = true;
    //        if (isDash)
    //        {
    //            coll.center = VectorDash;
    //            coll.height = heightDash;
    //            StartCoroutine(StopDash(0.8f));
    //        }
    //    }
    //}

    private IEnumerator ReadyStart()
    {
        yield return new WaitForSeconds(3f);
        canMove = true;
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
        if (isJump)
        {
            isDash = true;
            currentDisJump = 0;
        }
        else if(!isDash)
        {
            isDash = true;
            coll.center =VectorDash;
            coll.size = SizeDash;
            animator.SetBool("Sliding", true);
            StartCoroutine(StopDash(timeDash));
        }
    }

    private void MoveUp()
    {
        if(!isJump)
        {
            animator.SetBool("Jumping",true);
            currentDisJump = maxY;
            isJump = true;
            if (isDash)
            {
                StartCoroutine(StopDash(0));
            }
        }
    }

    private void MoveLeft()
    {
        if (disSwap > -maxX)
        {
            disSwap -= maxX;
        }

    }

    private void MoveRight()
    {
        if (disSwap < maxX)
        {
            disSwap += maxX;
        }
    }

    private IEnumerator StopDash(float time)
    {
        if(time>0)
        {
            yield return new WaitForSeconds(time);
        }
        coll.center = VectorNormal;
        coll.size=SizeNormal;
        isDash=false;
        animator.SetBool("Sliding", false);
    }

    private IEnumerator Stun()
    {
        currentSpeed = minSpeed;
        canMove = false;
        yield return new WaitForSeconds(0.6f);
        canMove = true;
    }
}
