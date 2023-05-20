using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetGridZ : MonoBehaviour
{
    private void Start()
    {
        Vector3 position = transform.position;
        transform.position = new Vector3(position.x, position.y, Mathf.Round(position.z));
    }
}
