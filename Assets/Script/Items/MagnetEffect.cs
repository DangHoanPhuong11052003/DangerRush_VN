using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetEffect : MonoBehaviour
{
    [SerializeField] private Transform player;
    private List<Transform> lst_Fishbone=new List<Transform>();
    private float timeActiveBuff=5f;
    private float timer=0;

    private void OnEnable()
    {
        lst_Fishbone.Clear();
        timer = timeActiveBuff;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin")&& timer>0)
        {
            lst_Fishbone.Add(other.transform);
        }
    }

    private void Update()
    {
        MagnetFishbone();
    }

    private void MagnetFishbone()
    {
        timer-=Time.deltaTime;
        for (int i = lst_Fishbone.Count - 1; i >= 0; i--)
        {
            lst_Fishbone[i].position = Vector3.MoveTowards(lst_Fishbone[i].position, player.position, 15 * Time.deltaTime);
            if (lst_Fishbone[i].position == player.position)
            {
                lst_Fishbone[i].gameObject.SetActive(false);
                lst_Fishbone.Remove(lst_Fishbone[i]);
            }
        }

        if (timer <= 0 && lst_Fishbone.Count==0)
        {
            gameObject.SetActive(false);
        }
    }
}