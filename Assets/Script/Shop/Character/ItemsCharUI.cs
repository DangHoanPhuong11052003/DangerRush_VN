using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsCharUI : MonoBehaviour
{
    [SerializeField] Image iconChar;
    private CharacterItem characterItem;

    private CharStoreManager characterStoreManager;

    public void SetData(CharacterItem characterItem, CharStoreManager characterStoreManager)
    {
        this.characterItem = characterItem;
        iconChar.sprite = characterItem.icon;
        this.characterStoreManager = characterStoreManager;
    }

    public void onclickitem()
    {
        characterStoreManager.SelectedItem(characterItem.id);
    }
}
