using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObstacleDog : MonoBehaviour
{
    [SerializeField] GameObject DogObstacle;
    [SerializeField] float distanceDog=50f;

    private void OnEnable()
    {
        DogObstacle.transform.position=new Vector3(transform.position.x, transform.position.y,transform.position.z+distanceDog);
        DogObstacle.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DogObstacle.SetActive(true);
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            DogObstacle.SetActive(false);
        }
    }
}
