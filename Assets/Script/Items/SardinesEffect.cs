using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SardinesEffect : PowerEffect
{

    [SerializeField] private PlayerManager playerManager;
    private float timer = 0f;

    public override void SetData(PowerData data)
    {
        level = data.level;

        switch (level)
        {
            case 2: timeBuff += 1; break;
            case 3: timeBuff += 2; break;
            case 4: timeBuff += 3; break;
            case 5: timeBuff += 4; break;
            default: return;
        }
    }

    private void OnEnable()
    {
        playerManager.isDoubleCoin = true;
        timer = timeBuff;

        AudioManager.instance.PlaySoundEffect(AudioManager.instance.premiumCurrency);
    }

    private void Update()
    {
        timer-=Time.deltaTime;

        if(timer <= 0f)
        {
            playerManager.isDoubleCoin = false;
            gameObject.SetActive(false);
        }
    }
}
