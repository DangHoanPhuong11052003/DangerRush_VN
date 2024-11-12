using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private List<Transform> lst_MapDeActive=new List<Transform>();
    [SerializeField] private List<Transform> lst_MapActive=new List<Transform>();
    [SerializeField] private Transform playerTranform;
    [SerializeField] private float distanceBetweenMap=126;

    private void FixedUpdate()
    {
        if (playerTranform.position.z - lst_MapActive.First().position.z> distanceBetweenMap)
        {
            Transform nextMap = lst_MapDeActive[Random.Range(0, lst_MapDeActive.Count)];
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
