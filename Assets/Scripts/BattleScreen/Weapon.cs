using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float radius = 3.0f;
    [SerializeField] private float shootSpeed = 0.3f;
    [SerializeField] private int damage = 20;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Enemies enemies;


    private void Start()
    {
        StartCoroutine(ShootingLoop());
    }

    private IEnumerator ShootingLoop()
    {
        while (true)
        {
            yield return Shooting();
            Debug.Log("Try Find Enemy");
            yield return new WaitForSeconds(shootSpeed);
        }
    }

    private IEnumerator Shooting()
    {
        Enemy target = FindTarget();
        Debug.Log("I find target: " + target);
        float timer = 0;
        while (target != null) 
        {
            timer += Time.deltaTime;
            transform.LookAt(target.transform);
            transform.Rotate(new Vector3(0, 90, 90));
            if (timer >= shootSpeed)
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
        if (enemies.enemies.Count == 0) return target;
        float min_distanse = radius;
        for (int i = 0; i < enemies.enemies.Count; i++)
        {
            if (enemies.enemies[i] == null) return target;
            float distance = Vector3.Distance(transform.position, enemies.enemies[i].transform.position);
            if ( distance <= radius && distance < min_distanse)
            {
                min_distanse = distance;
                target = enemies.enemies[i];
            }
        }
        return target;
    }
}
