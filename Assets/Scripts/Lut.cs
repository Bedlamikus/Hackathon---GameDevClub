using System.Collections;
using UnityEngine;

public enum LutType
{
    COIN,
    HLAM,
}

public class Lut : MonoBehaviour
{
    [SerializeField] private LutType lutType;
    [SerializeField] private int amount = 1;
    [SerializeField] private float angleSpeed = 1f;
    [SerializeField] private float mooveUpSpeed = 1f;
    [SerializeField] private float mooveUpDirection = 0.3f;
    [SerializeField] private bool destroyAfterMooveUp = true;
    [SerializeField] private float ofssetY = 1.0f;
    [SerializeField] private GameObject particles;

    private Mouse mouse;
    private FightSound sound;
    private bool pause = false;
    private bool dies = false;

    private void Start()
    {
        GlobalEvents.Pause.AddListener(Pause);
        GlobalEvents.UnPause.AddListener(UnPause);
        GlobalEvents.EndBattle.AddListener(Die);
        sound = FindObjectOfType<FightSound>();
        mouse = FindObjectOfType<Mouse>();
        var position = transform.position;
        position.y += ofssetY;
        transform.position = position;
        StartCoroutine(Life());
    }

    private IEnumerator Life()
    {
        float needTime = mooveUpDirection / mooveUpSpeed;
        var startPosition = transform.position;
        var endPosition = startPosition;
        endPosition.y += mooveUpDirection;
        float timer = 0f;
        while (timer <= needTime)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, endPosition, timer / needTime);
            transform.Rotate(0, angleSpeed, 0);
            yield return null;
        }
        if (destroyAfterMooveUp) Die();
    }

    private void OnMouseOver()
    {
        if (dies) return;
        if (pause) return;
        if (mouse.pressed)
        {
            ApplyLut();
            Die();
        }
    }

    private void ApplyLut()
    {
        if (lutType == LutType.COIN)
        {
            GlobalEvents.ApplyGolds.Invoke(amount);
        }
        if (lutType == LutType.HLAM)
        {
            GlobalEvents.ApplyHlam.Invoke(amount);
        }
        sound.ApplyCoin();
    }

    private void Die()
    {
        dies = true;
        particles.transform.parent = null;
        particles.SetActive(true);
        Destroy(gameObject);
    }

    private void Pause()
    {
        pause = true;
    }
    private void UnPause()
    {
        pause = false;
    }
}
