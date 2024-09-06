using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsCharUI : MonoBehaviour
{
    private CharacterItem characterItem;

    public void SetData(CharacterItem characterItem)
    {
        this.characterItem = characterItem;
        GetComponent<Image>().sprite = characterItem.icon;
    }


}
