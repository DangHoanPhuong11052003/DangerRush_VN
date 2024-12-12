using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private List<Transform> lst_MapDeActive=new List<Transform>();
    [SerializeField] private List<Transform> lst_MapActive=new List<Transform>();
    [SerializeField] private Transform playerTranform;
    [SerializeField] private float distanceBetweenMap=126;
    [SerializeField] private float timeToCurverMap;
    [SerializeField] private float timer=0;
    [SerializeField] private WorldCurver WorldCurver;
    [SerializeField] private float maxCurver = 0.05f;
    [SerializeField] private Transform posCharModel;

    private bool isCurverUp = true;
    private float currentCurver;

    
    private void Start()
    {
        timer = timeToCurverMap;
        currentCurver = -maxCurver;
    }

    private void FixedUpdate()
    {
        timer-= Time.deltaTime;

        if(timer < 0)
        {
            timer = timeToCurverMap;

            currentCurver = isCurverUp ? currentCurver + maxCurver/100 : currentCurver - maxCurver / 100;

            posCharModel.position = new Vector3(posCharModel.position.x, isCurverUp ? posCharModel.position.y - 0.5f / 200f : posCharModel.position.y + 0.5f / 200f, posCharModel.position.z);

            WorldCurver.SetCurver(currentCurver);

            if(currentCurver > maxCurver || currentCurver<-maxCurver)
            {
                isCurverUp = !isCurverUp;
            }

        }

        if (playerTranform.position.z - lst_MapActive.First().position.z> distanceBetweenMap)
        { 
            Transform nextMap = lst_MapDeActive[UnityEngine.Random.Range(0, lst_MapDeActive.Count)];
            lst_MapActive.First().gameObject.SetActive(false);
            nextMap.transform.position = lst_MapActive.Last().position + new Vector3(0, 0, distanceBetweenMap);
            nextMap.gameObject.SetActive(true);

            lst_MapActive.Add(nextMap);
            lst_MapDeActive.Add(lst_MapActive.First());

            lst_MapActive.RemoveAt(0);
            lst_MapDeActive.Remove(nextMap);

        }
    }
}
