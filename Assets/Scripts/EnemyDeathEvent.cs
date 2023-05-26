using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathEvent : MonoBehaviour
{
    [SerializeField] private GameObject parent;
    public void EventDeath()
    {
        Destroy(parent);
    }
}
