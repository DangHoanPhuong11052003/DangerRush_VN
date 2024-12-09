using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMap : MonoBehaviour
{
    [SerializeField] private List<GameObject> lst_DogObstacle=new List<GameObject>();
    [SerializeField] private float timeActiveDogObstacle;
    [SerializeField] private float timeToActiveEvent;

    private int pos;
    private float timer;

    private void Start()
    {
        timer = timeActiveDogObstacle;
    }

    private void Update()
    {
        if(timeToActiveEvent > 0)
            timeToActiveEvent-= Time.deltaTime;
        else
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                lst_DogObstacle[pos].SetActive(false);
                lst_DogObstacle[pos].SetActive(true);
                pos++;
                if (pos > lst_DogObstacle.Count - 1)
                    pos = 0;
                timer = timeActiveDogObstacle;
            }
        }
    }
}
