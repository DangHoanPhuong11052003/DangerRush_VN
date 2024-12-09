using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObstacleDog : MonoBehaviour
{
    [SerializeField] private GameObject DogObstacle;
    [SerializeField] private GameObject LineObject;
    [SerializeField] private Animation AnimationLine;

    [SerializeField] private float timeToActiveWarning;
    [SerializeField] private float timeToActiveDog;
    [SerializeField] private Transform player;

    private bool isFollowChar=true;

    private void Update()
    {
        if (isFollowChar)
            transform.position = new Vector3(player.position.x,transform.position.y, player.position.z);   
    }
    private void OnEnable()
    {
        isFollowChar = true;
        DogObstacle.GetComponent<DogObstacle>().ActiveDogGrowAudio();
        DogObstacle.SetActive(false);
        LineObject.SetActive(true);
        AnimationLine[AnimationLine.clip.name].time = 0;
        AnimationLine[AnimationLine.clip.name].speed = 1f;
        AnimationLine.Stop();

        StartCoroutine(ActiveAnimationLineWarning(timeToActiveWarning));
        StartCoroutine(ActiveDog(timeToActiveDog));
    }
    private void OnDisable()
    {
        DogObstacle.SetActive(false);
    }
    private IEnumerator ActiveAnimationLineWarning(float time)
    {
        yield return new WaitForSeconds(time);
        isFollowChar=false;
        AnimationLine.Play();
    }
    private IEnumerator ActiveDog(float time)
    {
        yield return new WaitForSeconds(time);
        DogObstacle.transform.position = new Vector3(transform.position.x, player.position.y, player.position.z-2);
        LineObject.SetActive(false);
        DogObstacle.SetActive(true);
    }
}
