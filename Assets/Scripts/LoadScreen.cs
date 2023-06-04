using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private void Awake()
    {
        GlobalEvents.DefaultSettingsLoaded.AddListener(Die);
    }

    private void Die(ExcelSettings _)
    {
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
