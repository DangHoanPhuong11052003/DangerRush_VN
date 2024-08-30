using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private GameObject Character;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator=Character.GetComponent<Animator>();
        animator.SetInteger("RandomIdle",Random.Range(0,5));
    }
}
