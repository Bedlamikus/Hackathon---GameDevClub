using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UILevelListMainMenu : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private AnimationCurve curve;
    [Range(0.01f, 1f)]
    [SerializeField] private float animationTime;
    [SerializeField] private List<UILevel> levels;

    private bool slided = false;
    private int currentLevel = 0;
    public int level => currentLevel;
    private int previousLevel = -1;

    private bool mouseDown;
    private Vector3 mousePosition = Vector3.zero;
    private Vector3 startMousePosition = Vector3.zero;
    private Vector3 mouseDirection = Vector3.zero;
    private Vector3 startCurrentLevelPosition = Vector3.zero;

    private void Start()
    {
        mouseDown = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (slided) return;
            mouseDown = true;
            startMousePosition = Input.mousePosition;
            startCurrentLevelPosition = levels[currentLevel].XYZ();
            return;
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (slided) return;
            if (mouseDown == false) return;
            mouseDown = false;
            if (mouseDirection.x > 0)
            {
                if (currentLevel == levels.Count - 1) return;
                SlideLevel(currentLevel, levels[currentLevel].XYZ().x, -Screen.width / 2);
                currentLevel++;
                if (currentLevel > levels.Count - 1)
                {
                    currentLevel--;
                    return;
                }
                previousLevel++;
            }
            if (mouseDirection.x < 0)
            {
                if (previousLevel < 0) return;
                SlideLevel(previousLevel, levels[previousLevel].XYZ().x, Screen.width / 2);
                previousLevel--;
                if (previousLevel <0)
                {
                    currentLevel = 0;
                    return;
                }
                currentLevel--;
            }
            return;
        }
        if (mouseDown)
        {
            if (slided) return;
            mousePosition = Input.mousePosition;
            mouseDirection.x = startMousePosition.x - mousePosition.x;
            mouseDirection.y = startMousePosition.y - mousePosition.y;
            if (currentLevel >= levels.Count - 1 && mouseDirection.x > 0) return;
            if (currentLevel <= 0 && mouseDirection.x < 0) return;
            if (mouseDirection.x > 0)
            {
                levels[currentLevel].MoveXY(startCurrentLevelPosition.x - mouseDirection.x, startCurrentLevelPosition.y);
            }
            if (mouseDirection.x < 0)
            {
                levels[previousLevel].MoveXY(-Screen.width / 2 - mouseDirection.x, startCurrentLevelPosition.y);
            }
            return;
        }
    }

    private void SlideLevel(int level, float startPosition, float endPosition)
    {
        StartCoroutine(SlideLevelCoroutine(level, startPosition, endPosition));
    }

    private IEnumerator SlideLevelCoroutine(int level, float startPosition, float endPosition)
    {
        slided = true;
        float time = 0f;
        while (time <= animationTime)
        {
            time += Time.deltaTime;
            levels[level].MoveXY(Mathf.Lerp(startPosition, endPosition, curve.Evaluate(time/animationTime)), startCurrentLevelPosition.y);
            yield return null;
        }
        slided = false;
        mouseDown = false;
    }

    public void SlideLevelLeft()
    {
        if (currentLevel == levels.Count - 1) return;
        SlideLevel(currentLevel, levels[currentLevel].XYZ().x, -Screen.width / 2);
        currentLevel++;
        if (currentLevel > levels.Count - 1)
        {
            currentLevel--;
            return;
        }
        previousLevel++;
    }

    public void SlideLevelRight()
    {
        if (previousLevel < 0) return;
        SlideLevel(previousLevel, levels[previousLevel].XYZ().x, Screen.width / 2);
        previousLevel--;
        if (previousLevel < 0)
        {
            currentLevel = 0;
            return;
        }
        currentLevel--;
    }

    public void ShowButtonStartLevel(int levelIndex)
    {
        levels[levelIndex].OpenLevel();
    }
}
