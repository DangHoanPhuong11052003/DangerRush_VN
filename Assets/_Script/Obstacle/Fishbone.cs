using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishbone : MonoBehaviour
{
    private Vector3 pos;
    private void OnEnable()
    {
        pos= transform.position;
    }
    private void OnDisable()
    {
        transform.position = pos;   
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
