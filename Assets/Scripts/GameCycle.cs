using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCycle : MonoBehaviour
{
    [SerializeField] private StressPoint stressPointPrefab;

    public void Init(CycleSettings cycleSettings)
    {
        var rails = FindObjectOfType<Rails>();
        foreach (var stressPoint in cycleSettings.battlePoints)
        {
            var stress = Instantiate(stressPointPrefab, transform);
            if (stressPoint.position == Vector2Int.zero)
                stress.transform.position = rails.GetRandomRailPosition();
            else
                stress.transform.localPosition = new Vector3(stressPoint.position.x, 0, stressPoint.position.y);
            stress.Init(stressPoint);
        }
    }
}
