using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedWay : MonoBehaviour
{
    [SerializeField] private MovedWay wayPrefab;

    private void Start()
    {
        GlobalEvents.DestroyWay.AddListener(CreateWay);
        GlobalEvents.StartBatlle.AddListener(Initialize);
    }

    private void Initialize()
    {
        MovedWay way = InstantiateWay(wayPrefab);
        way.transform.position = transform.position;
        way.Distance = 40.0f;
        way = InstantiateWay(wayPrefab);
        way.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 20.0f);
        way.Distance = 20.0f;
    }

    private void CreateWay()
    {
        InstantiateWay(wayPrefab);
    }

    private MovedWay InstantiateWay(MovedWay way)
    {
        way = Instantiate(wayPrefab, transform.position, Quaternion.identity);
        way.transform.parent = transform;
        return way;
    }
}
