using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedWay : MonoBehaviour
{
    [SerializeField] private MovedWay wayPrefab;

    private void Start()
    {
        GlobalEvents.DestroyWay.AddListener(CreateWay);
    }

    private void CreateWay()
    {
        MovedWay way = Instantiate(wayPrefab, transform.position, Quaternion.identity);
        way.transform.parent = transform;
    }
}
