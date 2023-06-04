using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public bool pressed = false;

    void Update()
    {
        pressed = false;
        if (Input.GetMouseButton(0))
            pressed = true;
    }
}
