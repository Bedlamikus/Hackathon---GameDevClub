using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    [SerializeField] private Rails rails;
    [SerializeField] private float speed;
    [SerializeField] private float angleSpeed;

    private float currentSpeed;
    private int paused = 1;

    private void Start()
    {
        currentSpeed = speed;
        StartCoroutine(Ride());
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            paused = 0;
            Debug.Log("Paused");
        }
        if (Input.GetKey(KeyCode.Return))
        {
            paused = 1;
            Debug.Log("UnPaused");
        }
    }

    private IEnumerator MoveToPoint(Vector3 point)
    {
        Vector3 startPoint = transform.position;
        Vector3 endPoint = new Vector3(point.x, transform.position.y, point.z);
        float needTime = (endPoint - startPoint).magnitude / speed;
        float timer = 0;
        while (timer < needTime)
        {
            timer += Time.deltaTime * paused;
            transform.position = Vector3.Lerp(startPoint, endPoint, timer / needTime);
            yield return null;
        }
        transform.position = endPoint;
    }

    private IEnumerator Ride()
    {
        while (true)
        {
            rails.SetNextPoint();
            yield return MoveToPoint(rails.CurrentPoint());

        }
    }
}
