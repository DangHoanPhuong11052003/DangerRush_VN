using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI fishbone;
    [SerializeField] private TextMeshProUGUI meter;
    [SerializeField] private List<GameObject> lst_Heart;
    [SerializeField] private PlayerManager playerManager;

    private int currentHeart=0;

    private void Start()
    {
        currentHeart = playerManager.quantityLife;
    }

    private void Update()
    {
        score.text = "Score: "+Mathf.FloorToInt(GameManager.instance.score);
        fishbone.text= Mathf.FloorToInt(GameManager.instance.coin).ToString();
        meter.text= "Meter: "+Mathf.FloorToInt(GameManager.instance.meter);

        if (currentHeart > playerManager.quantityLife)
        {
            currentHeart=playerManager.quantityLife;
            for (int i = 0; i < 3 - currentHeart; i++)
            {
                lst_Heart[i].SetActive(false);
            }
        }
    }
}
