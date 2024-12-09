using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    [SerializeField] private List<GameObject> tabs = new List<GameObject>();
    [SerializeField] private List<Button> buttons = new List<Button>();
    private int currentTab = 0;

    private void OnEnable()
    {
        for (int i = 0; i < tabs.Count; i++)
        {
            tabs[i].SetActive(false);
            buttons[i].interactable = true;
        }
        currentTab = 0;
        buttons[0].interactable = false;
        tabs[currentTab].SetActive(true);
    }

    public void TurnOnTab(int tab)
    {
        if(currentTab == tab)
        {
            return;
        }

        tabs[currentTab].SetActive(false);
        buttons[currentTab].interactable = true;

        currentTab = tab;
        buttons[currentTab].interactable = false;
        tabs[tab].SetActive(true);
    }
}
