using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rails : MonoBehaviour
{
    [SerializeField] private List<WayPoint> waypoints;
    [SerializeField] private List<GameObject> rails;

    private List<Vector2Int> occupiedPositions = new();

    private int currentWayPoint = 0;
    private int previousWayPoint = 0;

    public void Init()
    {
        Restart();
    }

    public int Count()
    {
        return waypoints.Count;
    }

    public void SetNextPoint()
    {
        previousWayPoint = currentWayPoint;
        currentWayPoint++;
        if (currentWayPoint >= waypoints.Count)
        { 
            currentWayPoint = 0; 
        }
    }

    public Vector3 CurrentPoint()
    {
        return waypoints[currentWayPoint].transform.position;
    }

    public Vector3 PreviousPoint()
    {
        return waypoints[previousWayPoint].transform.position;
    }

    private void Restart()
    {
        currentWayPoint = 0;
        previousWayPoint = 0;
    }

    public void CleanOccupiedPositions()
    {
        occupiedPositions.Clear();
        occupiedPositions.Add(new Vector2Int(0, 2));
        occupiedPositions.Add(new Vector2Int(0, 3));
        occupiedPositions.Add(new Vector2Int(0, 4));
    }
    public Vector3 GetRandomRailPosition()
    {
        var position = rails[Random.Range(0, rails.Count)].transform.position;
        var occupiedPosition = new Vector2Int((int)position.x, (int)position.y);
        while (occupiedPositions.Contains(occupiedPosition))
        {
            position = rails[Random.Range(0, rails.Count)].transform.position;
            occupiedPosition = new Vector2Int((int)position.x, (int)position.y);
        }
        occupiedPositions.Add(occupiedPosition);
        return position;
    }
}
