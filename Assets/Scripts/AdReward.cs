using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AdReward : MonoBehaviour
{
    [SerializeField] private int timer;
    [SerializeField] private TMP_Text timerText;

    private void Start()
    {
        GlobalEvents.EvReward.Invoke();
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
        GlobalEvents.EvRewarded.Invoke();
        Destroy(gameObject);
    }
}
