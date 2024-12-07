using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogObstacle : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private Animator animator;

    private void OnEnable()
    {
        animator.SetTrigger("Run");
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z - 1), speed * Time.deltaTime);
    }
}
