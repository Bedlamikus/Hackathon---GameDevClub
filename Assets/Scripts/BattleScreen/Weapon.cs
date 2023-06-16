using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float radius = 3.0f;
    [SerializeField] private Bullet bulletPrefab;
    private Enemies enemies;

    private bool pause = false;
    private PlayerStats playerSettings;
    private FightSound sound;
    private float attackSpeed;
    private bool superAttack = false;

    private void Start()
    {
        sound = FindObjectOfType<FightSound>();
        enemies = FindObjectOfType<Enemies>();
        playerSettings = FindObjectOfType<PlayerStats>();
        attackSpeed = playerSettings.attackSpeed.Value();
        GlobalEvents.Pause.AddListener(Pause);
        GlobalEvents.UnPause.AddListener(UnPause);
        GlobalEvents.SuperAttackSpeed.AddListener(SuperAttackSpeed);
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

    float timer;
    private IEnumerator ShootingLoop()
    {
        timer = 1 / attackSpeed;
        while (true)
        {
            Enemy target = FindTarget();
            timer += Time.deltaTime;
            if (target != null)
            {
                transform.LookAt(target.transform);
                transform.Rotate(new Vector3(0, 90, 65));
            }
            yield return Shooting(target);
        }
    }

    private IEnumerator Shooting(Enemy target)
    {
        while (target != null && target.transform.parent != null) 
        {
            timer += Time.deltaTime;
            transform.LookAt(target.transform);
            transform.Rotate(new Vector3(0, 90, 65));
            if (timer >= 1/ attackSpeed && !pause)
            {
                timer = 0;
                Bullet bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                bullet.damage = playerSettings.Damage;
                bullet.transform.LookAt(target.transform);
                bullet.transform.Rotate(new Vector3(-7, 0, 0));
                sound.PlayShootStandart();
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

    private void SuperAttackSpeed(float time)
    {
        if (superAttack) return;
        StartCoroutine(SuperAttackSpeedCoroutine(time));
    }

    private IEnumerator SuperAttackSpeedCoroutine(float time)
    {
        superAttack = true;
        attackSpeed = playerSettings.attackSpeed.Value() * 3;
        yield return new WaitForSeconds(time);
        attackSpeed = playerSettings.attackSpeed.Value();
        superAttack = false;
    }
}
