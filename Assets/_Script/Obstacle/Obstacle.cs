using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float timeDeActive = 0;
    [SerializeField] private GameObject Parent;
    [SerializeField] private AudioClip soundDead;
    [SerializeField] private List<string> CollisionImpact=new List<string>();

    private Animation animation;

    private void Awake()
    {
        animation = GetComponent<Animation>();
        animation.Stop();
    }

    private void OnEnable()
    {
        ResetAnimation();
        gameObject.GetComponent<MeshCollider>().enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (CollisionImpact.Contains(collision.gameObject.tag))
        {
            animation.Play();
            gameObject.GetComponent<MeshCollider>().enabled = false;

            AudioManager.instance.PlaySoundEffect(soundDead);

            StartCoroutine(DeActive());
        }
    }

    private IEnumerator DeActive()
    {
        yield return new WaitForSeconds(timeDeActive);

        if(Parent != null)
        {
            Parent.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void ResetAnimation()
    {
        AnimationState animState = animation[animation.clip.name];
        animState.time = 0f;
        animation.Sample();
        animation.Stop();
    }
}
