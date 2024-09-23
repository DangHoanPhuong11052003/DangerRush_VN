using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessoriesManager : MonoBehaviour
{
    [Serializable]
    private class PrefabAccessories
    {
        public int id;
        public GameObject prefab;
    }

    [SerializeField] private List<PrefabAccessories> lst_accessories;

    private bool canChangeAccessories = true;

    // Start is called before the first frame update
    void OnEnable()
    {
        if (canChangeAccessories)
        {
            foreach (var item in lst_accessories)
            {
                item.prefab.SetActive(false);
            }

            int id = LocalData.instance.GetCurrentIdAccessories();

            if (id != -1)
            {
                GameObject accessories = lst_accessories.Find(item => item.id == id).prefab;

                if (accessories != null)
                {
                    accessories.SetActive(true);
                }
            }
        }

    }

    public void ActiveAccessoriesById(int id,bool canChangeAccessories)
    {
        this.canChangeAccessories = canChangeAccessories;

        GameObject accessories = lst_accessories.Find(item => item.id == id).prefab;

        if (accessories != null)
        {
            foreach(var item in lst_accessories)
            {
                item.prefab.SetActive(false);
            }

            accessories.SetActive(true);
        }
    }

}
