using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartEffect : PowerEffect
{
    [SerializeField] PlayerManager PlayerManager;

    private void OnEnable()
    {
        ActivePower();
    }

    private IEnumerator DeActive()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

    public override void SetData(PowerData data)
    {
        return;
    }

    public override void ActivePower()
    {
        base.ActivePower();
        if (PlayerManager.QuantityLife < 3)
        {
            PlayerManager.QuantityLife++;
        }

        AudioManager.instance.PlaySoundEffect(AudioManager.instance.invincible);

        StartCoroutine(DeActive());
    }
}
