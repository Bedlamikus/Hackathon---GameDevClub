using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovedWay : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float distance = 20.0f;

    private void Start()
    {
        StartCoroutine(MoveTo());
    }

    private IEnumerator MoveTo()
    {
        Vector3 startPoint = transform.position;
        Vector3 endPoint = transform.position;
        endPoint.z -= distance;
        float needTime = distance / speed;
        float timer = 0;
        while (timer <= needTime)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(startPoint, endPoint, timer / needTime);
            yield return null;
        }
        GlobalEvents.DestroyWay.Invoke();
        Destroy(gameObject);
    }

    public float Distance
    {
        get { return distance; }
        set { distance = value; }
    }

}
