using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AchievementsNotification : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private Animator animator;
    [SerializeField] private AchivementData achivementData;

    public void ShowAchievementsNotification(string idAchievement)
    {
        Achievement achievement=achivementData.achievementsData.Find(x => x.id == idAchievement);

        this.title.text = achievement.title;

        animator.SetTrigger("isActive");
        float timeAnimation = animator.runtimeAnimatorController.animationClips[0].length;

        StartCoroutine(DeActiveAfterAnimation(timeAnimation));
    }

    private IEnumerator DeActiveAfterAnimation(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
