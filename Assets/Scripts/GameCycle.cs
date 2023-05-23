using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameCycle : MonoBehaviour
{
    [SerializeField] private List<StressPoints> stressPoints;
    
    public void Init(int numCycle)
    {
        for (int i = 0; i < numCycle; i++)
        {
            NextPoint();
        }
        stressPoints[0].gameObject.SetActive(true);
        GlobalEvents.StationEnter.AddListener(NextPoint);
    }

    public void NextPoint()
    {
        Destroy(stressPoints[0]);
        stressPoints.RemoveAt(0);
        if (stressPoints.Count == 0) return;
        stressPoints[0].gameObject.SetActive(true);
    }
}
