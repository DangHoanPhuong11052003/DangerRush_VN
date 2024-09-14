using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> CharactersModel=new List<GameObject>();

    private int currentIdModel = 0;

    private Animator animator;
    // Start is called before the first frame update
    private void OnEnable()
    {
        int currentIdData = LocalData.instance.GetCurrentChar();

        if (currentIdModel!= currentIdData)
        {
            CharactersModel[currentIdModel].SetActive(false);
            currentIdModel = CharactersModel.FindIndex(item => Convert.ToInt32(item.name) == currentIdData);
            CharactersModel[currentIdModel].gameObject.SetActive(true);

            animator = CharactersModel[currentIdModel].GetComponent<Animator>();
            animator.SetInteger("RandomIdle", UnityEngine.Random.Range(0, 5));
        }
        else
        {
            CharactersModel[currentIdModel].gameObject.SetActive(true);
            animator = CharactersModel[currentIdModel].GetComponent<Animator>();
            animator.SetInteger("RandomIdle", UnityEngine.Random.Range(0, 5));
        }

    }
}
