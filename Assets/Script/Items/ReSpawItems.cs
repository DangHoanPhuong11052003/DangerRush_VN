using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSpawItems : MonoBehaviour
{
    [SerializeField] private List<GameObject> lst_Items= new List<GameObject>();

    private void OnEnable()
    {
        foreach (var item in lst_Items)
        {
            item.SetActive(false);
        }

        int i = Random.Range(0, lst_Items.Count);
        lst_Items[i].SetActive(true);
        Debug.Log(lst_Items[i].active);
    }
}
