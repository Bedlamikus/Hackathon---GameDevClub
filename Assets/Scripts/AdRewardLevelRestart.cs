using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using YG;

public class AdRewardLevelRestart : MonoBehaviour
{
    [SerializeField] private int timer;
    [SerializeField] private TMP_Text timerText;

    private void Start()
    {
        GlobalEvents.EvRewardLevelRestart.Invoke();
        StartCoroutine(RewardCoroutine());
        YandexGame.RewVideoShow(0);
        YandexGame.CloseVideoEvent += RestartRewarded;
    }

    private IEnumerator RewardCoroutine()
    {
        int needTime = timer;
        for (int i = needTime; i >=0; i--)
        {
            timerText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        Destroy(gameObject);
    }
    private void RestartRewarded()
    {
        GlobalEvents.EvRewardedLevelRestart.Invoke();
        MetricEvents.Instance.RewardBeforePlayerDie();
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        YandexGame.CloseVideoEvent -= RestartRewarded;
    }
}
