using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressEnemy : MonoBehaviour
{
    private void Start()
    {
        Vector3 vector = transform.position + new Vector3(Random.Range(-2.0f, 2.0f), 0, Random.Range(-2.0f, 2.0f));
        transform.LookAt(vector);
        Vector3 vectorPosition = transform.position + new Vector3(Random.Range(-.2f, .2f), 0, Random.Range(-.2f, .2f));
        transform.position = vectorPosition;
    }
}
