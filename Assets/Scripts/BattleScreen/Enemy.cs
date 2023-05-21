using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private float speed;
    [SerializeField] private float cost;
    [SerializeField] private float attackDistance;
    [SerializeField] private int damage;

    [SerializeField] private BattleTrain target = null;

    private void Start()
    {
        target = FindObjectOfType<BattleTrain>();
        StartCoroutine(LifeCycle());
    }

    public void ApplyDamage(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            GlobalEvents.EnemyDie.Invoke(this);
            GlobalEvents.ApplyCoins.Invoke(cost);

            Destroy(gameObject);
        }
    }

    private IEnumerator RunToTarget()
    {
        while (Vector3.Distance(transform.position, target.transform.position) > attackDistance)
        {
            Vector3 targetPosition = target.transform.position;
            targetPosition.y = transform.position.y;
            transform.LookAt(targetPosition);
            transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime / (Vector3.Distance(transform.position, target.transform.position) / speed));
            yield return null;
        }
    }

    private IEnumerator AttackTarget()
    {
        if (target != null)
        {
            //Can attack from distance
            //target.ApplyDamage(damage);
            yield return new WaitForSeconds(1.0f);
            
        }
        yield return null;
    }

    private IEnumerator LifeCycle()
    {
        while (true)
        {
            yield return RunToTarget();
            yield return AttackTarget();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        BattleTrain train = collision.gameObject.GetComponent<BattleTrain>();
        if (train)
        {
            train.ApplyDamage(damage);
        }
    }
}
