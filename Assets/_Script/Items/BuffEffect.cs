using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffEffect : MonoBehaviour
{
    [SerializeField] GameObject DoubleScore;
    [SerializeField] GameObject Magnet;
    [SerializeField] GameObject Heart;
    [SerializeField] GameObject Sardines;

    public void ActiveDoubleScore()
    {
        DoubleScore.SetActive(true);
    }

    public void ActiveMagnet()
    {
        Magnet.SetActive(true);
    }

    public void ActiveHeart()
    {
        Heart.SetActive(true);
    }

    public void ActiveSardines()
    {
        Sardines.SetActive(true);
    }


}
