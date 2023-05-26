using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float radius = 3.0f;
    [SerializeField] private Bullet bulletPrefab;
    private Enemies enemies;

    private float attackSpeed;
    private float damage;
    private bool pause = false;

    private void Start()
    {
        enemies = FindObjectOfType<Enemies>();
        var settings = FindObjectOfType<PlayerStats>();
        GlobalEvents.Pause.AddListener(Pause);
        GlobalEvents.UnPause.AddListener(UnPause);
        damage = settings.Damage;
        attackSpeed = settings.AttackSpeed;
        StartCoroutine(ShootingLoop());
    }

    private void Pause()
    {
        pause = true;
    }

    private void UnPause()
    {
        pause = false;
    }

    private IEnumerator ShootingLoop()
    {
        while (true)
        {
            yield return Shooting();
            yield return new WaitForSeconds(0.005f / attackSpeed);
        }
    }

    private IEnumerator Shooting()
    {
        Enemy target = FindTarget();
        float timer = 0;
        while (target != null && target.transform.parent != null) 
        {
            timer += Time.deltaTime;
            transform.LookAt(target.transform);
            transform.Rotate(new Vector3(0, 90, 90));
            if (timer >= attackSpeed && !pause)
            {
                timer = 0;
                Bullet bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                bullet.damage = damage;
                bullet.transform.LookAt(target.transform);
            }
            yield return null;
        }
    }

    private Enemy FindTarget()
    {
        Enemy target = null;
        var enemies = this.enemies.GetComponentsInChildren<Enemy>();

        float min_distanse = radius;
        foreach (var enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if ( distance <= radius && distance < min_distanse)
            {
                min_distanse = distance;
                target = enemy;
            }
        }
        return target;
    }
}
