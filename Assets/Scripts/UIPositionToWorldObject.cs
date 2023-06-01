using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPositionToWorldObject : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    public void Init(Transform target)
    {
        rectTransform.position = Camera.main.WorldToScreenPoint(target.position);
    }
}
