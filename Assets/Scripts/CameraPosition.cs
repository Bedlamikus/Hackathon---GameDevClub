using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    [SerializeField] private bool target;
    [SerializeField] private Train train;
    [SerializeField] private float speed;
    [SerializeField] private GameObject mayak;

    private Vector3 trail;
    private void Start()
    {
        if (target)
        {
            trail = train.transform.position;
        }
    }
    private void Update()
    {
        if (!target) return;
        transform.position = Vector3.Lerp(
            transform.position, 
            mayak.transform.position, 
            speed * Time.deltaTime);
        trail = Vector3.Lerp(trail, train.transform.position, speed * Time.deltaTime);
        transform.LookAt(trail);
    }
}
