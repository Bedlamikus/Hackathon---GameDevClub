using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AdRewardLevelRestart : MonoBehaviour
{
    [SerializeField] private int timer;
    [SerializeField] private TMP_Text timerText;

    private void Start()
    {
        GlobalEvents.EvRewardLevelRestart.Invoke();
        StartCoroutine(RewardCoroutine());
    }

    private IEnumerator RewardCoroutine()
    {
        int needTime = timer;
        for (int i = needTime; i >=0; i--)
        {
            timerText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        GlobalEvents.EvRewardedLevelRestart.Invoke();
        Destroy(gameObject);
    }
}
