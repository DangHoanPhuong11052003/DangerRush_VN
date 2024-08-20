using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartEffect : MonoBehaviour
{
    [SerializeField] PlayerManager PlayerManager;

    private void OnEnable()
    {
        if (PlayerManager.quantityLife < 3)
        {
            PlayerManager.quantityLife++;
        }

        StartCoroutine(DeActive());
    }

    private IEnumerator DeActive()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
