using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AdRewardCoinMultiply : MonoBehaviour
{
    [SerializeField] private int timer;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private int multiply;

    public int coins = 0;

    private void Start()
    {
        GlobalEvents.EvRewardMuliplyCoin.Invoke();
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
        GlobalEvents.ApplyGolds.Invoke(coins * multiply);
        Destroy(gameObject);
    }
}
