using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetGridX : MonoBehaviour
{
    private void Start()
    {
        Vector3 position = transform.position;
        transform.position = new Vector3(Mathf.Round(position.x), position.y, position.z);
    }
}
