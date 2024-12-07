
using UnityEngine;

public class ReviewCharacter : MonoBehaviour
{
    private int currentIdModel = 0;
    private GameObject modelReview;
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

            Animator animator = modelReview.GetComponent<Animator>();
            animator.SetInteger("RandomIdle", UnityEngine.Random.Range(0, 5));
        }
        else
        {
            if (modelReview == null)
            {
                modelReview = Instantiate(CharacterManager.instance.GetPrefabCharacterById(currentIdData), transform);
            }

            Animator animator= modelReview.GetComponent<Animator>();
            animator.SetInteger("RandomIdle", UnityEngine.Random.Range(0, 5));
        }

    }
}
