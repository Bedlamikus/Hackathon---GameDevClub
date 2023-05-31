using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCycle : MonoBehaviour
{
    [SerializeField] private StressPoint stressPointPrefab;
    private int num;
    public void CreateSycle(ExcelSettings data)
    {
        num = 0;
        Init(data.settings[0]);
    }
    private void Start()
    {
        GlobalEvents.SettingsLoaded.AddListener(CreateSycle);
    }
    //private List<StressPoint> stressPoints = new();

    public void Init(CycleSettings cycleSettings)
    {
        var prefabs = FindObjectOfType<EnemyPrefabs>();

        num = cycleSettings.num;
        foreach (var stressPoint in cycleSettings.battlePoints)
        {
            var stress = Instantiate(stressPointPrefab, transform);
            stress.transform.localPosition = new Vector3(stressPoint.position.x, 0, stressPoint.position.y);
            stress.Init(stressPoint);
        }
    }
}
