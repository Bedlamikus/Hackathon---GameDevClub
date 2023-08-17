using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Train : MonoBehaviour
{
    [SerializeField] private Rails rails;
    [SerializeField] private Transform station;
    [SerializeField] private float speed;

    [SerializeField] private AudioClip choo;
    [SerializeField] private AudioClip continueChoo;
    [SerializeField] private AudioSource sound;
    [SerializeField] private float angleSpeed = 5f;
    [SerializeField] private GameObject forwardPosition;

    public bool inFight = false;
    private int paused = 0;
    private Quaternion startRotation;

    private bool restart = false;
    private Coroutine ride;

    private PlayerStats settings;

    public void Init()
    {
        GlobalEvents.Pause.AddListener(Pause);
        GlobalEvents.UnPause.AddListener(UnPause);
        GlobalEvents.StartBattle.AddListener(BattleStart);
        GlobalEvents.EndBattle.AddListener(BattleEnd);
        restart = false;
        startRotation = transform.rotation;
    }

    private IEnumerator RotationToPoint(Vector3 point)
    {
        var currentPoint = forwardPosition.transform.position;
        float needTime = (currentPoint - point).magnitude / angleSpeed;
        float timer = 0f;
        while (timer < needTime)
        {
            timer += Time.deltaTime;
            transform.LookAt(Vector3.Lerp(currentPoint, point, timer / needTime));
            yield return null;
        }
    }

    private IEnumerator MoveToPoint(Vector3 point)
    {
        if (restart) yield break;
        Vector3 startPoint = transform.position;
        Vector3 endPoint = new Vector3(point.x, startPoint.y, point.z);
        float needTime = (endPoint - startPoint).magnitude / speed;
        float timer = 0;

        StartCoroutine(RotationToPoint(new Vector3(point.x, startPoint.y, point.z)));

        while (timer < needTime)
        {
            timer += Time.deltaTime * paused;
            transform.position = Vector3.Lerp(startPoint, endPoint, timer / needTime);
            yield return null;
            if (restart) yield break;
        }
        transform.position = endPoint;
    }

    private bool fromStation = true;
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
        fromStation = true;
    }

    public void Pause()
    {
        paused = 0;
        sound.Stop();
    }

    public void UnPause()
    {
        if (inFight) return;
        
        if (fromStation)
        {
            fromStation = false;
            if (Settings.Mute == false)
                sound.PlayOneShot(choo, 0.2f);
        }
        else
        {
            if (Settings.Mute == false)
                sound.Play();
        }
        paused = 1;
    }

    private void BattleStart()
    {
        inFight = true;
    }
    private void BattleEnd()
    {
        inFight = false;
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

    private PlayerStats Settings
    {
        get
        {
            if (settings == null)
            {
                settings = FindObjectOfType<PlayerStats>();
            }
            return settings;
        }
    }
}
