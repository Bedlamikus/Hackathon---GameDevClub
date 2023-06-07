using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AdReward : MonoBehaviour
{
    [SerializeField] private int timer;
    [SerializeField] private TMP_Text timerText;

    private bool stop = false;

    private Coroutine rewardCoroutine;

    private IEnumerator RewardCoroutine()
    {
        int needTime = this.timer;
        for (int i = needTime; i >=0; i--)
        {
            timerText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        stop = true;
    }

    private void Update()
    {
        if (stop) 
        { 
            StopCoroutine(rewardCoroutine); 
            stop = false;
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        GlobalEvents.EvReward.Invoke();
        rewardCoroutine = StartCoroutine(RewardCoroutine());
    }

    private void OnDisable()
    {
        GlobalEvents.EvRewarded.Invoke();
        //FindObjectOfType<Station>().OnRewarded();
        FindObjectOfType<Train>().ResetPosition(0);
    }
}
