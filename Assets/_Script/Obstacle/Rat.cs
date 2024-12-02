using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Rat : MonoBehaviour
{
    [SerializeField] float speed = 0;
    [SerializeField] float timeDeActive = 0;

    [SerializeField] private AudioClip hit;
    [SerializeField] private AudioClip loop1;
    [SerializeField] private AudioClip loop2;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<string> tagImpact=new List<string>();

    private float maxMoveX=1.6f;
    private float moveX;

    private Animator animator;

    private bool isReset=false;

    private bool isDead=false;

    // Start is called before the first frame update
    void Awake()
    {
        moveX=-transform.position.x;
        animator = GetComponent<Animator>();
        animator.SetFloat("SpeedRatio", 0.58f);
    }

    private void OnEnable()
    {
        if (isReset)
        {
            animator.SetBool("Dead", false);
            GetComponent<BoxCollider>().enabled = true;
            animator.SetFloat("SpeedRatio", 0.58f);
            transform.position = new Vector3(-moveX, transform.position.y, transform.position.z);
            if (transform.position.x == -1.6f)
            {
                transform.rotation = Quaternion.Euler(0,90,0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 270, 0);
            }
            isDead = false;
            isReset = false;
        }
    }

    private void OnDisable()
    {
        isReset = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(moveX, transform.position.y, transform.position.z), speed * Time.deltaTime);
            if (transform.position.x >= maxMoveX || transform.position.x <= -maxMoveX)
            {
                if (Random.value * 10 <= 2)
                {
                    audioSource.PlayOneShot(moveX>0?loop1:loop2);
                }

                moveX = -moveX;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (tagImpact.Contains(collision.gameObject.tag))
        {
            animator.SetBool("Dead", true);

            audioSource.PlayOneShot(hit);

            GetComponent<BoxCollider>().enabled = false;
            isReset = true;
            StartCoroutine(DeActive());
        }
    }

    private IEnumerator DeActive()
    {
        yield return new WaitForSeconds(timeDeActive);
        gameObject.SetActive(false);
    }
}
