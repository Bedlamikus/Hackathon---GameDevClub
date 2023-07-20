using UnityEngine;
using UnityEngine.UI;

public class UILevel : MonoBehaviour
{
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject comigSoon;

    private RectTransform rectTransform;

    public void MoveXY(float x, float y)
    {
        var z = position.position.z;
        position.position = new Vector3(x, y, z);
    }

    public void MoveZ(int z)
    {
        var currentPosition = position.position;
        currentPosition.z = z;
        position.position = currentPosition;
    }

    public Vector3 XYZ()
    {
        return position.position;
    }

    private RectTransform position
    {
        get
        {
            if (rectTransform == null)
            {
                rectTransform = GetComponent<RectTransform>();
            }
            return rectTransform;
        }
    }

    public void OpenLevel()
    {
        startButton.SetActive(true);
        comigSoon.SetActive(false);
    }
}
