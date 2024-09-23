using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ReviewCharacter : MonoBehaviour
{
    private int currentIdModel = 0;
    private GameObject modelReview;
    [SerializeField] private AnimatorController animatorController;
    // Start is called before the first frame update
    private void OnEnable()
    {
        int currentIdData = LocalData.instance.GetCurrentChar();

        if (currentIdModel!= currentIdData)
        {
            if(modelReview!= null)
            {
                Destroy(modelReview);
            }
            modelReview=Instantiate(CharacterManager.instance.GetPrefabCharacterById(currentIdData),transform);
            currentIdModel=currentIdData;
            modelReview.GetComponent<Animator>().runtimeAnimatorController = animatorController;

            Animator animator = modelReview.GetComponent<Animator>();
            animator.SetInteger("RandomIdle", UnityEngine.Random.Range(0, 5));
        }
        else
        {
            if (modelReview == null)
            {
                modelReview = Instantiate(CharacterManager.instance.GetPrefabCharacterById(currentIdData), transform);
                modelReview.GetComponent<Animator>().runtimeAnimatorController = animatorController;
            }

            Animator animator= modelReview.GetComponent<Animator>();
            animator.SetInteger("RandomIdle", UnityEngine.Random.Range(0, 5));
        }

    }
}
