using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float radius = 3.0f;
    [SerializeField] private float shootSpeed = 0.3f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private List<GameObject> enemyes = new List<GameObject>();

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
        GameObject target = FindTarget();
        Debug.Log("I find target: " + target);
        float timer = 0;
        while (target) 
        {
            timer += Time.deltaTime;
            transform.LookAt(target.transform);
            transform.Rotate(new Vector3(0, 90, 90));
            if (timer >= shootSpeed)
            {
                timer = 0;
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                bullet.transform.LookAt(target.transform);
            }
            yield return null;
        }
    }

    private GameObject FindTarget()
    {
        GameObject target = null;
        float min_distanse = radius;
        for (int i = 0; i < enemyes.Count; i++)
        {
            float distance = Vector3.Distance(transform.position, enemyes[i].transform.position);
            if ( distance <= radius && distance < min_distanse)
            {
                min_distanse = distance;
                target = enemyes[i];
            }
        }
        return target;
    }
}
