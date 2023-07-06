using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class LoadScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    private bool ImDie = false;
    private void Awake()
    {
        print("LoadScreen: подписался на SettingsLoaded");
        GlobalEvents.SettingsLoaded.AddListener(Die);
    }

    private void Start()
    {
        if (ImDie) return;
        YandexGame.Instance._LoadProgress();
    }

    private void Die(string _)
    {
        ImDie = true;
        StartCoroutine(Transparent());
    }

    private IEnumerator Transparent()
    {
        var backGround = GetComponent<Image>();
        var textColor = text.color;
        float timer = 1f;
        while (timer >0)
        {
            timer -= Time.deltaTime;
            if (timer < 0) timer = 0;
            backGround.color = new Color(0, 0, 0, timer);
            textColor.a = timer;
            text.color = textColor;
            yield return null;
        }
        Destroy(gameObject);
    }

}
