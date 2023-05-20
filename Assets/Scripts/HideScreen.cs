using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideScreen : MonoBehaviour
{
    [SerializeField] private GameObject screen;

    public void Hide()
    {
        screen.SetActive(false);
    }
}
