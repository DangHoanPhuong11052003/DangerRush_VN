using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [Serializable]
    private class PrefabCharacter
    {
        public int id;
        public GameObject prefab;
    }

    public static CharacterManager instance;
    [SerializeField] private List<PrefabCharacter> lst_character = new List<PrefabCharacter>();

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
        return lst_character.Find(item=>item.id==id).prefab;
    }


}
