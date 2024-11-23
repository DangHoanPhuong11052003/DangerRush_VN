using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StackSlider : MonoBehaviour
{
    [System.Serializable]
    private class StackOfSlider
    {
        public Image image;
        public Color color;
    }

    [SerializeField] List<StackOfSlider> StacksLst=new List<StackOfSlider>();
    [SerializeField] TextMeshProUGUI txtInfor;

    public void SetStacksActiveAndTxtInfor(int quantityStacksActive,string infor)
    {
        int number=quantityStacksActive;
        foreach (var item in StacksLst)
        {
            --number;
            if(number >= 0)
            {
                if(number == 0)
                {
                    txtInfor.text = infor;
                    txtInfor.transform.SetParent(item.image.transform);
                    txtInfor.transform.position=item.image.transform.position;
                }
                item.image.color = item.color;
            }
            else
            {
                item.image.color = Color.white;
            }
        }
    }

}
