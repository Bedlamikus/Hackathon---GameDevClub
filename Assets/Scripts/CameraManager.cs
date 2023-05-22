using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private List<CameraPosition> positions;

    private void Start()
    {
        GlobalEvents.BattleTrainDie.AddListener(MoveCameraToFirstScreen);
        GlobalEvents.EndBattle.AddListener(MoveCameraToFirstScreen);
        GlobalEvents.StartBattle.AddListener(MoveCameraToBatlleScreen);
    }

    private void MoveCameraToFirstScreen()
    {
        _camera.transform.SetParent(positions[0].transform, false);
    }

    private void MoveCameraToBatlleScreen()
    {
        _camera.transform.SetParent(positions[1].transform, false);
    }
}
