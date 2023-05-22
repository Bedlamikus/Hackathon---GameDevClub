using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charls : Enemy
{
    [SerializeField] private int retreatDistance = 10;
    [SerializeField] private float retreadSpeed = 5.0f;
    [SerializeField] private float retreatTimer = 5.0f;

    private bool retreat = false;

    protected override IEnumerator LifeCycle()
    {
        while (true)
        {
            yield return RunToTarget(target.transform.position);
            retreat = true;
            yield return RunToTarget(FindRetreatPoint());
            retreat = false;
        }
    }

    protected override IEnumerator RunToTarget(Vector3 targetPosition)
    {
        float timer = 0;
        while (Vector3.Distance(transform.position, targetPosition) > attackDistance)
        {
            timer += Time.deltaTime;
            targetPosition.y = transform.position.y;
            transform.LookAt(targetPosition);
            if (retreat)
            {
                rb.AddForce(transform.forward * retreadSpeed);
                rb.AddForce(-transform.forward * retreadSpeed * 0.8f);
            }
            else
            {
                rb.AddForce(transform.forward * speed_velocity);
                rb.AddForce(-transform.forward * speed_velocity * 0.8f); ;
            }
            rb.AddForce(new Vector3(0, 0, -1) * speedWay_velocity);
            yield return null;
            if (retreat && timer > retreatTimer)
            {
                yield break;
            }
            if (attack)
            {
                attack = false;
                yield break;
            }
        }
    }


    private Vector3 FindRetreatPoint()
    {
        var point = transform.position;
        float x = point.x + Random.Range(-retreatDistance, retreatDistance);
        float z = point.z - retreatDistance;
        return new Vector3(x, point.y, z);
    }
}
