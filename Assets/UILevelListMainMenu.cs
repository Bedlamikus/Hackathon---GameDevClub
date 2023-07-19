using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UILevelListMainMenu : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private List<UILevel> levels;
    private int currentLevel = 0;
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
            mouseDown = true;
            startMousePosition = Input.mousePosition;
            startCurrentLevelPosition = levels[currentLevel].XYZ();
            return;
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (mouseDown == false) return;
            mouseDown = false;
            SlideCurrentLevel(levels[currentLevel].XYZ().x, );
            return;
        }
        if (mouseDown)
        {
            mousePosition = Input.mousePosition;
            mouseDirection.x = startMousePosition.x - mousePosition.x;
            mouseDirection.y = startMousePosition.y - mousePosition.y;
            levels[currentLevel].MoveXY(startCurrentLevelPosition.x - mouseDirection.x, startCurrentLevelPosition.y);
            return;
        }
    }

    private void SlideCurrentLevel(float startPosition, float endPosition)
    {
        StartCoroutine(SlideCurrentLevelCoroutine(startPosition, endPosition));
    }

    private IEnumerator SlideCurrentLevelCoroutine(float startPosition, float endPosition)
    {
        float time = 0f;
        while (time <= 1f)
        {
            time += Time.deltaTime;
            levels[currentLevel].MoveXY(Mathf.Lerp(startPosition, endPosition, time), startCurrentLevelPosition.y);
            yield return null;
        }
    }

}
