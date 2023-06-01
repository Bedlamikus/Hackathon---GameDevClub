using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPositionToWorldObject : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private TMP_Text text;
    public void Init(string text, Transform target, bool moveUp, bool splash)
    {
        rectTransform.position = Camera.main.WorldToScreenPoint(target.position);
        this.text.text = text;
        if (moveUp)
        {
            StartCoroutine(MoveUp());
        }
    }

    IEnumerator MoveUp()
    {
        float timer = 1f;
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            var pos = rectTransform.position;
            rectTransform.position = new Vector3(pos.x, pos.y + 1,0);
            yield return null;
        }
    }
}
