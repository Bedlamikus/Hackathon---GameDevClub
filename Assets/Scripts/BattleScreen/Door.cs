using System;
using System.Collections;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts.BattleScreen
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private bool opened = false;
        [SerializeField] private int speed = 1;
        [SerializeField] private GameObject door;
        [SerializeField] private float angleDirection = 90;

        private void Start()
        {
            if (opened)
            {
                Open();
                return;            
            }
            Close();
        }
        private void Close()
        {
            StartCoroutine(DirectionDoor(false));
        }
        private void Open()
        {
            StartCoroutine(DirectionDoor(true));
        }
        private IEnumerator DirectionDoor(bool open = false)
        {
            float startAngle = 0;
            float endAngle = 90;
            float currentAngle = 0;
            float timer = 1;
            float dirTimer = -1;
            if (open)
            {
                timer = 0;
                dirTimer = 1;
            }

            while (timer >= 0 && timer <= 1)
            {
                timer += Time.deltaTime * speed * dirTimer;
                currentAngle = Mathf.LerpAngle(startAngle, endAngle, timer);
                door.transform.localEulerAngles = new Vector3(currentAngle - angleDirection, 0, 0);
                yield return null;
            }
        }
    }
}