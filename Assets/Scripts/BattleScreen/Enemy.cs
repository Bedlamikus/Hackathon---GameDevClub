using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class Enemy : MonoBehaviour
{
    [SerializeField] private UIPositionToWorldObject prefabUIPosition;
    [SerializeField] private Canvas canvas;

    [SerializeField] private float health;
    [SerializeField] protected float speed_velocity;
    [SerializeField] protected float speedWay_velocity;
    [SerializeField] private int cost;
    [SerializeField] protected float attackDistance;
    [SerializeField] private float coolDownAttack;
    [SerializeField] private float damage;
    [SerializeField] private float heightheightAboveGround = 0.51f;
    [SerializeField] private Animator animator;
    [SerializeField] private Coin coin;
    [SerializeField] protected BattleTrain target = null;
    protected Rigidbody rb;
    [SerializeField] private Collider _collider;
    protected bool attack = false;
    private bool pause = false;
    protected bool charles = false;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = FindObjectOfType<BattleTrain>();
        _collider = GetComponent<Collider>();
        StartCoroutine(LifeCycle());

        GlobalEvents.BattleTrainDie.AddListener(Die);
        GlobalEvents.Pause.AddListener(Pause);
        GlobalEvents.UnPause.AddListener(UnPause);
    }

    public void Init(int health, float coolDownAttack, float adamage)
    {
        this.health = health;
        this.coolDownAttack = coolDownAttack;
        this.damage = adamage;
    }

    private void Pause()
    {
        pause = true;
        rb.velocity = Vector3.zero;
    }

    private void UnPause()
    {
        pause = false;
    }

    public void ApplyDamage(float damage)
    {
        if (pause) return;
        health -= damage;
        var text = Instantiate(prefabUIPosition, canvas.transform);
        if (health > 0)
        {
            text.Init("-" + damage.ToString(), transform, 100, true, false);
        }
        else
        {
            text.Init("-" + (damage + health).ToString(), transform, 100, true, false);
            Die();
        }
    }

    private IEnumerator MooveWith(Transform target)
    {
        if (!target) yield break;
        float direction = transform.position.z - target.position.z;
        while (true)
        {
            if (target)
            {
                var selfPosition = transform.position;
                selfPosition.z = target.position.z + direction;
                transform.position = selfPosition;
            }
            yield return null;
        }
    }

    private void Die()
    {
        _collider.enabled = false;
        transform.parent = null;
        rb.isKinematic = true;
        var movedWay = FindObjectOfType<MovedWay>();
        if (movedWay != null)
        {
            StartCoroutine(MooveWith(movedWay.transform));
        }
        animator.SetBool("bool_die", true);
        GlobalEvents.EnemyDie.Invoke();
        GlobalEvents.ApplyGolds.Invoke(cost);
        GlobalEvents.ApplyExperience.Invoke(cost);
        GlobalEvents.ApplyHlam.Invoke(cost);
        if (Random.Range(0,2) == 1) Instantiate(coin, transform.position, Quaternion.identity);
    }

    protected virtual IEnumerator RunToTarget(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > attackDistance)
        {
            if (!pause)
            {
                transform.LookAt(targetPosition);
                transform.position = new Vector3(transform.position.x, heightheightAboveGround, transform.position.z);
                rb.AddForce(speed_velocity * Time.deltaTime * transform.forward);
                rb.AddForce(speedWay_velocity * Time.deltaTime * new Vector3(0, 0, -1));
            }
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
            if (!pause)
            {
                yield return RunToTarget(target.transform.position);
                yield return AttackTarget();
            }
            yield return null;
        }
    }

    float timerAttack = 0;
    protected void OnCollisionStay(Collision collision)
    {
        if (timerAttack > 0) return;
        if (pause) return;
        BattleTrain train = collision.gameObject.GetComponent<BattleTrain>();
        if (train)
        {
            GlobalEvents.ApplyDamage.Invoke(damage);
            timerAttack = coolDownAttack;
            attack = true;
        }
    }

    private void Update()
    {
        if (pause) return;
        if (timerAttack > 0)
            timerAttack -= Time.deltaTime;
    }
}
