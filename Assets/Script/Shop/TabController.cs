using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabController : MonoBehaviour
{
    [SerializeField] private List<GameObject> tabs = new List<GameObject>();
    private int currentTab = 0;

    private void OnEnable()
    {
        for (int i = 0; i < tabs.Count; i++)
        {
            tabs[i].SetActive(false);
        }
        currentTab = 1;
        tabs[currentTab].SetActive(true);
    }

    public void TurnOnTab(int tab)
    {
        if(currentTab == tab)
        {
            return;
        }

        for (int i = 0; i < tabs.Count; i++)
        {
            tabs[i].SetActive(false);
        }
        currentTab = tab;
        tabs[tab].SetActive(true);
    }
}
