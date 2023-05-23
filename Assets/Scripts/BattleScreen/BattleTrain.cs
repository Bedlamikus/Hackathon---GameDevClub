using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTrain : MonoBehaviour
{
    private float health;
    private float armor;

    public void Init()
    { 
        var settings = FindObjectOfType<PlayerStats>();
        health = settings.Health;
        armor = settings.Armor;
    }

    public void ApplyDamage(float damage)
    {
        health -= damage - damage * armor / 100;
        if (health <= 0)
        {
            GlobalEvents.BattleTrainDie.Invoke();
        }
    }
}
