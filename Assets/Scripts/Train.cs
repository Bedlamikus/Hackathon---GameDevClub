using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    [SerializeField] private Rails rails;
    [SerializeField] private Transform station;
    [SerializeField] private float speed;

    [SerializeField] private AudioClip choo;
    [SerializeField] private AudioSource sound;

    private int paused = 0;
    private Quaternion startRotation;

    private bool restart = false;
    private Coroutine ride;

    public void Init()
    {
        GlobalEvents.Pause.AddListener(Pause);
        GlobalEvents.UnPause.AddListener(UnPause);
        restart = false;
        startRotation = transform.rotation;
    }

    private IEnumerator MoveToPoint(Vector3 point)
    {
        if (restart) yield break;
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
        for (int i = 0; i < rails.Count(); i++)
        {
            rails.SetNextPoint();
            if (restart || rails == null) yield break;
            yield return MoveToPoint(rails.CurrentPoint());
        }
        ride = null;
        Pause();
    }

    public void Pause()
    {
        paused = 0;
        sound.Stop();
    }

    public void UnPause()
    {
        sound.PlayOneShot(choo, 0.2f);
        paused = 1;
    }

    public bool IsRide()
    {
        if (ride != null) return true;
        return false;
    }

    public void StartRide()
    {
        ride = StartCoroutine(Ride());
        UnPause();
    }

    public void ResetPosition()
    {
        Pause();
        if (ride != null) { StopCoroutine(ride); }
        ride = null;
        transform.rotation = startRotation;
        transform.position = new Vector3(station.position.x, transform.position.y, station.position.z);
    }
}
