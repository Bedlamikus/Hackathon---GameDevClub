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

    Vector2 mousePosition = Vector2.zero;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDown = true;
            mousePosition = Input.mousePosition;
            ShowButtonOrHide();
            return;
        }
        if (Input.GetMouseButtonUp(0))
        {
            mouseDown = false;
            if (Vector3.Distance(mousePosition, Input.mousePosition) < 2) return;
            targetLevel = GetNewTargetLevel();
            ShowButtonOrHide();
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
    public void NextTargetLevel()
    {
        if (targetLevel < levels)
            targetLevel++;
        ShowButtonOrHide();
    }
    public void PrevoiusTargetLevel()
    {
        if (targetLevel >0)
            targetLevel--;
        ShowButtonOrHide();
    }
    public void ShowButtonOrHide()
    {
        StartButton.SetActive(true);
        if (targetLevel > lastOpenedLevel)
            StartButton.SetActive(false);
    }
}
