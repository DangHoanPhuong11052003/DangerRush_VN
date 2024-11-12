using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleScoreScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            BuffEffect(collision.gameObject);
            gameObject.SetActive(false);
        }
    }

    private void BuffEffect(GameObject player)
    {
        player.GetComponentInParent<BuffEffect>().ActiveDoubleScore();
    }
}
