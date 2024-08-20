using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCharacter : MonoBehaviour
{
    [SerializeField] PlayerManager playerController;

    private void OnCollisionEnter(Collision collision)
    {
        playerController.CollisionEnter(collision);
    }
}
