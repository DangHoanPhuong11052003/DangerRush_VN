using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotEnoughFishboneUI : MonoBehaviour
{
    [SerializeField] private Canvas Canvas;

    private void OnEnable()
    {
        Canvas.worldCamera=Camera.main;

    }
}
