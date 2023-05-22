using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] protected float speed_velocity;
    [SerializeField] protected float speedWay_velocity;
    [SerializeField] private int cost;
    [SerializeField] protected float attackDistance;
    [SerializeField] private float coolDownAttack;
    [SerializeField] private int damage;
    [SerializeField] private float heightheightAboveGround = 0.51f;

    [SerializeField] protected BattleTrain target = null;
    protected Rigidbody rb;
    private Enemies enemies;
    protected bool attack = false;


    private void Start()
    {
        enemies = FindObjectOfType<Enemies>();
        rb = GetComponent<Rigidbody>();
        target = FindObjectOfType<BattleTrain>();
        StartCoroutine(LifeCycle());
    }

    public void ApplyDamage(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            GlobalEvents.EnemyDie.Invoke();
            GlobalEvents.ApplyCoins.Invoke(cost);
            enemies.enemies.Remove(this);
            Destroy(gameObject);
        }
    }

    protected virtual IEnumerator RunToTarget(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > attackDistance)
        {
            transform.LookAt(targetPosition);
            transform.position = new Vector3(transform.position.x, heightheightAboveGround, transform.position.z);
            rb.AddForce(transform.forward * speed_velocity);
            rb.AddForce(new Vector3(0,0,-1) * speedWay_velocity);
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

    protected virtual IEnumerator LifeCycle()
    {
        while (true)
        {
            yield return RunToTarget(target.transform.position);
            yield return AttackTarget();
        }
    }

    float timerAttack = 0;
    protected void OnCollisionStay(Collision collision)
    {
        if (timerAttack > 0) return;
        BattleTrain train = collision.gameObject.GetComponent<BattleTrain>();
        if (train)
        {
            train.ApplyDamage(damage);
            timerAttack = coolDownAttack;
            print("Attack");
            attack = true;
        }
    }

    private void Update()
    {
        if (timerAttack > 0)
            timerAttack -= Time.deltaTime;
    }
}
