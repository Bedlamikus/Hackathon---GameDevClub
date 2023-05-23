using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerStyle : MonoBehaviour
{
    public Vector2 coordinate;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            coordinate = Input.mousePosition;
    }
}
