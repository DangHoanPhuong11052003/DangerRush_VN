using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float timeDeActive = 0;
    [SerializeField] private GameObject Parent;
    [SerializeField] private AudioClip soundDead;

    private Animation animation;

    private void Awake()
    {
        animation = GetComponent<Animation>();
        animation.Stop();
    }

    private void OnEnable()
    {
        StartCoroutine(ResetAnimation());
        gameObject.GetComponent<MeshCollider>().enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
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

    private IEnumerator ResetAnimation()
    {
        animation[animation.clip.name].time = 0f;
        animation[animation.clip.name].speed = 1f;
        yield return new WaitForEndOfFrame();
        animation.Stop();
    }
}
