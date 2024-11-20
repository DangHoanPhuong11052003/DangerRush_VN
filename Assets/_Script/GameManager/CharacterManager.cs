using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{

    public static CharacterManager instance;
    [SerializeField] private CharacterData lst_characters;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject GetPrefabCharacterById(int id)
    {
        return lst_characters.characters.Find(item=>item.id==id).model;
    }


}
