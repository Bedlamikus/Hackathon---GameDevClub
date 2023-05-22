using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTrain : MonoBehaviour
{
    [SerializeField] private int health = 1000;
    [SerializeField] private int armor = 300;

    public void Init()
    { 
        //etc
    }

    public void ApplyDamage(int damage)
    {
        int armorCalc = armor - damage;
        if (armorCalc > 0) 
        { 
            armor = armorCalc;
            return;
        }
        armor = 0;
        health += armorCalc;
        if (health <= 0)
        {
            GlobalEvents.BattleTrainDie.Invoke();
        }
    }
}
