using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCycle : MonoBehaviour
{
    [SerializeField] private StressPoint stressPointPrefab;

    public void Init(CycleSettings cycleSettings)
    {
        foreach (var stressPoint in cycleSettings.battlePoints)
        {
            var stress = Instantiate(stressPointPrefab, transform);
            stress.transform.localPosition = new Vector3(stressPoint.position.x, 0, stressPoint.position.y);
            stress.Init(stressPoint);
        }
    }
}
