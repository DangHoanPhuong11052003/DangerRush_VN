using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using static UnityEngine.AudioSettings;


public class PlayerManager: MonoBehaviour
{
    [Header("===Data===")]

    [SerializeField] float minSpeed = 2;
    [SerializeField] float maxSpeed = 2;
    [SerializeField]  float maxX = 6;
    [SerializeField] private float maxY = 0.77f;
    [SerializeField] int speedSwap = 2;
    [SerializeField] float speedJump = 2;
    [SerializeField] private float timeDash = 1f;

    [SerializeField] private Vector3 VectorNormal;
    [SerializeField] private Vector3 VectorDash;
    [SerializeField] private Vector3 SizeNormal;
    [SerializeField] private Vector3 SizeDash;

    [SerializeField] private int quantityLife = 3;


    [Header("======")]

    [SerializeField] private GameObject Character;
    [SerializeField] private GameObject ColliderCharacter;
    [SerializeField] private GameObject BuffEffect;
    [SerializeField] GameObject LoseEffect; 
    [SerializeField] private AnimatorController animatiorPlayer;

    [SerializeField] private UIGamePlayManager canvasManager;

    private CharacterAudioSoucre characterSounds;

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
    private bool isDash = false;
    private bool isInvincible=false;

    private BoxCollider coll;

    private Renderer[] renderers;


    [HideInInspector] public bool isDoubleCoin=false;
    [HideInInspector] public float score = 0;
    [HideInInspector] public bool isDoubleScore = false;
    [HideInInspector] public float meter = 0;
    [HideInInspector] public int coin = 0;

    public int QuantityLife
    {
        get => quantityLife;
        set {
            if (quantityLife != value)
            {
                quantityLife = value;
                EventManager.NotificationToActions(KeysEvent.HeathUpdate.ToString(), quantityLife);
            }
        }
    }

    private void Awake()
    {
        if (GameManager.instance == null)
        {
            SceneManager.LoadScene("Main");
        }
    }

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
        isMobile = false;


        //get model character
        GameObject prefabCharacter = CharacterManager.instance.GetPrefabCharacterById(LocalData.instance.GetCurrentChar());

        Instantiate(prefabCharacter, Character.transform);

        //get sounds character
        characterSounds=AudioManager.instance.GetCharacterSound(LocalData.instance.GetCurrentChar());

        //get collider character
        coll = ColliderCharacter.GetComponentInChildren<BoxCollider>();
        animator=Character.GetComponentInChildren<Animator>();
        animator.runtimeAnimatorController = animatiorPlayer;

        renderers=Character.GetComponentsInChildren<Renderer>();

        //set speed
        currentSpeed = maxSpeed;
        animator.SetTrigger("Moving");
        StartCoroutine(ReadyStart());
    }

    private void Update()
    {
        IncreaseScoreAndMeter();

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

            if (isMobile)
                MoveToLaneMobile();
            else 
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

                AudioManager.instance.PlaySoundEffect(AudioManager.instance.slide);

                StartCoroutine(StopDash(timeDash));
            }
        }
        else if (collider.gameObject.CompareTag("Obstacle")&& !isInvincible)
        {
            --QuantityLife;

            if (QuantityLife <= 0)
            {
                if (isMobile)
                {
                    canvasManager.OpenOrCloseReviveUI(true);
                }
                else
                {
                    LoseGamePublic();
                }
                return;
            }

            StartCoroutine(Stun());
        }
        else if (collider.gameObject.CompareTag("Coin"))
        {
            AudioManager.instance.PlaySoundEffect(AudioManager.instance.fishBone);

            IncreaseCoin(isDoubleCoin ? 2 : 1);
        }
    }

    private void IncreaseScoreAndMeter()
    {
        score += (isDoubleScore ? 2f * (transform.position.z - meter) : transform.position.z - meter);
        meter = transform.position.z;
    }

    public void IncreaseCoin(int quanity)
    {
        coin += quanity;
    }

    private IEnumerator LoseGame()
    {
        //Play dead sound
        AudioManager.instance.PlaySoundEffect(characterSounds.deadSound);

        canMove = false;
        animator.SetBool("Dead", true);
        animator.SetTrigger("Hit");
        animator.SetBool("Sliding", false);
        animator.SetBool("Jumping", false);

        BuffEffect.SetActive(false);
        StartCoroutine(ActiveLoseEffect());

        if (coin > 0)
        {
            int coinData = LocalData.instance.GetCoin();
            coinData += coin;
            LocalData.instance.SetCoin(coinData);
        }
        yield return new WaitForSeconds(2f);
        canvasManager.OpenLoseUI();
    }
    public void LoseGamePublic()
    {
        StartCoroutine(LoseGame());
    }

    private IEnumerator ReadyStart()
    {
        yield return new WaitForSeconds(3f);
        canMove = true;
    }
    private IEnumerator ActiveLoseEffect()
    {
        yield return new WaitForSeconds(0.6f);
        LoseEffect.SetActive(true);
    }
    public IEnumerator ActiveInvincible(float time)
    {
        float timer = 0f;
        isInvincible = true;
        while (timer < time - 0.1f)
        {
            foreach (var renderer in renderers)
            {
                renderer.enabled = !renderer.enabled;
            }
            yield return new WaitForSeconds(0.1f);
            timer += 0.1f;
        }
        foreach (var renderer in renderers)
        {
            renderer.enabled = true;
        }
        yield return new WaitForSeconds(time);

        isInvincible = false;
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

            AudioManager.instance.PlaySoundEffect(AudioManager.instance.slide);

            StartCoroutine(StopDash(timeDash));
        }
    }

    private void MoveUp()
    {
        if(!isJump)
        {
            animator.SetBool("Jumping",true);
            //play jump sound
            AudioManager.instance.PlaySoundEffect(characterSounds.jumpSound);

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
        //Play hurt sound
        AudioManager.instance.PlaySoundEffect(characterSounds.hurtSound);

        StartCoroutine(ActiveInvincible(1.5f));

        animator.SetTrigger("Hit");
        animator.SetBool("Sliding", false);
        animator.SetBool("Jumping", false);

        currentSpeed = minSpeed;
        canMove = false;
        yield return new WaitForSeconds(0.6f);
        canMove = true;
    }

    public void StunPublic()
    {
        StartCoroutine(Stun());
    }
}
