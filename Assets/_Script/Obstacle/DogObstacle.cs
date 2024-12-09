using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DogObstacle : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private Animator animator;
    [SerializeField] List<string> lst_CollierImpact=new List<string>();
    [SerializeField] private int life = 3;
    [SerializeField] BoxCollider colliderNormal;
    [SerializeField] BoxCollider colliderDead;

    [SerializeField] private List<AudioClip> DogLoops=new List<AudioClip>();
    [SerializeField] private AudioClip DogDead;

    private bool canMove = true;
    private int heath;

    private void OnEnable()
    {
        colliderNormal.enabled = true;
        colliderDead.enabled = false;

        canMove = true;
        animator.SetTrigger("Run");
        heath = life;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (lst_CollierImpact.Contains(collision.gameObject.tag))
        {
            if(heath>0)
                heath--;
            else
            {
                animator.SetTrigger("Death");
                canMove = false;
                AudioManager.instance.PlaySoundEffect(DogDead);
                colliderNormal.enabled = false;
                colliderDead.enabled = true;
            }
                 
        }
    }

    public void ActiveDogGrowAudio()
    {
        AudioManager.instance.PlaySoundEffect(DogLoops[(int)(Random.value * 1.9)]);
    }

    private void Update()
    {
        if(canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + 1), speed * Time.deltaTime);
        }
    }
}
