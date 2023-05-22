using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StressPositions : MonoBehaviour
{
    [SerializeField ]private List<GameObject> points = new List<GameObject>();

    public void Init()
    {
        var pointsInChildren = GetComponentsInChildren<StressPosition>();
        foreach (var point in pointsInChildren)
        {
            points.Add(point.gameObject);
        }
    }
    public GameObject Pop()
    {
        if (points.Count == 0) return null;
        int randomItemIndex = Random.Range(0, points.Count);
        var point = points[randomItemIndex];
        points.RemoveAt(randomItemIndex);
        return point;
    }
}
