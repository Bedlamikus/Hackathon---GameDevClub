using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScrollLevels : MonoBehaviour
{
    [SerializeField] private RectTransform targetScreen;
    [SerializeField] private GameObject content;
    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private int levels;
    [SerializeField] private int animationSpeed;
    [SerializeField] private GameObject StartButton;

    private int targetLevel;
    private int lastOpenedLevel;
    
    private bool mouseDown;

    private void Start()
    {
        mouseDown = false;
        targetLevel = 0;
        lastOpenedLevel = 0;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDown = true;
            print($"targetlevel = {targetLevel}, lastopenedlevel = {lastOpenedLevel}");
            StartButton.SetActive(true);
            if (targetLevel > lastOpenedLevel)
                StartButton.SetActive(false);
            return;
        }
        if (Input.GetMouseButtonUp(0))
        {
            mouseDown = false;
            targetLevel = GetNewTargetLevel();
            StartButton.SetActive(true);
            if (targetLevel > lastOpenedLevel)
                StartButton.SetActive(false);
            return;
        }
        if (mouseDown)
        {
            return;
        }
        scrollbar.value = ScrollBarValue();
    }

    private float ScrollBarValue()
    {
        var targetValue = targetLevel * 1f / levels;
        return Mathf.Lerp(scrollbar.value, targetValue, animationSpeed * Time.deltaTime);
    }
    private int GetNewTargetLevel()
    {
        int target = (int)Mathf.Round(scrollbar.value / (1f / levels));
        return target;
    }
    public void StartLevel()
    {
        SceneManager.LoadScene(targetLevel + 1);
    }
}
