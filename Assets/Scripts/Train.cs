using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    [SerializeField] private Rails rails;
    [SerializeField] private Transform station;
    [SerializeField] private float speed;

    private int paused = 0;
    private Quaternion startRotation;

    private bool restart = false;
    private Coroutine ride;

    private void Start()
    {
        GlobalEvents.Pause.AddListener(Pause);
        GlobalEvents.UnPause.AddListener(UnPause);
        GlobalEvents.Restart.AddListener(ResetPosition);

        startRotation = transform.rotation;

        ride = StartCoroutine(Ride());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Paused");
            GlobalEvents.StressWin.Invoke();
            GlobalEvents.EndBatlle.Invoke();
        }
    }

    private IEnumerator MoveToPoint(Vector3 point)
    {
        Vector3 startPoint = transform.position;
        Vector3 endPoint = new Vector3(point.x, startPoint.y, point.z);
        float needTime = (endPoint - startPoint).magnitude / speed;
        float timer = 0;

        transform.LookAt(new Vector3(point.x, startPoint.y, point.z));

        while (timer < needTime)
        {
            timer += Time.deltaTime * paused;
            transform.position = Vector3.Lerp(startPoint, endPoint, timer / needTime);
            yield return null;
            if (restart) yield break;
        }
        transform.position = endPoint;
    }

    private IEnumerator Ride()
    {
        while (true)
        {
            rails.SetNextPoint();
            if (restart) yield break;
            yield return MoveToPoint(rails.CurrentPoint());

        }
    }

    private void Pause()
    {
        paused = 0;
    }

    private void UnPause()
    {
        paused = 1;
        restart = false;
        if (ride == null) StartRide();
    }
    private void StartRide()
    {
        ride = StartCoroutine(Ride());
    }
    private void ResetPosition()
    {
        Pause();
        transform.rotation = startRotation;
        restart = true;
        StopAllCoroutines();
        ride = null;
        transform.position = new Vector3( station.position.x, transform.position.y, station.position.z);
        
    }
}
